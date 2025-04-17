using ER_Recovery.Domains.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ER_Recovery.Domains.Models.DTOs
{
    public class AddMeetingDTO
    {
        public MeetingDay Day { get; set; }
        public TimeSpan Time { get; set; }
        public string? Description { get; set; }
        public string? Location { get; set; }
        public string? Address { get; set; }
        public string? City { get; set; } = "Eaton Rapids";
        public string? State { get; set; } = "MI";
        public bool OpenMeeting { get; set; } = false;

        public MeetingType MeetingType { get; set; } = MeetingType.AA;
    }
}
