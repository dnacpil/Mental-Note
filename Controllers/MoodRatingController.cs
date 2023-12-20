using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using MentalNote.Data;
using MentalNote.Models;
namespace MentalNote.Controllers;
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
        // Assuming your Syncfusion component sends the selected mood as an integer.
        // The integer could represent the index or any other value that maps to your mood levels.

        // Map the rating value to a corresponding emoji or mood level as needed.
        string moodEmoji = GetMoodEmoji(rating);

        // Create a new MoodRating instance and populate its properties.
        MoodRating moodRating = new MoodRating
        {
            
            Rating = rating,
            MoodNote = moodEmoji,
            // Other properties as needed, such as OwnerId.
        };

        // Save the mood rating to the database.
        _db.MoodRating.Add(moodRating);
        _db.SaveChanges();

        // Redirect or return a response as needed.
        return RedirectToAction(nameof(Index)); // Redirect to home page, adjust as necessary.
    }

    private string GetMoodEmoji(int rating)
    {
        // Map the rating to the corresponding emoji or mood level based on your logic.
        // You can use a switch statement, if-else, or any other mapping mechanism.
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
