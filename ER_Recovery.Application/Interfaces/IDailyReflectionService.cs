using ER_Recovery.Domains.Entities;

namespace ER_Recovery.Application.Interfaces
{
    public interface IDailyReflectionService
    {
        Task<DailyReflection> GetTodaysReflectionAsync();
    }
}
