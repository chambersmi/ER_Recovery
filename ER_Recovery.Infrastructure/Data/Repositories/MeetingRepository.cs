using ER_Recovery.Domains.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

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

        public async Task<Meeting> GetMeetingByIdAsync(int id)
        {
            return await _context.Meetings.FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task<Meeting> AddMeetingAsync(Meeting meeting)
        {
            await _context.Meetings.AddAsync(meeting);
            await _context.SaveChangesAsync();

            return meeting;
        }

        public async Task<Meeting> UpdateMeetingAsync(Meeting meeting)
        {
            _context.Meetings.Update(meeting);
            await _context.SaveChangesAsync();
            
            return meeting;
        }

        public async Task<bool> DeleteMeetingByIdAsync(int id)
        {
            var meetingRequest = await _context.Meetings.FindAsync(id);

            if (meetingRequest != null)
            {
                _context.Meetings.Remove(meetingRequest);
                await _context.SaveChangesAsync();

                return true;
            }

            return false;
        }
    }
}
