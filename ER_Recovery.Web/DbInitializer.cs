using ER_Recovery.Domains.Entities;
using ER_Recovery.Domains.Enums;
using ER_Recovery.Infrastructure;
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

            if(context.MessageBoard.Any())
            {
                return;
            }

            var messageBoard = new List<MessageBoard>
            {
                new MessageBoard
                {
                    MessageId = 1,
                    UserId = "123408123409",
                    Title = "Test Message",
                    Content = "Some gibberish here",
                    CreatedTime = DateTime.UtcNow,                    
                }
            };



            var meetings = new List<Meeting>
            {
                new Meeting
                {
                    Id = 1,
                    Day = MeetingDay.Sunday,
                    Time = new TimeSpan(20, 0, 0),
                    Location = "Church of Nazarene",
                    Description = "AA Topic Meeting",
                    Address = "2225 S. Michigan Rd",
                    City = "Eaton Rapids",
                    State = "MI",
                    OpenMeeting = true,
                    MeetingType = MeetingType.AA
                },

                new Meeting
                {
                    Id = 2,
                    Day = MeetingDay.Monday,
                    Time = new TimeSpan(17, 0, 0),
                    Location = "Church of Nazarene",
                    Description = "AA 12 and 12",
                    Address = "2225 S. Michigan Rd",
                    City = "Eaton Rapids",
                    State = "MI",
                    OpenMeeting = false,
                    MeetingType = MeetingType.AA
                },

                new Meeting
                {
                    Id = 3,
                    Day = MeetingDay.Monday,
                    Time = new TimeSpan(19, 0, 0),
                    Location = "Eaton Rapids Assembly of God",
                    Description = "",
                    Address = "2764 S. Michigan Rd",
                    City = "Eaton Rapids",
                    State = "MI",
                    OpenMeeting = false,
                    MeetingType = MeetingType.AA
                },

                new Meeting
                {
                    Id = 4,
                    Day = MeetingDay.Tuesday,
                    Time = new TimeSpan(17, 0, 0),
                    Location = "Church of Nazarene",
                    Description = "AA Beginners Meeting",
                    Address = "2225 S. Michigan Rd",
                    City = "Eaton Rapids",
                    State = "MI",
                    OpenMeeting = false,
                    MeetingType = MeetingType.AA
                },

                new Meeting
                {
                    Id = 5,
                    Day = MeetingDay.Tuesday,
                    Time = new TimeSpan(19, 0, 0),
                    Location = "Eaton Rapids Assembly of God",
                    Description = "",
                    Address = "2764 S. Michigan Rd",
                    City = "Eaton Rapids",
                    State = "MI",
                    OpenMeeting = false,
                    MeetingType = MeetingType.AA
                },

                new Meeting
                {
                    Id = 6,
                    Day = MeetingDay.Wednesday,
                    Time = new TimeSpan(17, 0, 0),
                    Location = "Church of Nazarene",
                    Description = "AA Topic Meeting",
                    Address = "2225 S. Michigan Rd",
                    City = "Eaton Rapids",
                    State = "MI",
                    OpenMeeting = false,
                    MeetingType = MeetingType.AA
                },

                new Meeting
                {
                    Id = 7,
                    Day = MeetingDay.Thursday,
                    Time = new TimeSpan(18, 30, 0),
                    Location = "Church of Nazarene",
                    Description = "Mens Group (Men Only)",
                    Address = "2225 S. Michigan Rd",
                    City = "Eaton Rapids",
                    State = "MI",
                    OpenMeeting = false,
                    MeetingType = MeetingType.AA
                },

                new Meeting
                {
                    Id = 8,
                    Day = MeetingDay.Friday,
                    Time = new TimeSpan(19, 0, 0),
                    Location = "Eaton Rapids Assembly of God",
                    Description = "",
                    Address = "2225 S. Michigan Rd",
                    City = "Eaton Rapids",
                    State = "MI",
                    OpenMeeting = false,
                    MeetingType = MeetingType.AA
                },

                new Meeting
                {
                    Id = 9,
                    Day = MeetingDay.Saturday,
                    Time = new TimeSpan(9, 0, 0),
                    Location = "Church of Nazarene",
                    Description = "AA Big Book Meeting",
                    Address = "2764 S. Michigan Rd",
                    City = "Eaton Rapids",
                    State = "MI",
                    OpenMeeting = false,
                    MeetingType = MeetingType.AA
                }

            };

            context.Meetings.AddRange(meetings);
            context.SaveChanges();

            context.MessageBoard.AddRange(messageBoard);
            context.SaveChanges();
        }
    }
}
