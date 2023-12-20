using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
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

[HttpPost]
    public IActionResult SaveMoodRating(int rating)
    {

        string moodEmoji = GetMoodEmoji(rating);

        MoodRating moodRating = new MoodRating
        {
            //RatingDate = DateTime.Now,
            Rating = rating,
            MoodNote = moodEmoji,
        };
        _db.MoodRating.Add(moodRating);
        _db.SaveChanges();

        return RedirectToAction(nameof(Index));
    }

    private string GetMoodEmoji(int rating)
    {
        switch (rating)
        {
            case 0:
                return "😡"; // Angry emoji
            case 1:
                return "😠"; // Disagree emoji
            case 2:
                return "😐"; // Neutral emoji
            case 3:
                return "😊"; // Agree emoji
            default:
                return "😃"; // Happy emoji
        }
    }
}