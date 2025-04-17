using ER_Recovery.Domains.Models.DTOs;
using ER_Recovery.Domains.Models.ViewModels;
using ER_Recovery.Infrastructure.Data.Repositories;
using ER_Recovery.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ER_Recovery.Application.Services
{
    public class MeetingsService : IMeetingsService
    {
        private readonly IMeetingRepository _meetingRepository;
        private readonly ILogger<MeetingsService> _logger;
        public List<Meeting> Meetings { get; set; } = new List<Meeting>();

        public MeetingsService(ILogger<MeetingsService> logger, IMeetingRepository meetingsRepository)
        {
            _meetingRepository = meetingsRepository;
            _logger = logger;
        }

        public async Task<List<Meeting>> GetScheduledMeetings()
        {
            Meetings = await _meetingRepository.GetAllMeetingsAsync();

            return Meetings;
        }

        public async Task<Meeting> AddMeetingAsync(AddMeetingDTO meetingDTO)
        {
            // Consider AutoMapper
            var meeting = new Meeting
            {
                Day = meetingDTO.Day,
                Time = meetingDTO.Time,
                Description = meetingDTO.Description,
                Location = meetingDTO.Location,
                Address = meetingDTO.Address,
                City = meetingDTO.City,
                State = meetingDTO.State,
                OpenMeeting = meetingDTO.OpenMeeting,
                MeetingType = meetingDTO.MeetingType
            };

            await _meetingRepository.AddMeetingAsync(meeting);

            

            return meeting;           
        }
    }
}
