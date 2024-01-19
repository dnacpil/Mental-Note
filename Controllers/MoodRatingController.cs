using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using MentalNote.Data;
using MentalNote.Models;
using MentalNote.Services;

namespace MentalNote.Controllers;

[Route("api/[controller]/[action]")]
public class MoodRatingController : Controller
{
    private readonly MentalNoteDbContext _db;
    private readonly UserManager<IdentityUser> _userManager;
    private readonly ReminderService _reminderService;

    public MoodRatingController(UserManager<IdentityUser> userManager, MentalNoteDbContext context, ReminderService reminderService)
    {
        _userManager = userManager;
        _db = context;
        _reminderService = reminderService;
    }
    [HttpGet]
    /* public IActionResult Index()
    {

        return View();
    } */

    [HttpGet]
    public IActionResult Create()
    {

        return RedirectToAction("Index", controllerName: "Home");
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

            _reminderService.CheckMoodAndSendReminder(currentUser.Id);

            return RedirectToAction("Index", controllerName: "Home");
        }

        return View(moodRating);
    }

    /* public IActionResult RedirectToRemindersView()
    {
        return View();
    } */
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
}