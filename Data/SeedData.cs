using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MentalNote.Data;
using System;
using System.Linq;
using Syncfusion.EJ2.Inputs;

namespace MentalNote.Models;
public static class SeedData
{
    public static void Initialize(IServiceProvider serviceProvider)
    {
        using (var context = new MentalNoteDbContext(
            serviceProvider.GetRequiredService<DbContextOptions<MentalNoteDbContext>>()))
        {

            if (context.MoodRating.Any())
            {
                return;
            }
            context.MoodRating.AddRange(
                new MoodRating { Date = DateTime.Parse("2024-01-02 22:00"), Rating = 10, MoodNote = "Feeling great today!", OwnerId = "98d6a7c5-0508-4f00-bed6-91824cf564e9" },
                new MoodRating { Date = DateTime.Parse("2024-01-04 18:00"), Rating = 5, MoodNote = "Not my best day, but hanging in there.", OwnerId = "98d6a7c5-0508-4f00-bed6-91824cf564e9" },
                new MoodRating { Date = DateTime.Parse("2024-01-05 12:00"), Rating = 5, MoodNote = "Maybe it's just the weather affecting my mood", OwnerId = "98d6a7c5-0508-4f00-bed6-91824cf564e9" },
                new MoodRating { Date = DateTime.Parse("2024-01-06 08:00"), Rating = 8, MoodNote = "Had a good workout, feeling energized.", OwnerId = "98d6a7c5-0508-4f00-bed6-91824cf564e9" },
                new MoodRating { Date = DateTime.Parse("2024-01-08 21:00"), Rating = 4, MoodNote = "Stressed about  deadlines.", OwnerId = "98d6a7c5-0508-4f00-bed6-91824cf564e9" },
                new MoodRating { Date = DateTime.Parse("2024-01-09 18:00:"), Rating = 4, MoodNote = "Worried about the company lay-offs - what if I'm next.", OwnerId = "98d6a7c5-0508-4f00-bed6-91824cf564e9" },
                new MoodRating { Date = DateTime.Parse("2024-01-10 22:00:"), Rating = 10, MoodNote = "Spent quality time with friends; Feeling happy and grateful.", OwnerId = "98d6a7c5-0508-4f00-bed6-91824cf564e9" },
                new MoodRating { Date = DateTime.Parse("2024-01-11 10:00:"), Rating = 8, MoodNote = "Had a great meditation session.", OwnerId = "98d6a7c5-0508-4f00-bed6-91824cf564e9" },
                new MoodRating { Date = DateTime.Parse("2024-01-12 14:00:"), Rating = 10, MoodNote = "Feeling loved after getting a surprise gift.", OwnerId = "98d6a7c5-0508-4f00-bed6-91824cf564e9" },
                new MoodRating { Date = DateTime.Parse("2024-01-13 13:00:"), Rating = 6, MoodNote = "Dealing with a minor setback, but staying positive.", OwnerId = "98d6a7c5-0508-4f00-bed6-91824cf564e9" },
                new MoodRating { Date = DateTime.Parse("2024-01-14 17:00:"), Rating = 9, MoodNote = "Went for a little short hike, feeling more connected to surroundings, and refreshed as I wasn't looking at a screen.", OwnerId = "98d6a7c5-0508-4f00-bed6-91824cf564e9" },
                new MoodRating { Date = DateTime.Parse("2024-01-15 11:00:"), Rating = 1, MoodNote = "Coping with the loss of a loved one.", OwnerId = "98d6a7c5-0508-4f00-bed6-91824cf564e9" },
                new MoodRating { Date = DateTime.Parse("2024-01-16 10:00:"), Rating = 3, MoodNote = "Facing financial difficulties and uncertainty.", OwnerId = "98d6a7c5-0508-4f00-bed6-91824cf564e9" },
                new MoodRating { Date = DateTime.Parse("2024-01-17 15:00:"), Rating = 4, MoodNote = "Suffering from a lack of sleep for a week now", OwnerId = "98d6a7c5-0508-4f00-bed6-91824cf564e9" },
                new MoodRating { Date = DateTime.Parse("2024-01-18 20:00:"), Rating = 3, MoodNote = "Experiencing a setback in personal goals.", OwnerId = "98d6a7c5-0508-4f00-bed6-91824cf564e9" }
            );
            context.SaveChanges();
        }
    }
}