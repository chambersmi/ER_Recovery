using ER_Recovery.Application.Services.Interfaces;
using ER_Recovery.Domains.Enums;
using ER_Recovery.Domains.Models.DTOs;
using ER_Recovery.Domains.Models.ViewModels;
using ER_Recovery.Infrastructure.Data.Repositories.Interfaces;
using ER_Recovery.Web.Models;
using Microsoft.Extensions.Logging;


namespace ER_Recovery.Application.Services
{
    public class MeetingsService : IMeetingsService
    {
        private readonly IMeetingRepository _meetingRepository;
        private readonly ILogger<MeetingsService> _logger;


        public MeetingsService(ILogger<MeetingsService> logger, IMeetingRepository meetingsRepository)
        {
            _meetingRepository = meetingsRepository;
            _logger = logger;
        }

        public async Task<List<MeetingDTO>> GetScheduledMeetings()
        {
            var meetings = await _meetingRepository.GetAllMeetingsAsync();

            if (meetings != null)                
            {
                return meetings.Select(m => new MeetingDTO
                {
                    Id = m.Id,
                    Day = m.Day.GetValueOrDefault(),
                    Time = m.Time,
                    Description = m.Description,
                    Location = m.Location,
                    Address = m.Address,
                    City = m.City,
                    State = m.State,
                    OpenMeeting = m.OpenMeeting,
                    MeetingType = m.MeetingType
                }).ToList();
            }

            return new List<MeetingDTO>();
        }

        public async Task<MeetingDTO?> GetMeetingByIdAsync(int id)
        {
            var existingMeeting = await _meetingRepository.GetMeetingByIdAsync(id);

            if (existingMeeting == null)
            {
                return null;
            }

            return new MeetingDTO
            {
                Id = existingMeeting.Id,
                Day = existingMeeting.Day.GetValueOrDefault(),
                Time = existingMeeting.Time,
                Description = existingMeeting.Description,
                Location = existingMeeting.Location,
                Address = existingMeeting.Address,
                City = existingMeeting.City,
                State = existingMeeting.State,
                OpenMeeting = existingMeeting.OpenMeeting,
                MeetingType = existingMeeting.MeetingType
            };
        }


        public async Task<MeetingDTO> AddMeetingAsync(AddMeetingDTO meetingDTO)
        {
            // Consider AutoMapper
            var meeting = new Meeting
            {
                Id = meetingDTO.Id,
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

            var meetingResponse = await _meetingRepository.AddMeetingAsync(meeting);

            return new MeetingDTO
            {
                Id = meetingResponse.Id,
                Day = meetingResponse.Day.GetValueOrDefault(),
                Time = meetingResponse.Time,
                Description = meetingResponse.Description,
                Location = meetingResponse.Location,
                Address = meetingResponse.Address,
                City = meetingResponse.City,
                State = meetingResponse.State,
                OpenMeeting = meetingResponse.OpenMeeting,
                MeetingType = meetingResponse.MeetingType
            };
        }

        public async Task<MeetingDTO> UpdateMeetingAsync(EditMeetingDTO meetingDTO)
        {
            var existingMeeting = await _meetingRepository.GetMeetingByIdAsync(meetingDTO.Id);

            if (existingMeeting == null)
                throw new KeyNotFoundException($"Meeting with ID {meetingDTO.Id} not found.");

            existingMeeting.Id = meetingDTO.Id;
            existingMeeting.Day = meetingDTO.Day;
            existingMeeting.Time = meetingDTO.Time;
            existingMeeting.Description = meetingDTO.Description;
            existingMeeting.Location = meetingDTO.Location;
            existingMeeting.Address = meetingDTO.Address;
            existingMeeting.City = meetingDTO.City;
            existingMeeting.State = meetingDTO.State;
            existingMeeting.OpenMeeting = meetingDTO.OpenMeeting;
            existingMeeting.MeetingType = meetingDTO.MeetingType;

            var meetingResponse = await  _meetingRepository.UpdateMeetingAsync(existingMeeting);

            return new MeetingDTO
            {
                Id = meetingResponse.Id,
                Day = meetingResponse.Day.GetValueOrDefault(),
                Time = meetingResponse.Time,
                Description = meetingResponse.Description,
                Location = meetingResponse.Location,
                Address = meetingResponse.Address,
                City = meetingResponse.City,
                State = meetingResponse.State,
                OpenMeeting = meetingResponse.OpenMeeting,
                MeetingType = meetingResponse.MeetingType
            };
        }

        public async Task<bool> DeleteMeetingByIdAsync(int id)
        {
            return await _meetingRepository.DeleteMeetingByIdAsync(id);
        }
    }
}