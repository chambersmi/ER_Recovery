using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ER_Recovery.Domains.Models.DTOs
{
    public class MeetingDTO
    {
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
