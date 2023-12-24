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

    [HttpPost]
    /* public IActionResult SubmitMood(MoodRating moodRating)
    {
        moodRating.OwnerId = userManager.GetUserId(User);

        moodRating.RatingDate = DateTime.Now;

        _dbContext.MoodRatings.Add(moodRating);
        _dbContext.SaveChanges();

        return RedirectToAction("Index"); 
    

} */
    public IActionResult SaveMoodRating(int rating)
    {
        // Assuming your Syncfusion component sends the selected mood as an integer.
        // The integer could represent the index or any other value that maps to your mood levels.

        // Map the rating value to a corresponding emoji or mood level as needed.
        string moodEmoji = GetMoodEmoji(rating);

        // Create a new MoodRating instance and populate its properties.
        MoodRating moodRating = new MoodRating
        {

            RatingDate = DateTime.Now,
            Rating = rating,
            MoodNote = moodEmoji
        };

        return RedirectToAction(nameof(Index));

    }
    private string GetMoodEmoji(int rating)
    {
        // Map the rating to the corresponding emoji or mood level based on your logic.
        // You can use a switch statement, if-else, or any other mapping mechanism.
        switch (rating)
        {
            case 0:
                return "&#128545"; // Angry emoji
            case 1:
                return "&#128577"; // Disagree emoji
            case 2:
                return "&#128528"; // Neutral emoji
            case 3:
                return "&#128578"; // Agree emoji
            default:
                return "😃"; // Happy emoji
        }
    }
}