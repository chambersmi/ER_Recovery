using ER_Recovery.Application.Services;
using ER_Recovery.Domains.Enums;
using ER_Recovery.Domains.Models.DTOs;
using ER_Recovery.Domains.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Text.Json;

namespace ER_Recovery.Web.Pages.Admin
{
    //[Authorize(Roles = "User")]
    public class AddMeetingModel : PageModel
    {
        private readonly IMeetingsService _meetingService;

        [BindProperty]
        public AddMeetingDTO AddMeetingDTO { get; set; } = new AddMeetingDTO();
        public MeetingDay? MeetingDayEnum { get; set; } = null;
        public MeetingType? MeetingTypeEnum { get; set; } = null;
        public List<SelectListItem> DaysOfWeekSelectList { get; set; }
        public List<SelectListItem> MeetingTypeSelectList { get; set; }

        public AddMeetingModel(IMeetingsService meetingService)
        {
            _meetingService = meetingService;
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
                }).ToList(); ;
        }

        public async Task<IActionResult> OnPostAsync()
        {
            //var meeting = new Meeting()
            //{
            //    Day = AddMeetingDTO.Day,
            //    Time = AddMeetingDTO.Time,
            //    Description = AddMeetingDTO.Description,
            //    Location = AddMeetingDTO.Location,
            //    Address = AddMeetingDTO.Address,
            //    City = AddMeetingDTO.City,
            //    State = AddMeetingDTO.State,
            //    OpenMeeting = AddMeetingDTO.OpenMeeting                
            //};

            await _meetingService.AddMeetingAsync(AddMeetingDTO);

            var notification = new Notifications
            {
                Type = Domains.Enums.NotificationType.Success,
                Message = "Meeting successfully added!"
            };

            TempData["Notification"] = JsonSerializer.Serialize(notification);

            return RedirectToPage("/Meetings");
        }


    }
}
