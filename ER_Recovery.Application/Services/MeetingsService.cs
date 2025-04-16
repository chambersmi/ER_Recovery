using ER_Recovery.Infrastructure.Data.Repositories;
using ER_Recovery.Web.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
    }
}
