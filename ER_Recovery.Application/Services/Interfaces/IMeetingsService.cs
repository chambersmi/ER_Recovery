using ER_Recovery.Domains.Models.DTOs;
using ER_Recovery.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ER_Recovery.Application.Services.Interfaces
{
    public interface IMeetingsService
    {
        Task<MeetingDTO> AddMeetingAsync(AddMeetingDTO meetingDTO);
        Task<MeetingDTO> UpdateMeetingAsync(EditMeetingDTO meetingDTO);
        Task<MeetingDTO?> GetMeetingByIdAsync(int id);
        Task<List<MeetingDTO>> GetScheduledMeetings();
        Task<bool> DeleteMeetingByIdAsync(int id);
    }
}
