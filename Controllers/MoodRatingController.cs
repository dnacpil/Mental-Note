using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using MentalNote.Data;
using MentalNote.Models;

namespace MentalNote.Controllers;

[Route("api/[controller]/[action]")]
public class MoodRatingController : Controller
{
    private readonly MentalNoteDbContext _db;

    public MoodRatingController(MentalNoteDbContext context)
    {
        _db = context;
    }

/* [HttpPost]
public IActionResult SubmitMood(MoodRating moodRating)
{
    moodRating.OwnerId = userManager.GetUserId(User);

    moodRating.RatingDate = DateTime.Now;

    dbContext.MoodRatings.Add(moodRating);
    dbContext.SaveChanges();

    return RedirectToAction("Index");*/


}