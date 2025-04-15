using ER_Recovery.Web.Services;
using HtmlAgilityPack;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;

namespace ER_Recovery.Web.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly IDailyReflectionService _dailyReflectionService;

        public string ReflectionTitle { get; set; } = "";
        public string ReflectionBody { get; set; } = "";
        public string ReflectionDate { get; set; } = "";

        public IndexModel(ILogger<IndexModel> logger, IDailyReflectionService dailyReflectionService)
        {
            _logger = logger;
            _dailyReflectionService = dailyReflectionService;
        }

        public async Task OnGetAsync()
        {
            var reflection = await _dailyReflectionService.GetTodaysReflectionAsync();
            ReflectionTitle = reflection.Title;
            ReflectionBody = reflection.Body;
            ReflectionDate = reflection.Date.ToString("MMMM dd");

        }
    }
}
