using ER_Recovery.Domains.Entities;

namespace ER_Recovery.Infrastructure.Data.Repositories
{
    public interface IMeetingRepository
    {
        Task<List<Meeting>> GetAllMeetingsAsync();
        Task<Meeting> AddMeetingAsync(Meeting meetingDTO);
        Task<Meeting> GetMeetingByIdAsync(int id);
        Task<Meeting> UpdateMeetingAsync(Meeting meeting);        
        Task<bool> DeleteMeetingByIdAsync(int id);
    }
}
