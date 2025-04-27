using ER_Recovery.Domains.Enums;

namespace ER_Recovery.Domains.Entities

{
    public class Meeting
    {
        public int Id { get; set; }
        public MeetingDay? Day { get; set; }
        public TimeSpan Time { get; set; }        
        public string? Description { get; set; }
        public string? Location { get; set; }
        public string? Address { get; set; }
        public string? City { get; set; } 
        public string? State { get; set; }        
        public bool OpenMeeting { get; set; }
        public MeetingType MeetingType { get; set; }
    }
}
