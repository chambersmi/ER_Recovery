using ER_Recovery.Web.Models;

namespace ER_Recovery.Application.Services.Interfaces
{
    public interface IDailyReflectionService
    {
        Task<DailyReflection> GetTodaysReflectionAsync();
    }
}
