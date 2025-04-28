using ER_Recovery.Application.Interfaces;
using ER_Recovery.Domains.Entities;
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
        private readonly ILogger<UserManagerModel> _logger;
                
        [BindProperty]
        public List<UserWithRole> UsersWithRolesViewModel { get; set; } = new();

        [BindProperty]
        public List<string> AllRoles { get; set; } = new();

        public string? GetCurrentUserId { get; set; }
        public bool IsSelf { get; set; } = true;
        public bool IsAdmin { get; set; } = false;


        public UserManagerModel(IUserRepository userRepo, 
            UserManager<ApplicationUser> userManager,
            IUserManagerService userManagerService,
            RoleManager<IdentityRole> roleManager,
            ILogger<UserManagerModel> logger)
        {
            _userRepo = userRepo;
            _userManager = userManager;
            _roleManager = roleManager;
            _userManagerService = userManagerService;
            _logger = logger;
        }
        public async Task OnGet()
        {
            
            var currentUser = await _userManager.GetUserAsync(User);                       
            IsAdmin = await _userManager.IsInRoleAsync(currentUser, UserRoles.Role_Admin);            

            var userDTO = await _userManagerService.GetAllUsersWithRoles();
            
            if(userDTO == null || !userDTO.Any())
            {
                // log warning
            }

            AllRoles = _roleManager.Roles.Select(r => r.Name!).ToList();

            UsersWithRolesViewModel = userDTO.Select(dto => new UserWithRole
            {
                UserId = dto.UserId,
                UserHandle = dto.UserHandle,
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                City = dto.City,
                Birthdate = dto.Birthdate,
                SobrietyDate = dto.SobrietyDate,
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
            // Check if user is trying to delete themselves
            //var currentUser = await _userManager.GetUserAsync(User);
            //if(currentUser == null)
            //{
            //    return RedirectToPage();
            //}

            //IsSelf = currentUser?.Id == userId;
            //IsAdmin = await _userManager.IsInRoleAsync(currentUser, UserRoles.Role_Admin);
            
            try
            {

                    await _userManagerService.DeleteUserByIdAsync(userId);
            } 
            catch (Exception ex)
            {
                _logger.LogError($"Delete Failed.\n{ex.Message}");
            }
            
            return RedirectToPage();
        }
        
    }
}
