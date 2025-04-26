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
        public string? ProgramChoice { get; set; }

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;

        }

        public IActionResult OnGet()
        {
            return RedirectToPage("/AA/Index");
            //if (User?.Identity?.IsAuthenticated == true)
            //{
            //    var getUserCookieChoice = Request.Cookies["ProgramChoice"];

            //    if (!string.IsNullOrEmpty(getUserCookieChoice))
            //    {
            //        return RedirectToPage($"/{getUserCookieChoice}/Index");
            //    }
            //}

            //return Page();
        }

        public IActionResult OnPost(string choice)
        {
            if(!string.IsNullOrEmpty(choice))
            {
                Response.Cookies.Append("ProgramChoice", choice, new CookieOptions
                {
                    Expires = DateTimeOffset.UtcNow.AddDays(30),                                        
                });

                return RedirectToPage($"/{choice}/Index");
            }

            return Page();
        }
    }
}
