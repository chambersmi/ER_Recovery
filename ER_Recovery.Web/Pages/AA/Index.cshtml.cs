using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ER_Recovery.Web.Pages.AA
{
    public class IndexModel : PageModel
    {
        public IActionResult OnGet()
        {
            if(User?.Identity?.IsAuthenticated == true)
            {
                var cookie = Request.Cookies["ProgramChoice"];
                if(!string.IsNullOrEmpty(cookie))
                {
                    Response.Cookies.Append("ProgramChoice", "AA", new CookieOptions
                    {
                        Expires = DateTimeOffset.Now.AddDays(30)
                    });
                }
            }

            return Page();
        }
    }
}
