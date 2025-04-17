namespace ER_Recovery.Web.Models
{
    public class Meeting
    {
        public int Id { get; set; }
        public DayOfWeek Day { get; set; }
        public string? Time { get; set; }        
        public string? Description { get; set; }
        public string? Location { get; set; }
        public string? Address { get; set; }
        public string? City { get; set; } 
        public string? State { get; set; }        
        public bool OpenMeeting { get; set; } = false;
    }
}
