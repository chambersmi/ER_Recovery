using ER_Recovery.Web.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ER_Recovery.Web.Pages.AA
{
    public class DailyReflectionsModel : PageModel
    {
        private readonly ILogger<DailyReflectionsModel> _logger;

        private readonly IDailyReflectionService _dailyReflectionService;

        public DailyReflectionsModel(ILogger<DailyReflectionsModel> logger, IDailyReflectionService dailyReflectionService)
        {
            _logger = logger;
            _dailyReflectionService = dailyReflectionService;
        }

        public string ReflectionTitle { get; set; } = "";
        public string ReflectionBody { get; set; } = "";
        public string ReflectionDate { get; set; } = "";

        public async Task OnGetAsync()
        {
            var reflection = await _dailyReflectionService.GetTodaysReflectionAsync();
            ReflectionTitle = reflection.Title;
            ReflectionBody = reflection.Body;
            ReflectionDate = reflection.Date.ToString("MMMM dd");

        }
    }
}
