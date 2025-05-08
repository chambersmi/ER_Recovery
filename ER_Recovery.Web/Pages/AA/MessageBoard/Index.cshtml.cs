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
        private readonly IMessageBoardService _messageBoardService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;        

        [BindProperty]
        public List<MessageBoardDTO> MessageBoard { get; set; } = new List<MessageBoardDTO>();

        public string? CurrentUserId { get; set; }
        public bool IsAdmin { get; set; }

        [BindProperty]
        public ReplyInputModelDTO ReplyInput { get; set; } = new();

        public IndexModel(
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager,
            IMessageBoardService messageBoardService
            )
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _messageBoardService = messageBoardService;
        }

        public async Task OnGetAsync()
        {
            MessageBoard = await _messageBoardService.GetAllMessagesAsync();

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
            await _messageBoardService.DeleteMessageAsync(messageId);

            var notification = new Notifications
            {
                Type = Domains.Enums.NotificationType.Success,
                Message = "Post successfully deleted."
            };

            TempData["Notification"] = JsonSerializer.Serialize(notification);

            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostReplyMessageAsync()
        {
            var user = await _userManager.GetUserAsync(User);

            if (user == null)
                return RedirectToPage("/Account/Login");

            if(!ModelState.IsValid || string.IsNullOrWhiteSpace(ReplyInput.Content))
            {
                return RedirectToPage();
            }

            var dto = new ReplyInputModelDTO
            {
                Content = ReplyInput.Content,
                ParentMessageId = ReplyInput.ParentMessageId,
                Title = $"Reply to message {ReplyInput.ParentMessageId}"
            };

            await _messageBoardService.PostReplyAsync(dto, user.Id, user.UserHandle);

            var notification = new Notifications
            {
                Type = Domains.Enums.NotificationType.Success,
                Message = "Reply posted!"
            };

            TempData["Notification"] = JsonSerializer.Serialize(notification);

            return RedirectToPage();
        }
    }
}
