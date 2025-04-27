using ER_Recovery.Application.Interfaces;
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
    public class UpdateDeleteMeetingModel : PageModel
    {
        private readonly IMeetingsService _meetingService;
        private readonly ILogger<UpdateDeleteMeetingModel> _logger;

        public MeetingDay? MeetingDayEnum { get; set; } = null;
        public MeetingType? MeetingTypeEnum { get; set; } = null;
        public List<SelectListItem> DaysOfWeekSelectList { get; set; }
        public List<SelectListItem> MeetingTypeSelectList { get; set; }

        [BindProperty]
        public EditMeetingDTO EditMeetingDTO { get; set; } = new EditMeetingDTO();

        public UpdateDeleteMeetingModel(IMeetingsService meetingService, ILogger<UpdateDeleteMeetingModel> logger)
        {
            _logger = logger;
            _meetingService = meetingService;
        }

        public async Task OnGet(int id)
        {

            var meetingRequest = await _meetingService.GetMeetingByIdAsync(id);

            if (meetingRequest != null)
            {
                EditMeetingDTO = new EditMeetingDTO
                {
                    Id = meetingRequest.Id,
                    Day = meetingRequest.Day,
                    Time = meetingRequest.Time,
                    Description = meetingRequest.Description,
                    Location = meetingRequest.Location,
                    Address = meetingRequest.Address,
                    City = meetingRequest.City,
                    State = meetingRequest.State,
                    OpenMeeting = meetingRequest.OpenMeeting,
                    MeetingType = meetingRequest.MeetingType
                };

                MeetingDayEnum = meetingRequest.Day;
                MeetingTypeEnum = meetingRequest.MeetingType;
            }

            DaysOfWeekSelectList = Enum.GetValues(typeof(MeetingDay))
                .Cast<MeetingDay>()
                .Select(d => new SelectListItem
                {
                    Text = d.ToString(),
                    Value = d.ToString(),
                    Selected = (meetingRequest?.Day == d)
                }).ToList();

            MeetingTypeSelectList = Enum.GetValues(typeof(MeetingType))
                .Cast<MeetingType>()
                .Select(d => new SelectListItem
                {
                    Text = d.ToString(),
                    Value = d.ToString(),
                    Selected = (meetingRequest?.MeetingType == d)
                }).ToList();
        
        }

        public async Task<IActionResult> OnPostEdit()
        {

            if(ModelState.IsValid)
            {
                try
                {
                    await _meetingService.UpdateMeetingAsync(EditMeetingDTO);
                    return RedirectToPage("/AA/Meetings");
                } catch (Exception ex)
                {
                    _logger.LogError($"Error updating meeting: {ex.Message}");
                }
            }

            return Page();
        }

        public async Task<IActionResult> OnPostDelete(int id)
        {
        
            try
            {
                var success = await _meetingService.DeleteMeetingByIdAsync(id);

                var notification = new Notifications
                {
                    Type = NotificationType.Success,
                    Message = "Meeting successfully removed!"
                };

                TempData["Notification"] = JsonSerializer.Serialize(notification);

                if (!success)
                {
                    ModelState.AddModelError("", "Failed to delete meeting.");
                    return Page();
                }

                return RedirectToPage("/AA/Meetings");
            } 
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error deleting meeting with ID: {id}");
                ModelState.AddModelError("", "An unexpected error has occured.");

                var notification = new Notifications
                {
                    Type = NotificationType.Error,
                    Message = "Error in deleting meeting."
                };

                return Page();
            }

        }
    }
}