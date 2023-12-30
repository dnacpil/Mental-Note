using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using MentalNote.Data;
using MentalNote.Models;


namespace MentalNote.Controllers;

[Route("api/[controller]/[action]")]
public class MoodRatingController : Controller
{
    private readonly MentalNoteDbContext _db;
    private readonly UserManager<IdentityUser> _userManager;

    public MoodRatingController(UserManager<IdentityUser> userManager, MentalNoteDbContext context)
    {
        _userManager = userManager;
        _db = context;
    }
    [HttpGet]
    public IActionResult Index()
    {

        return View();
    }

    [HttpPost]
    public async Task<ActionResult<IEnumerable<MoodRating>>> Create([Bind("MoodRatingID, RatingDate, Emoji, Rating, MoodNote, Owner, OwnerId")] MoodRating moodRating)
    {
        if (ModelState.IsValid)
        {
            _db.Add(moodRating);
            await _db.SaveChangesAsync();
            RedirectToAction(nameof(Index));

        }
       return View(moodRating);

    }

    /* public async Task<IActionResult> SubmitMood(MoodRating moodRating)
    {
        try
        {
            moodRating.OwnerId = _userManager.GetUserId(User);
            moodRating.RatingDate = DateTime.Now;

            _db.Add(moodRating);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
            
        }
        catch (Exception)
        {
            // Log the exception or handle it appropriately
            return StatusCode(500, "Internal Server Error");
        }
    } */



}