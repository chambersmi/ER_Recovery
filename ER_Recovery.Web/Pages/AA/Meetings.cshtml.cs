using ER_Recovery.Application.Services;
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
        public List<Meeting> Meetings { get; set; } = new List<Meeting>();

        public MeetingsModel(ILogger<MeetingsModel> logger, IMeetingsService meetingService)
        {
            _meetingService = meetingService;
            _logger = logger;
        }

        public async Task OnGetAsync()
        {
            Meetings = await _meetingService.GetScheduledMeetings();
        }

        private List<Meeting> GetScheduledMeetings()
        {
            return null;
        }
    }
}
