using ER_Recovery.Application.DTOs;
using ER_Recovery.Application.Interfaces;
using ER_Recovery.Domains.Entities;
using ER_Recovery.Infrastructure.Utility;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ER_Recovery.Web.Pages.AA
{
    public class BoardModel : PageModel
    {
        private readonly IMessageBoardService _messageBoardService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public List<MessageBoardDTO> MessageBoard { get; set; } = new List<MessageBoardDTO>();

        public string? CurrentUserId { get; set; }
        public bool IsAdmin { get; set; }

        public BoardModel(
            IMessageBoardService messageBoardService,
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager
            )
        {
            _messageBoardService = messageBoardService;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task OnGetAsync()
        {
            MessageBoard = await _messageBoardService.GetAllMessagesAsync();
            CurrentUserId = _userManager.GetUserId(User);
            IsAdmin = User.IsInRole(UserRoles.Role_Admin);

        }

        public async Task<IActionResult> OnPostMessageDeleteAsync(int messageId)
        {
            await _messageBoardService.DeleteMessageAsync(messageId);
            return RedirectToPage();
        }
    }
}
