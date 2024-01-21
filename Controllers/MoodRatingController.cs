using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using MentalNote.Data;
using MentalNote.Models;
using MentalNote.Services;

namespace MentalNote.Controllers;

[Authorize]
[Route("api/[controller]/[action]")]
public class MoodRatingController : Controller
{
    private readonly MentalNoteDbContext _db;
    private readonly UserManager<IdentityUser> _userManager;
    //private readonly ReminderService _reminderService;

    public MoodRatingController(UserManager<IdentityUser> userManager, MentalNoteDbContext context)
    {
        _userManager = userManager;
        _db = context;
        //, ReminderService reminderService _reminderService = reminderService;
    }
    public IActionResult Index()
    {
        if (_db.MoodRating == null)
            {
                return NotFound();
            }
        return View();
    }

    [HttpGet]
    public IActionResult Create()
    {

        return View();
    }

    [HttpPost]
    public async Task<ActionResult<IEnumerable<MoodRating>>> Create([Bind("MoodRatingID, RatingDate, Emoji, Rating, MoodNote, Owner, OwnerId")] MoodRating moodRating)
    {
        if (ModelState.IsValid)
        {
            IdentityUser currentUser = await _userManager.GetUserAsync(User);

            moodRating.Owner = currentUser;
            moodRating.OwnerId = currentUser.Id;

            _db.Add(moodRating);
            await _db.SaveChangesAsync();

            //_reminderService.CheckMoodAndSendReminder(currentUser.Id);

            return RedirectToAction(nameof(Index));
        }

        return View(moodRating);
    }

    //To get mood data for dashboard
    public IActionResult GetMoodData(string userId)
    {
        var moodData = _db.MoodRating
            .Where(m => m.OwnerId == userId)
            .OrderBy(m => m.Date)
            .Select(m => new { m.Date, m.Rating })
            .ToList();

        // Format data for Syncfusion Spline chart
        var splineChartData = moodData.Select(m => new { x = m.Date, y = m.Rating }).ToList();
        
        ViewBag.SplineChartData = splineChartData;

        return View();
    }

    //Reminder feature
    public void CheckMoodAndSendReminder(string ownerId)
    {

        var moodHistory = _db.MoodRating
            .Where(m => m.OwnerId == ownerId)
            .OrderByDescending(m => m.Date)
            .Take(50)
            .ToList();

        var daysLowMood = moodHistory.Count(mood => mood.Rating <= 5);

        if (daysLowMood <= 1)
        {
            ShowReminderToUser("It might be time to seek for help again.");
        }
    }

    private void ShowReminderToUser(string message)
    {

        ReminderMessage = message;
    }

    public static string? ReminderMessage { get; private set; }
}