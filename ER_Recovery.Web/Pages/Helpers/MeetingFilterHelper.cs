using ER_Recovery.Domains.Enums;
using ER_Recovery.Domains.Models.DTOs;

namespace ER_Recovery.Web.Pages.Helpers
{
    public static class MeetingFilterHelper
    {
        public static List<MeetingDTO> GetTodaysMeetings(List<MeetingDTO> meetings, MeetingType meetingType)
        {
            var currentDay = (MeetingDay)DateTime.Now.DayOfWeek;
            return meetings
                .Where(m => m.Day == currentDay && m.MeetingType == meetingType)
                .OrderBy(m => m.Time)
                .ToList();
        }

        public static List<MeetingDTO> GetOtherMeetings(List<MeetingDTO> meetings, MeetingType meetingType)
        {
            var currentDay = (MeetingDay)DateTime.Now.DayOfWeek;
            return meetings
                .Where(m => m.Day != currentDay && m.MeetingType == meetingType)
                .OrderBy(m => m.Day)
                .ThenBy(m => m.Time)
                .ToList();
        }
    }
}