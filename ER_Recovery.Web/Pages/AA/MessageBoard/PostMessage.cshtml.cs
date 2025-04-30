using ER_Recovery.Application.DTOs;
using ER_Recovery.Application.Interfaces;
using ER_Recovery.Domains.Entities;
using ER_Recovery.Domains.Models.ViewModels;
using ER_Recovery.Infrastructure.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;

namespace ER_Recovery.Web.Pages.AA.MessageBoard
{
    [Authorize]
    public class PostMessageModel : PageModel
    {

        private readonly IMessageBoardService _messageBoardService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<PostMessageModel> _logger;

        [BindProperty]
        public AddMessageBoardDTO AddMessageBoardDTO { get; set; } = new AddMessageBoardDTO();

        public PostMessageModel(
            IMessageBoardService messageBoardService, 
            ILogger<PostMessageModel> logger,
            UserManager<ApplicationUser> userManager)
        {
            _messageBoardService = messageBoardService;
            _userManager = userManager;
            _logger = logger;
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            
            if (user == null)
                return Challenge();

            try
            {
                await _messageBoardService.PostMessageAsync(AddMessageBoardDTO, user.Id, user.UserHandle);

                var notification = new Notifications
                {
                    Type = Domains.Enums.NotificationType.Success,
                    Message = "Post successfully added!"
                };

                TempData["Notification"] = JsonSerializer.Serialize(notification);

                return RedirectToPage("/AA/MessageBoard/Index");

            } catch (Exception ex)
            {
                var notification = new Notifications
                {
                    Type = Domains.Enums.NotificationType.Success,
                    Message = "There was an error in adding your post."
                };

                _logger.LogError($"Error when adding message: {ex.Message}");

                return Page();
            }
        }
    }
}
