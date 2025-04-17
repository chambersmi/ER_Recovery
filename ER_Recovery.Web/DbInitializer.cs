using ER_Recovery.Infrastructure.Data;
using ER_Recovery.Web.Models;
using Microsoft.EntityFrameworkCore;

namespace ER_Recovery.Web
{
    public class DbInitializer
    {

        public static void InitDb(WebApplication app)
        {
            using var scope = app.Services.CreateScope();

            var context = scope.ServiceProvider.GetRequiredService<AppDbContext>() ?? throw new InvalidOperationException("Failed to retrieve context class.");

            SeedData(context);
        }

        public static void SeedData(AppDbContext context)
        {

            context.Database.Migrate();


            if (context.Meetings.Any())
            {
                return;
            }




            var meetings = new List<Meeting>
            {
                new Meeting
                {
                    Id = 1,
                    Day = DayOfWeek.Sunday,
                    Time = "8:00 PM",
                    Location = "Church of Nazarene",
                    Description = "AA Topic Meeting",
                    Address = "2225 S. Michigan Rd",
                    City = "Eaton Rapids",
                    State = "MI",
                    OpenMeeting = true
                },

                new Meeting
                {
                    Id = 2,
                    Day = DayOfWeek.Monday,
                    Time = "5:00 PM",
                    Location = "Church of Nazarene",
                    Description = "AA 12 and 12",
                    Address = "2225 S. Michigan Rd",
                    City = "Eaton Rapids",
                    State = "MI",
                    OpenMeeting = false
                },

                new Meeting
                {
                    Id = 3,
                    Day = DayOfWeek.Monday,
                    Time = "7:00 PM",
                    Location = "Eaton Rapids Assembly of God",
                    Description = "",
                    Address = "2764 S. Michigan Rd",
                    City = "Eaton Rapids",
                    State = "MI",
                    OpenMeeting = false
                },

                new Meeting
                {
                    Id = 4,
                    Day = DayOfWeek.Tuesday,
                    Time = "5:00 PM",
                    Location = "Church of Nazarene",
                    Description = "AA Beginners Meeting",
                    Address = "2225 S. Michigan Rd",
                    City = "Eaton Rapids",
                    State = "MI",
                    OpenMeeting = false
                },

                new Meeting
                {
                    Id = 5,
                    Day = DayOfWeek.Tuesday,
                    Time = "7:00 PM",
                    Location = "Eaton Rapids Assembly of God",
                    Description = "",
                    Address = "2764 S. Michigan Rd",
                    City = "Eaton Rapids",
                    State = "MI",
                    OpenMeeting = false
                },

                new Meeting
                {
                    Id = 6,
                    Day = DayOfWeek.Wednesday,
                    Time = "5:00 PM",
                    Location = "Church of Nazarene",
                    Description = "AA Topic Meeting",
                    Address = "2225 S. Michigan Rd",
                    City = "Eaton Rapids",
                    State = "MI",
                    OpenMeeting = false
                },

                new Meeting
                {
                    Id = 7,
                    Day = DayOfWeek.Thursday,
                    Time = "6:30 PM",
                    Location = "Church of Nazarene",
                    Description = "Mens Group (Men Only)",
                    Address = "2225 S. Michigan Rd",
                    City = "Eaton Rapids",
                    State = "MI",
                    OpenMeeting = false
                },

                new Meeting
                {
                    Id = 8,
                    Day = DayOfWeek.Friday,
                    Time = "7:00 PM",
                    Location = "Eaton Rapids Assembly of God",
                    Description = "",
                    Address = "2225 S. Michigan Rd",
                    City = "Eaton Rapids",
                    State = "MI",
                    OpenMeeting = false
                },

                new Meeting
                {
                    Id = 9,
                    Day = DayOfWeek.Saturday,
                    Time = "9:00 AM",
                    Location = "Church of Nazarene",
                    Description = "AA Big Book Meeting",
                    Address = "2764 S. Michigan Rd",
                    City = "Eaton Rapids",
                    State = "MI",
                    OpenMeeting = false
                }

            };

            context.Meetings.AddRange(meetings);
            context.SaveChanges();
        }
    }
}
