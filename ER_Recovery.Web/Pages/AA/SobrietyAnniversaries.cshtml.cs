using ER_Recovery.Application.Interfaces;
using ER_Recovery.Domains.Models.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ER_Recovery.Web.Pages.AA
{
    public class SobrietyAnniversariesModel : PageModel
    {
        private readonly ISobrietyDateService _sobrietyDateService;
        public List<UserDTO> SobrietyDateUsers { get; set; } = new List<UserDTO>();

        public SobrietyAnniversariesModel(ISobrietyDateService sobrietyDateService)
        {
            _sobrietyDateService = sobrietyDateService;
        }
        public async Task<IActionResult> OnGetAsync()
        {
            SobrietyDateUsers = await _sobrietyDateService.GetSobrietyDateAsync();

            return Page();
        }
    }
}
