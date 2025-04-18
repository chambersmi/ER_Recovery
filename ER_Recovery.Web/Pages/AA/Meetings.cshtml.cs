using ER_Recovery.Application.Services;
using ER_Recovery.Domains.Models.DTOs;
using ER_Recovery.Infrastructure.Data.Repositories;
using ER_Recovery.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ER_Recovery.Web.Pages.AA
{
    public class MeetingsModel : PageModel
    {
        private readonly IMeetingsService _meetingService;
        private readonly ILogger<MeetingsModel> _logger;
        public List<MeetingDTO> Meetings { get; set; } = new List<MeetingDTO>();

        public MeetingsModel(ILogger<MeetingsModel> logger, IMeetingsService meetingService)
        {
            _meetingService = meetingService;
            _logger = logger;
        }

        public async Task OnGetAsync()
        {
            Meetings = await _meetingService.GetScheduledMeetings();
        }


    }
}
