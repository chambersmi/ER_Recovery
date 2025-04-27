using ER_Recovery.Application.DTOs;
using ER_Recovery.Application.Interfaces;
using Microsoft.Extensions.Logging;

namespace ER_Recovery.Application.Services
{
    public class MessageBoardService : IMessageBoardService
    {
        private readonly IMessageBoardRepository _messageBoardRepository;
        private readonly ILogger<MessageBoardService> _logger;

        public MessageBoardService(IMessageBoardRepository messageBoardRepository, ILogger<MessageBoardService> logger)
        {
            _messageBoardRepository = messageBoardRepository;
            _logger = logger;
        }

        public async Task<List<MessageBoardDTO>> GetAllMessagesAsync()
        {
            var messages = await _messageBoardRepository.GetAllMessagesAsync();

            if (messages != null)
            {
                return messages.Select(m => new MessageBoardDTO
                {
                    MessageId = m.MessageId,
                    UserHandle = m.User.UserHandle,
                    Title = m.Title,
                    Content = m.Content,
                    CreatedTime = m.CreatedTime
                
                }).ToList();
            }
            else
            {
                return new List<MessageBoardDTO>();
            }
        }
    }
}
