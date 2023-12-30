using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using MentalNote.Data;
using MentalNote.Models;

namespace MentalNote.Controllers;

[Authorize]
[Route("api/[controller]/[action]")]
public class JournalEntryController : Controller
{
    private readonly MentalNoteDbContext _db;
    public JournalEntryController(MentalNoteDbContext context)
    {
        _db = context;
    }

    // GET: Index
    [HttpGet]
    public async Task<ActionResult<IEnumerable<JournalEntry>>> Index(string? sortOrder)
    {

        if (_db.JournalEntry == null)
        {
            return NotFound();
        }

        ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
        ViewData["CategorySortParm"] = sortOrder == "Title" ? "title_desc" : "Title";

        var entries = from e in _db.JournalEntry
                      select e;

        switch (sortOrder)
        {
            case "name_desc":
                entries = entries.OrderByDescending(e => e.Title);
                break;

            default:
                entries = entries.OrderBy(e => e.EntryDate);
                break;
        }

        return View(await entries.AsNoTracking().ToListAsync());
    }

    // APIs to create, edit and delete journal entries
    [HttpGet]
    public IActionResult Create()
    {

        return View();
    }

    [HttpPost]
    public async Task<ActionResult<IEnumerable<JournalEntry>>> Create([Bind("JournalEntryID, EntryDate, Title, JournalContent, Owner, OwnerId")] JournalEntry entry)
    {
        if (ModelState.IsValid)
        {
            _db.Add(entry);
            await _db.SaveChangesAsync();
            RedirectToAction(nameof(Index));

        }
        return View(entry);

    }

    //GET & POST: Edit an item
    [HttpGet]
    public async Task<ActionResult<IEnumerable<JournalEntry>>> Edit(int? id)
    {
        if (id == null || _db.JournalEntry == null)
        {
            return NotFound();
        }

        var entry = await _db.JournalEntry.FindAsync(id);
        if (entry == null)
        {
            return NotFound();
        }
        return View(entry);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> PostEdit(int id, [Bind("JournalEntryID, EntryDate, Title, JournalContent, Owner, OwnerId")] JournalEntry entry)
    {
        if (id != entry.JournalEntryID)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _db.Update(entry);
                await _db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EntryExists(entry.JournalEntryID))
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
        return View(entry);
    }

    //GET & POST: Delete items
    [HttpGet]
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null || _db.JournalEntry == null)
        {
            return NotFound();
        }

        var entry = await _db.JournalEntry
            .FirstOrDefaultAsync(m => m.JournalEntryID == id);
        if (entry == null)
        {
            return NotFound();
        }

        return View(entry);
    }


    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult PostDelete(int? id)
    {
        var entry = _db.JournalEntry.Find(id);

        if (entry == null)
        {
            return NotFound();
        }
        _db.JournalEntry.Remove(entry);
        _db.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }


    private bool EntryExists(int id)
    {
        return (_db.JournalEntry?.Any(e => e.JournalEntryID == id)).GetValueOrDefault();
    }


    //To show the full content of journal entry, allowing the user to read them

    [HttpGet]
    [Route("api/JournalEntry/Content/{id}")]
    public async Task<IActionResult> Content(int? id)
    {

        if (id == null || _db.JournalEntry == null)
        {
            return NotFound();
        }

        var notes = await _db.JournalEntry
            .FirstOrDefaultAsync(m => m.JournalEntryID == id);
        if (notes == null)
        {
            return NotFound();
        }

        return View(notes);

    }
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


