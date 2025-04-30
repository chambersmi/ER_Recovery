using ER_Recovery.Application.DTOs;
using ER_Recovery.Application.Interfaces;
using ER_Recovery.Domains.Entities;
using ER_Recovery.Domains.Enums;
using ER_Recovery.Domains.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;

namespace ER_Recovery.Web.Pages.AA.MessageBoard
{
    public class EditMessageModel : PageModel
    {
        private readonly ILogger<EditMessageModel> _logger;
        private readonly IMessageBoardService _messageBoardService;
        private readonly UserManager<ApplicationUser> _userManager;

        [BindProperty]
        public EditMessageBoardDTO EditMessageBoardDTO { get; set; } = new EditMessageBoardDTO();

        public MessageBoardDTO MessageBoardDTO { get; set; } = new MessageBoardDTO();

        public EditMessageModel(
            ILogger<EditMessageModel> logger,
            IMessageBoardService messageBoardService,
            UserManager<ApplicationUser> userManager)
        {
            _logger = logger;
            _messageBoardService = messageBoardService;
            _userManager = userManager;
        }

        public async Task OnGet(int messageId)
        {
            var existingMessage = await _messageBoardService.GetMessageByIdAsync(messageId);
            var userId = _userManager.GetUserId(User);

            if (existingMessage != null)
            {
                EditMessageBoardDTO = new EditMessageBoardDTO
                {
                    MessageId = existingMessage.MessageId,
                    UserId = userId,
                    Title = existingMessage.Title,
                    Content = existingMessage.Content
                };
            }
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if(ModelState.IsValid)
            {
                try
                {
                    await _messageBoardService.EditMessageAsync(EditMessageBoardDTO);

                    var notification = new Notifications
                    {
                        Type = NotificationType.Success,
                        Message = "Post successfully updated!"
                    };

                    TempData["Notification"] = JsonSerializer.Serialize(notification);


                    return RedirectToPage("/AA/MessageBoard/Index");
                } 
                catch (Exception ex)
                {
                    _logger.LogError($"Error updating message: {ex.Message}");
                    Console.WriteLine(ex.Message);
                }
            }

            return Page();
        }
    }
}
