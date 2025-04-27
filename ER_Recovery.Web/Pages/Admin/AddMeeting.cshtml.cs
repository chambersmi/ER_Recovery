using ER_Recovery.Application.Services.Interfaces;
using ER_Recovery.Domains.Enums;
using ER_Recovery.Domains.Models.DTOs;
using ER_Recovery.Domains.Models.ViewModels;
using ER_Recovery.Infrastructure.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Text.Json;

namespace ER_Recovery.Web.Pages.Admin
{
    [Authorize(Roles = UserRoles.Role_Admin)]
    public class AddMeetingModel : PageModel
    {
        private readonly IMeetingsService _meetingService;
        private readonly ILogger<AddMeetingModel> _logger;

        [BindProperty]
        public AddMeetingDTO AddMeetingDTO { get; set; } = new AddMeetingDTO();
        public MeetingDay? MeetingDayEnum { get; set; } = null;
        public MeetingType? MeetingTypeEnum { get; set; } = null;
        public List<SelectListItem> DaysOfWeekSelectList { get; set; } = new();
        public List<SelectListItem> MeetingTypeSelectList { get; set; } = new();

        public AddMeetingModel(ILogger<AddMeetingModel> logger, IMeetingsService meetingService)
        {
            _meetingService = meetingService;
            _logger = logger;
        }

        public void OnGet()
        {
            DaysOfWeekSelectList = Enum.GetValues(typeof(MeetingDay))
                .Cast<MeetingDay>()
                .Select(d => new SelectListItem
                {
                    Text = d.ToString(),
                    Value = d.ToString()
                }).ToList();

            MeetingTypeSelectList = Enum.GetValues(typeof(MeetingType))
                .Cast<MeetingType>()
                .Select(d => new SelectListItem
                {
                    Text = d.ToString(),
                    Value = d.ToString()
                }).ToList();
        }

        public async Task<IActionResult> OnPostAsync()
        {

            try
            {
                await _meetingService.AddMeetingAsync(AddMeetingDTO);

                var notification = new Notifications
                {
                    Type = Domains.Enums.NotificationType.Success,
                    Message = "Meeting successfully added!"
                };

                TempData["Notification"] = JsonSerializer.Serialize(notification);

                return RedirectToPage("/AA/Meetings");

            } catch (Exception ex)
            {
                var notification = new Notifications
                {
                    Type = NotificationType.Error,
                    Message = "Error in adding meeting."
                };

                _logger.LogError($"Error when adding meeting: {ex.Message}");

                return Page();
            }
        }
    }
}
