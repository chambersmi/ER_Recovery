using ER_Recovery.Application.DTOs;
using ER_Recovery.Application.Interfaces;
using ER_Recovery.Domains.Entities;
using ER_Recovery.Infrastructure.Data.Repositories;
using Microsoft.Extensions.Logging;

namespace ER_Recovery.Application.Services
{
    public class MessageBoardService : IMessageBoardService
    {
        private readonly IMessageBoardRepository _messageBoardRepository;
        private readonly IUserRepository _userRepository;

        private readonly ILogger<MessageBoardService> _logger;

        public MessageBoardService(
            IMessageBoardRepository messageBoardRepository, 
            IUserRepository userRepository,
            ILogger<MessageBoardService> logger)
        {
            _messageBoardRepository = messageBoardRepository;
            _userRepository = userRepository;
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
                    CreatedTime = m.CreatedTime,
                    UserId = m.UserId                
                }).ToList();
            }
            else
            {
                return new List<MessageBoardDTO>();
            }
        }

        public async Task<bool> DeleteMessageAsync(int id)
        {
            var isDeleted = await _messageBoardRepository.DeleteMessageById(id);

            return isDeleted;
        }

        public async Task<MessageBoardDTO> PostMessageAsync(AddMessageBoardDTO dto, string userId, string userHandle)
        {


            var message = new MessageBoard
            {                
                Title = dto.Title,
                Content = dto.Content,
                UserId = userId,
                UserHandle = userHandle,
                CreatedTime = DateTime.UtcNow
            };

            var createdPost = await _messageBoardRepository.PostMessageAsync(message);

            return new MessageBoardDTO
            {
                MessageId = createdPost.MessageId,
                Title = createdPost.Title,
                Content = createdPost.Content,
                CreatedTime = createdPost.CreatedTime,
                UserHandle = createdPost.User.UserHandle,
                UserId = createdPost.UserId
            };
        }
    }
}
