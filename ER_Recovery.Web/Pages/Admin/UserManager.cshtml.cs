using ER_Recovery.Application.Services;
using ER_Recovery.Domains.Models;
using ER_Recovery.Domains.Models.DTOs;
using ER_Recovery.Domains.Models.ViewModels;
using ER_Recovery.Infrastructure.Data.Repositories;
using ER_Recovery.Infrastructure.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ER_Recovery.Web.Pages.Admin
{
    [Authorize(Roles = UserRoles.Role_Admin)]
    public class UserManagerModel : PageModel
    {
        //needs a service class
        private readonly IUserRepository _userRepo;
        private readonly IUserManagerService _userManagerService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
                
        [BindProperty]
        public List<UserWithRole> UsersWithRolesViewModel { get; set; } = new();

        [BindProperty]
        public List<string> AllRoles { get; set; } = new();

        public bool isAdmin { get; set; }

        public UserManagerModel(IUserRepository userRepo, 
            UserManager<ApplicationUser> userManager,
            IUserManagerService userManagerService,
            RoleManager<IdentityRole> roleManager)
        {
            _userRepo = userRepo;
            _userManager = userManager;
            _roleManager = roleManager;
            _userManagerService = userManagerService;
        }
        public async Task OnGet()
        {
            //var users = await _userRepo.GetAllUsersAsync();
            var userDTO = await _userManagerService.GetAllUsersWithRoles();
            AllRoles = _roleManager.Roles.Select(r => r.Name!).ToList();

            UsersWithRolesViewModel = userDTO.Select(dto => new UserWithRole
            {
                User = dto.User,
                Roles = dto.Roles,
                SelectedRole = dto.Roles.FirstOrDefault() ?? ""
            }).ToList();

        }

        public async Task<IActionResult> OnPostChangeRoleAsync(string userId, string newRole)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null || string.IsNullOrEmpty(newRole))
                return RedirectToPage();

            var currentRole = await _userManager.GetRolesAsync(user);
            await _userManager.RemoveFromRolesAsync(user, currentRole);
            await _userManager.AddToRoleAsync(user, newRole);
            //141789
            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostDeleteUserAsync(string userId)
        {
            await _userRepo.DeleteUserByIdAsync(userId);
            return RedirectToPage();
        }
        
    }
}
