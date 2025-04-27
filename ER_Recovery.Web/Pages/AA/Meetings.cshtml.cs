using ER_Recovery.Application.Interfaces;
using ER_Recovery.Domains.Models.DTOs;
using ER_Recovery.Domains.Models.ViewModels;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;

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
            var notificationJson = (string)TempData["Notification"];
            if(notificationJson != null)
            {
                ViewData["Notification"] = JsonSerializer.Deserialize<Notifications>(notificationJson);
            }
            Meetings = await _meetingService.GetScheduledMeetings();
        }
    }
}
