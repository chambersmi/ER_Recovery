using ER_Recovery.Application.DTOs;
using ER_Recovery.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ER_Recovery.Web.Pages.AA
{
    public class MessageBoardModel : PageModel
    {
        private readonly IMessageBoardService _messageBoardService;
        public List<MessageBoardDTO> MessageBoard { get; set; } = new List<MessageBoardDTO>();


        public MessageBoardModel(IMessageBoardService messageBoardService)
        {
            _messageBoardService = messageBoardService;
        }

        public async Task OnGetAsync()
        {
            MessageBoard = await _messageBoardService.GetAllMessagesAsync();

        }
    }
}
