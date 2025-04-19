using ER_Recovery.Domains.Models;
using ER_Recovery.Domains.Models.ViewModels;
using ER_Recovery.Infrastructure.Data.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ER_Recovery.Web.Pages.Admin
{
    public class UserManagerModel : PageModel
    {
        private readonly IUserRepository _userRepo;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
                
        [BindProperty]
        public List<UserWithRole> UsersWithRoles { get; set; } = new();

        [BindProperty]
        public List<string> AllRoles { get; set; } = new();

        public UserManagerModel(IUserRepository userRepo, 
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            _userRepo = userRepo;
            _userManager = userManager;
            _roleManager = roleManager;
        }
        public async Task OnGet()
        {
            var users = await _userRepo.GetAllUsersAsync();
            AllRoles = _roleManager.Roles.Select(r => r.Name!).ToList();

            foreach(var user in users)
            {
                var roles = await _userManager.GetRolesAsync(user);
                UsersWithRoles.Add(new UserWithRole
                {
                    User = user,
                    Roles = roles,
                    SelectedRole = roles.FirstOrDefault() ?? ""
                });
            }
        }

        public async Task OnPostPromote()
        {

        }
    }
}
