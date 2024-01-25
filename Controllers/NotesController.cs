using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MentalNote.Data;
using MentalNote.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace MentalNote.Controllers;

[Authorize]
[Route("api/[controller]/[action]")]
public class NotesController : Controller
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly MentalNoteDbContext _db;

    public NotesController(UserManager<IdentityUser> userManager, MentalNoteDbContext context)
    {
        _userManager = userManager;
        _db = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Notes>>> Index(string? sortOrder)
    {
        if (_db.Notes == null)
        {
            return NotFound();
        }

        ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
        ViewData["CategorySortParm"] = sortOrder == "Title" ? "title_desc" : "Title";

        IdentityUser currentUser = await _userManager.GetUserAsync(User);


        var item = from i in _db.Notes
                   where i.OwnerId == currentUser.Id
                   select i;

        switch (sortOrder)
        {
            case "name_desc":
                item = item.OrderByDescending(e => e.Title);
                break;

            default:
                item = item.OrderBy(n => n.NoteDate);
                break;
        }

        return View(await item.AsNoTracking().ToListAsync());

    }

    // APIs to create, edit and delete journal note
    [HttpGet]
    public IActionResult Create()
    {

        return View();
    }

    [HttpPost]
    public async Task<ActionResult<IEnumerable<Notes>>> Create([Bind("NoteID, NoteDate, Title, Note, Exercisesm, Owner, OwnerId")] Notes item)
    {
        if (ModelState.IsValid)
        {
            IdentityUser currentUser = await _userManager.GetUserAsync(User);

            item.Owner = currentUser;
            item.OwnerId = currentUser.Id;

            _db.Add(item);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(item);
    }

    //Edit an item
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Notes>>> Edit(int? id)
    {
        if (id == null || _db.Notes == null)
        {
            return NotFound();
        }

        var item = await _db.Notes.FindAsync(id);
        if (item == null)
        {
            return NotFound();
        }
        return View(item);
    }


    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> PostEdit(int id, [Bind("NoteID, NoteDate, Title, Note, Exercisesm, Owner, OwnerId")] Notes item)
    {
        if (id != item.NoteID)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                IdentityUser currentUser = await _userManager.GetUserAsync(User);

                item.Owner = currentUser;
                item.OwnerId = currentUser.Id;
                _db.Update(item);
                await _db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!NoteExists(item.NoteID))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToAction(nameof(Index));
        }
        return View(item);
    }

    //Delete items
    [HttpGet]
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null || _db.Notes == null)
        {
            return NotFound();
        }

        var item = await _db.Notes
            .FirstOrDefaultAsync(m => m.NoteID == id);
        if (item == null)
        {
            return NotFound();
        }

        return View(item);
    }


    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult PostDelete(int? id)
    {
        var item = _db.Notes.Find(id);

        if (item == null)
        {
            return NotFound();
        }
        _db.Notes.Remove(item);
        _db.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }


    private bool NoteExists(int id)
    {
        return (_db.Notes?.Any(e => e.NoteID == id)).GetValueOrDefault();
    }

    //To show the full content of the Notes and Exercises, allowing the user to read them
    [HttpGet]
    public IActionResult PatientView()
    {

        IEnumerable<Notes> objNoteList = _db.Notes.ToList();

        return View(objNoteList);
    }

    [HttpGet]
    [Route("api/Notes/Content/{id}")]
    public async Task<IActionResult> Content(int? id)
    {

        if (id == null || _db.Notes == null)
        {
            return NotFound();
        }

        var notes = await _db.Notes
            .FirstOrDefaultAsync(m => m.NoteID == id);
        if (notes == null)
        {
            return NotFound();
        }

        return View(notes);

    }
    /*[HttpGet]
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
     public IActionResult Error()
    {
        return View(new ErrorModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
    [HttpPost]
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult ErrorPost()
    {
        return View(new ErrorModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    } */
}

