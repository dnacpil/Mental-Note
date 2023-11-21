using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
//using Microsoft.AspNetCore.Identity;
//using System.Diagnostics;
using MentalNote.Data;
using MentalNote.Models;
//using MentalNote.Areas.Identity.Pages;

namespace MentalNote.Controllers;

[Route("api/[controller]/[action]")]
public class NotesController : Controller
{
    private readonly MentalNoteDbContext _db;

    public NotesController(MentalNoteDbContext context)
    {
        _db = context;
    }

    // GET including sorting functionality
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Notes>>> Index(string? sortOrder)
    {
        if (_db.Notes == null)
        {
            return NotFound();
        }

        ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
        ViewData["CategorySortParm"] = sortOrder == "Title" ? "title_desc" : "Title";
        //ViewData["PriceSortParm"] = sortOrder == "Price" ? "price_desc" : "Price";

        var note = from n in _db.Notes
                   select n;

        switch (sortOrder)
        {
            case "name_desc":
                note = note.OrderByDescending(e => e.Title);
                break;
            /* case "Category":
                products = products.OrderBy(p => p.CatId);
                break;
            case "category_desc":
                products = products.OrderByDescending(p => p.CatId);
                break;
            case "Price":
                products = products.OrderBy(p => p.CatId);
                break;
            case "price_desc":
                products = products.OrderByDescending(p => p.UnitPrice);
                break; */
            default:
                note = note.OrderBy(n => n.NoteDate);
                break;
        }

        return View(await note.AsNoTracking().ToListAsync());

    }

    // APIs to create, edit and delete journal note
    [HttpGet]
    public IActionResult Create()
    {
        var categories = _db.Notes.Select(c => c.Title).Distinct().ToList();
        var selectListItems = categories.Select(category => new SelectListItem
        {
            Text = category,
            Value = category
        }).ToList();

        ViewBag.Categories = selectListItems;

        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult<IEnumerable<Notes>>> Create([Bind("NoteID, NoteDate, Title, Note, Exercisesm, Owner, OwnerId")] Notes note)
    {
        if (ModelState.IsValid)
        {
            _db.Add(note);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(note);
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Notes>>> Edit(int? id)
    {
        if (id == null || _db.Notes == null)
        {
            return NotFound();
        }

        var note = await _db.Notes.FindAsync(id);
        if (note == null)
        {
            return NotFound();
        }
        return View(note);
    }


    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> PostEdit(int id, [Bind("NoteID, NoteDate, Title, Note, Exercisesm, Owner, OwnerId")] Notes note)
    {
        if (id != note.NoteID)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _db.Update(note);
                await _db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!NoteExists(note.NoteID))
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
        return View(note);
    }


    [HttpGet]
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null || _db.Notes == null)
        {
            return NotFound();
        }

        var note = await _db.Notes
            .FirstOrDefaultAsync(m => m.NoteID == id);
        if (note == null)
        {
            return NotFound();
        }

        return View(note);
    }


    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult PostDelete(int? id)
    {
        var note = _db.Notes.Find(id);

        if (note == null)
        {
            return NotFound();
        }
        _db.Notes.Remove(note);
        _db.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }


    private bool NoteExists(int id)
    {
        return (_db.Notes?.Any(e => e.NoteID == id)).GetValueOrDefault();
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

