using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ER_Recovery.Web.Pages.Shared.Helpers
{
    public class RedirectHomeModel : PageModel
    {
        public IActionResult OnGet()
        {
            var cookie = Request.Cookies["ProgramChoice"];

            if(!string.IsNullOrEmpty(cookie))
            {
                return Redirect(cookie);
            }

            return RedirectToPage("/Index");
        }
    }
}
