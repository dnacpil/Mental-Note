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
    public async Task<ActionResult<IEnumerable<MoodRating>>> Create([Bind("MoodRatingID, RatingDate, Emoji, Rating, MoodNote, OwnerId")] MoodRating moodRating)
    {
        if (ModelState.IsValid)
        {
            IdentityUser currentUser = await _userManager.GetUserAsync(User);

            //moodRating.Owner = currentUser;
            moodRating.OwnerId = currentUser.Id;

            _db.Add(moodRating);
            await _db.SaveChangesAsync();
            TempData["success"] = "Saved";
            //_reminderService.CheckMoodAndSendReminder(currentUser.Id);

            return RedirectToAction(nameof(Index));
        }

        return View(moodRating);
    }

    //Dashboard
    /* public IActionResult Dashboard()
    {

        return View();
    }  */
    //To get mood data for chart
    public async Task<ActionResult> Dashboard(string userId)
    {
       /*  DateTime startDate = DateTime.Today.AddDays(-30); // Change to -30 to get the last 30 days
        DateTime endDate = DateTime.Today; 
        && i.Date >= startDate && i.Date <= endDate*/


        /* List<MoodRating> MoodRatings = await _db.MoodRating
            .Where(i => i.OwnerId == userId)
            .ToListAsync();

        List<LineChartData> MoodSummary = MoodRatings
            .GroupBy(i => i.Date.Date)
        .Select(k => new LineChartData()
        {
            day = k.Key,
            moodRating = (int)k.Average(r => r.Rating) // Calculate the average rating for the day
        })
        .ToList();

    DateTime[] last30Days = Enumerable.Range(0, 30)
        .Select(i => startDate.AddDays(i))
        .ToArray();

    ViewBag.dataSource = from day in last30Days
                         join moodData in MoodSummary
                         on day equals moodData.day into joined
                         from moodData in joined.DefaultIfEmpty()
                         select new
                         {
                             day = day.ToString("dd-MMM"),
                             moodRating = moodData?.moodRating ?? 0
                         };

    return View(); */
    DateTime startDate = DateTime.Today.AddDays(-30);
    DateTime endDate = DateTime.Today;

    List<MoodRating> moodRatings = await _db.MoodRating
        .Where(i => i.OwnerId == userId && i.Date >= startDate && i.Date <= endDate)
        .ToListAsync();

    List<LineChartData> moodSummary = moodRatings
        .GroupBy(i => i.Date.Date)
        .Select(k => new LineChartData()
        {
            day = k.Key,
            moodRating = (int)k.Average(r => r.Rating)
        })
        .ToList();

    ViewBag.dataSource = moodSummary
        .OrderBy(entry => entry.day)
        .Select(entry => new
        {
            day = entry.day.ToString("dd-MMM"),
            moodRating = entry.moodRating
        });

    return View();

        /*  */
    }

    public class LineChartData
    {
        public DateTime day;
        public int moodRating;
    }
    //Reminder feature
    public IActionResult CheckMoodAndSendReminder(string ownerId)
    {
        var MoodHistory = _db.MoodRating
            .Where(m => m.OwnerId == ownerId)
            .OrderByDescending(m => m.Date)
            .Take(50)
            .ToList();

        var daysLowMood = MoodHistory.Count(mood => mood.Rating <= 5);

        if (daysLowMood <= 1)
        {
            TempData["message"] = "It might be time to seek for help again.";
            
        }
        return View();
    }

    /* private void ShowReminderToUser(string message)
    {

        ReminderMessage = message;
    } */

    //public static string? ReminderMessage { get; private set; }
}