using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using MentalNote.Data;
using MentalNote.Models;

namespace MentalNote.Services;

public class ReminderService{
    private readonly MentalNoteDbContext _db;
    private readonly UserManager<IdentityUser> _userManager;

    public ReminderService(UserManager<IdentityUser> userManager, MentalNoteDbContext context)
    {
        _userManager = userManager;
        _db = context;
    }

    public void CheckMoodAndSendReminder(string ownerId)
    {

        var moodHistory = _db.MoodRating
            .Where(m => m.OwnerId == ownerId)
            .OrderByDescending(m => m.Date)
            .Take(50)
            .ToList();

        var daysLowMood = moodHistory.Count(mood => mood.Rating <= 5);

        if (daysLowMood >= 1)
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