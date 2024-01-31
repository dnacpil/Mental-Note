using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using MentalNote.Data;
using MentalNote.Models;
using MentalNote.Services;
using System.Globalization;
using Microsoft.EntityFrameworkCore;
using Syncfusion.EJ2.Inputs;
using Syncfusion.EJ2.Linq;

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
    public async Task<IActionResult> IndexAsync(MoodRating moodRating)
    {
        IdentityUser currentUser = await _userManager.GetUserAsync(User);

        moodRating.Owner = currentUser;
        moodRating.OwnerId = currentUser.Id;

        if (_db.MoodRating == null)
        {
            return NotFound();
        }
        CheckMoodAndSendReminder(currentUser.Id);
        return View();
    }

    //To add a rating
    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public async Task<ActionResult<IEnumerable<MoodRating>>> Create([Bind("MoodRatingID, RatingDate, Emoji, Rating, MoodNote, OwnerId")] MoodRating moodRating)
    {
        if (ModelState.IsValid)
        {
            IdentityUser currentUser = await _userManager.GetUserAsync(User);

            moodRating.Owner = currentUser;
            moodRating.OwnerId = currentUser.Id;

            _db.Add(moodRating);
            await _db.SaveChangesAsync();
            TempData["success"] = "Saved";
            //_reminderService.CheckMoodAndSendReminder(currentUser.Id);

            return RedirectToAction(nameof(IndexAsync));
        }

        return View(moodRating);
    }

    //Reminder feature
    public IActionResult CheckMoodAndSendReminder(string ownerId)
    {
        var MoodHistory = _db.MoodRating
        .Where(m => m.OwnerId == ownerId)
        .OrderByDescending(m => m.Date)
        .Take(40)
        .ToList();

        var daysLowMood = MoodHistory.Count(mood => mood.Rating <= 5);

        if (daysLowMood >= 30)
        {
            TempData["message"] = "It might be time to get some help again.";
        }

        return Json(new { success = true, message = "Reminder checked." });
    }


    //Dashboard
    public async Task<ActionResult> Dashboard(string userId)
    {
        DateTime StartDate = DateTime.Today.AddDays(-30);
        DateTime EndDate = DateTime.Today;

        List<MoodRating> moodRatings = await _db.MoodRating
            .Where(i => i.OwnerId == userId && i.Date >= StartDate && i.Date <= EndDate)
            .ToListAsync();

        List<SplineChartData> LowMoodSummary = moodRatings
            .Where(r => r.Rating < 5)
            .GroupBy(i => i.Date.Date)
            .Select(k => new SplineChartData()
            {
                day = k.Key,
                moodRating = (int)k.Average(r => r.Rating)
            })
            .ToList();

        List<SplineChartData> HighMoodSummary = moodRatings
            .Where(r => r.Rating >= 6 && r.Rating <= 10)
            .GroupBy(i => i.Date.Date)
            .Select(k => new SplineChartData()
            {
                day = k.Key,
                moodRating = (int)k.Average(r => r.Rating)
            })
            .ToList();

        DateTime[] Last30Days = Enumerable.Range(0, 30)
        .Select(i => StartDate.AddDays(i))
        .ToArray();

        ViewBag.SplineChartData = from day in Last30Days
                                  join low in LowMoodSummary on day equals low.day into lowGroup
                                  join high in HighMoodSummary on day equals high.day into highGroup
                                  select new
                                  {
                                      day = day.ToString("dd-MMM"),
                                      lowMoodRating = lowGroup.Any() ? lowGroup.First().moodRating : 0,
                                      highMoodRating = highGroup.Any() ? highGroup.First().moodRating : 0,
                                  };

        return View();

    }
    public class SplineChartData
    {
        public DateTime day;
        public int moodRating;
    }
}

