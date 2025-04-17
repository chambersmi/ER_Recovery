using ER_Recovery.Domains.Models.DTOs;
using ER_Recovery.Web.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ER_Recovery.Infrastructure.Data.Repositories
{
    public class MeetingRepository : IMeetingRepository
    {
        private readonly AppDbContext _context;
        private readonly ILogger<MeetingRepository> _logger;

        public MeetingRepository(ILogger<MeetingRepository> logger, AppDbContext context)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<List<Meeting>> GetAllMeetingsAsync()
        {
            return await _context.Meetings.ToListAsync();
        }

        public async Task<Meeting> AddMeetingAsync(Meeting meeting)
        {
            await _context.Meetings.AddAsync(meeting);
            await _context.SaveChangesAsync();

            return meeting;
        }
    }
}
