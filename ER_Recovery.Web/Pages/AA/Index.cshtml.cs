using ER_Recovery.Application.Services;
using ER_Recovery.Domains.Models.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ER_Recovery.Web.Pages.AA
{
    public class IndexModel : PageModel
    {
        private readonly IMeetingsService _meetingService;
        // Could be added to UserService
        private readonly ISobrietyDateService _sobrietyDateService;

        private readonly ILogger<IndexModel> _logger;
        public List<MeetingDTO> Meetings { get; set; } = new List<MeetingDTO>();
        public List<UserDTO> SobrietyDateUsers { get; set; } = new List<UserDTO>();

        public IndexModel(ISobrietyDateService sobrietyDateService, IMeetingsService meetingsService, ILogger<IndexModel> logger)
        {
            _sobrietyDateService = sobrietyDateService;
            _meetingService = meetingsService;
            _logger = logger;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            Meetings = await _meetingService.GetScheduledMeetings();

            if(User?.Identity?.IsAuthenticated == true)
            {
                var cookie = Request.Cookies["ProgramChoice"];
                if(!string.IsNullOrEmpty(cookie))
                {
                    Response.Cookies.Append("ProgramChoice", "AA", new CookieOptions
                    {
                        Expires = DateTimeOffset.Now.AddDays(30)
                    });
                }
            }

            SobrietyDateUsers = await _sobrietyDateService.GetSobrietyDateAsync();

            return Page();
        }
    }
}
