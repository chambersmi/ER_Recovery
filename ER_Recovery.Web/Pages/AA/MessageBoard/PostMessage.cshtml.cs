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

        private readonly IPostService<PostDTO, EditMessageBoardDTO, AddMessageBoardDTO> _postService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<PostMessageModel> _logger;

        [BindProperty]
        public AddMessageBoardDTO AddMessageBoardDTO { get; set; } = new AddMessageBoardDTO();

        public PostMessageModel(
            IPostService<PostDTO, EditMessageBoardDTO, AddMessageBoardDTO> postService,
            ILogger<PostMessageModel> logger,
            UserManager<ApplicationUser> userManager)
        {
            _postService = postService;
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
                //errors with userhandle and userid and user
                await _postService.AddPostWithUserAsync(AddMessageBoardDTO, user.Id, user.UserHandle);

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
