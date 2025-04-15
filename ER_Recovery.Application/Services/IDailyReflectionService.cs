using ER_Recovery.Web.Models;

namespace ER_Recovery.Web.Services
{
    public interface IDailyReflectionService
    {
        Task<DailyReflection> GetTodaysReflectionAsync();
    }
}
