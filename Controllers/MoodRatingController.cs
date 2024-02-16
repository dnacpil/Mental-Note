using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using MentalNote.Data;
using MentalNote.Models;
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


    public IActionResult List()
    {
        return View();
    }

    //To add a rating
    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public async Task<ActionResult<IEnumerable<MoodRating>>> Create([Bind("MoodRatingID, gDate, Rating, MoodNote, OwnerId")] MoodRating moodRating)
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
    public async Task<ActionResult> Dashboard(MoodRating moodRating)
    {
        IdentityUser currentUser = await _userManager.GetUserAsync(User);

        moodRating.Owner = currentUser;
        moodRating.OwnerId = currentUser.Id;

        List<MoodRating> MoodRatings = await _db.MoodRating
            .Where(i => i.OwnerId == currentUser.Id)
            .ToListAsync();

        // Data for Doughnut Chart: calculation of low mood (less than 5) and high mood (6 to 10)
        int lowMoodCount = MoodRatings.Count(rating => rating.Rating <= 5);
        int highMoodCount = MoodRatings.Count(rating => rating.Rating >= 6 && rating.Rating <= 10);

        double totalMoodCount = lowMoodCount + highMoodCount;
        double lowMoodPercentage = lowMoodCount / totalMoodCount * 100;
        double highMoodPercentage = highMoodCount / totalMoodCount * 100;

        List<DoughnutChartData> DoughnutChart = new List<DoughnutChartData>
            {
                new DoughnutChartData { MoodCategory = "Low Mood", Percentage = lowMoodPercentage },
                new DoughnutChartData { MoodCategory = "High Mood", Percentage = highMoodPercentage }
            };

        ViewBag.doughnutDataSource = DoughnutChart;

        //To display the mood ratings for the last 30 days 
        DateTime startDate = DateTime.Today.AddDays(-30);
        DateTime endDate = DateTime.Today;

        List<LineChartData> LineChart = MoodRatings.Select(rating => new LineChartData
        {
            Date = rating.Date.ToString("yyyy-MM-dd"),
            MoodRating = rating.Rating
        }).ToList();

        ViewBag.dataSource = LineChart;
        return View();

    }

    public class DoughnutChartData
    {
        public string MoodCategory { get; set; }
        public double Percentage { get; set; }
    }
    public class LineChartData
    {
        public string Date { get; set; }
        public int MoodRating { get; set; }
    }
}




