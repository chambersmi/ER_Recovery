using ER_Recovery.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ER_Recovery.Web.Pages.AA
{
    public class MeetingsModel : PageModel
    {
        public List<Meeting>? AllMeetings { get; set; }
        public List<Meeting>? TodaysMeetings { get; set; }
        public List<Meeting>? OtherMeetings { get; set; }

        public void OnGet()
        {
            AllMeetings = GetSampleMeetings();

            var today = DateTime.Today.DayOfWeek;

            TodaysMeetings = AllMeetings.Where(m => m.Day == today).ToList();

            OtherMeetings = AllMeetings.Where(m => m.Day != today).OrderBy(m => m.Day).ToList();
        }

        private List<Meeting> GetSampleMeetings()
        {
            return new List<Meeting>
        {
            new Meeting { Day = DayOfWeek.Monday, Time = "6:00 PM", Description = "Downtown AA Group" },
            new Meeting { Day = DayOfWeek.Tuesday, Time = "7:00 PM", Description = "Sunset Sobriety" },
            new Meeting { Day = DayOfWeek.Wednesday, Time = "12:00 PM", Description = "Lunch Break Group" },
            new Meeting { Day = DayOfWeek.Thursday, Time = "6:30 PM", Description = "Evening Serenity" },
            new Meeting { Day = DayOfWeek.Friday, Time = "8:00 PM", Description = "End of Week Reflections" },
            new Meeting { Day = DayOfWeek.Saturday, Time = "10:00 AM", Description = "Weekend Warriors" },
            new Meeting { Day = DayOfWeek.Sunday, Time = "5:00 PM", Description = "Spiritual Sundays" },
        };
        }
    }
}
