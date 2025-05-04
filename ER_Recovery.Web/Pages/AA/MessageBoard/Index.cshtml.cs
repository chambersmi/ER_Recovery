using ER_Recovery.Application.DTOs;
using ER_Recovery.Application.Interfaces;
using ER_Recovery.Domains.Entities;
using ER_Recovery.Domains.Models.ViewModels;
using ER_Recovery.Infrastructure.Utility;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;

namespace ER_Recovery.Web.Pages.AA.MessageBoard
{
    public class IndexModel : PageModel
    {
        //private readonly IMessageBoardService _messageBoardService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        private readonly IPostService<PostDTO, EditMessageBoardDTO, AddMessageBoardDTO> _postService;

        [BindProperty]
        public List<PostDTO> MessageBoard { get; set; } = new List<PostDTO>();

        public string? CurrentUserId { get; set; }
        public bool IsAdmin { get; set; }

        public IndexModel(
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager,
            IPostService<PostDTO, EditMessageBoardDTO, AddMessageBoardDTO> postService
            )
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _postService = postService;
        }

        public async Task OnGetAsync()
        {
            MessageBoard = await _postService.GetAllPostsAsync();

            var user = await _userManager.GetUserAsync(User);
            
            CurrentUserId = user?.Id;
            IsAdmin = user != null && await _userManager.IsInRoleAsync(user, UserRoles.Role_Admin);

            var notificationJson = (string)TempData["Notification"];
            if (notificationJson != null)
            {
                ViewData["Notification"] = JsonSerializer.Deserialize<Notifications>(notificationJson);
            }
        }

        public async Task<IActionResult> OnPostMessageDeleteAsync(int messageId)
        {
            await _postService.DeletePostAsync(messageId);

            var notification = new Notifications
            {
                Type = Domains.Enums.NotificationType.Success,
                Message = "Post successfully deleted."
            };

            TempData["Notification"] = JsonSerializer.Serialize(notification);

            return RedirectToPage();
        }
    }
}
