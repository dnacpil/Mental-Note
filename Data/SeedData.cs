using Microsoft.EntityFrameworkCore;
using MentalNote.Models;

namespace MentalNote.Data;
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
                new MoodRating { Date = DateTime.Parse("1/2/2024 22:00:00"), Rating = 10, MoodNote = "Feeling great today!", OwnerId = "98d6a7c5-0508-4f00-bed6-91824cf564e9" },
                new MoodRating { Date = DateTime.Parse("1/4/2024 18:00:00"), Rating = 5, MoodNote = "Not my best day, but hanging in there.", OwnerId = "98d6a7c5-0508-4f00-bed6-91824cf564e9" },
                new MoodRating { Date = DateTime.Parse("1/5/2024 12:00:00"), Rating = 5, MoodNote = "Maybe it's just the weather affecting my mood", OwnerId = "98d6a7c5-0508-4f00-bed6-91824cf564e9" },
                new MoodRating { Date = DateTime.Parse("1/6/2024 8:00:00"), Rating = 8, MoodNote = "Had a good workout, feeling energized.", OwnerId = "98d6a7c5-0508-4f00-bed6-91824cf564e9" },
                new MoodRating { Date = DateTime.Parse("1/8/2024 21:00:00"), Rating = 4, MoodNote = "Stressed about  deadlines.", OwnerId = "98d6a7c5-0508-4f00-bed6-91824cf564e9" },
                new MoodRating { Date = DateTime.Parse("1/9/2024 18:00:00"), Rating = 4, MoodNote = "Worried about the company lay-offs - what if I'm next.", OwnerId = "98d6a7c5-0508-4f00-bed6-91824cf564e9" },
                new MoodRating { Date = DateTime.Parse("1/10/2024 22:00:00"), Rating = 10, MoodNote = "Spent quality time with friends; Feeling happy and grateful.", OwnerId = "98d6a7c5-0508-4f00-bed6-91824cf564e9" },
                new MoodRating { Date = DateTime.Parse("1/11/2024 10:00:00"), Rating = 8, MoodNote = "Had a great meditation session.", OwnerId = "98d6a7c5-0508-4f00-bed6-91824cf564e9" },
                new MoodRating { Date = DateTime.Parse("1/12/2024 14:00:00"), Rating = 10, MoodNote = "Feeling loved after getting a surprise gift.", OwnerId = "98d6a7c5-0508-4f00-bed6-91824cf564e9" },
                new MoodRating { Date = DateTime.Parse("1/13/2024 13:00:00"), Rating = 6, MoodNote = "Dealing with a minor setback, but staying positive.", OwnerId = "98d6a7c5-0508-4f00-bed6-91824cf564e9" },
                new MoodRating { Date = DateTime.Parse("1/14/2024 17:00:00"), Rating = 9, MoodNote = "Went for a little short hike, feeling more connected to surroundings, and refreshed as I wasn't looking at a screen.", OwnerId = "98d6a7c5-0508-4f00-bed6-91824cf564e9" },
                new MoodRating { Date = DateTime.Parse("1/15/2024 11:00:00"), Rating = 1, MoodNote = "Coping with the loss of a loved one.", OwnerId = "98d6a7c5-0508-4f00-bed6-91824cf564e9" },
                new MoodRating { Date = DateTime.Parse("1/16/2024 10:00:00"), Rating = 3, MoodNote = "Facing financial difficulties and uncertainty.", OwnerId = "98d6a7c5-0508-4f00-bed6-91824cf564e9" },
                new MoodRating { Date = DateTime.Parse("1/17/2024 15:00:00"), Rating = 4, MoodNote = "Suffering from a lack of sleep for a week now", OwnerId = "98d6a7c5-0508-4f00-bed6-91824cf564e9" },
                new MoodRating { Date = DateTime.Parse("1/18/2024 20:00:00"), Rating = 3, MoodNote = "Experiencing a setback in personal goals.", OwnerId = "98d6a7c5-0508-4f00-bed6-91824cf564e9" }
            );
            context.SaveChanges();
        }
    }
}