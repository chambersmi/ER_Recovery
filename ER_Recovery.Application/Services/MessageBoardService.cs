using ER_Recovery.Application.DTOs;
using ER_Recovery.Application.Interfaces;
using ER_Recovery.Domains.Entities;
using ER_Recovery.Infrastructure.Data.Repositories;
using Microsoft.Extensions.Logging;
using System.Security.Cryptography.X509Certificates;

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
                    UserId = m.UserId,
                    Replies = m.Replies
                    .Where(r => r.ParentReplyId == null)
                    .Select(r => new PostReplyDTO
                    {
                        ReplyId = r.ReplyId,
                        Content = r.Content,
                        UserId = r.UserId,
                        UserHandle = r.UserHandle,
                        CreatedTime = r.CreatedTime,
                        ParentMessageId = r.ParentMessageId,
                        Replies = r.Replies
                        .Where(subReply => subReply.ParentReplyId == r.ReplyId)
                        .Select(sr => new PostReplyDTO
                        {
                            ReplyId = sr.ReplyId,
                            Content = sr.Content,
                            UserId = sr.UserId,
                            UserHandle = sr.UserHandle,
                            CreatedTime = sr.CreatedTime,
                            ParentMessageId = sr.ParentMessageId
                        }).ToList()
                    }).ToList()
                }).ToList();
            }
            else
            {
                return new List<MessageBoardDTO>();
            }
        }

        public async Task<MessageBoardDTO> GetMessageByIdAsync(int id)
        {
            var message = await _messageBoardRepository.GetMessageByIdAsync(id);

            if(message != null)
            {
                var dto = new MessageBoardDTO
                {
                    MessageId = message.MessageId,
                    UserHandle = message.UserHandle,
                    Title = message.Title,
                    Content = message.Content,
                    CreatedTime = message.CreatedTime,
                    UserId = message.UserId
                };

                return dto;            
            }

            return new MessageBoardDTO();

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
                UserHandle = userHandle,
                UserId = createdPost.UserId
            };
        }

        public async Task<ReplyInputModelDTO> PostReplyAsync(ReplyInputModelDTO dto, string userId, string userHandle)
        {
            if(dto.ParentMessageId == null)
            {
                throw new ArgumentException("Reply must have a parent Message ID.");
            }

            var reply = new PostReply
            {
                Content = dto.Content,
                UserId = userId,
                UserHandle = userHandle,
                CreatedTime = DateTime.UtcNow,
                ParentMessageId = dto.ParentMessageId.Value
            };

            var createdPostReply = await _messageBoardRepository.PostReplyAsync(reply);

            return new ReplyInputModelDTO
            {
                Content = createdPostReply.Content,
                ParentMessageId = dto.ParentMessageId
            };
        }

        public async Task<EditMessageBoardDTO> EditMessageAsync(EditMessageBoardDTO dto)
        {
            var existingMessage = await _messageBoardRepository.GetMessageByIdAsync(dto.MessageId);

            if(existingMessage == null)
            {
                _logger.LogError($"Message with ID {existingMessage.MessageId} not found.");
                throw new KeyNotFoundException($"Message with ID {existingMessage.MessageId} not found.");                
            }
            
            existingMessage.Title = dto.Title;
            existingMessage.Content = dto.Content;

            var messageResponse = await _messageBoardRepository.UpdateMessageAsync(existingMessage);

            return new EditMessageBoardDTO
            {
                Title = messageResponse.Title,
                Content = messageResponse.Content
            };
        }

        private MessageBoardDTO MapToMessageBoardDTO(MessageBoard message)
        {
            return new MessageBoardDTO
            {
                MessageId = message.MessageId,
                Title = message.Title,
                Content = message.Content,
                CreatedTime = message.CreatedTime,
                UserId = message.UserId,
                UserHandle = message.UserHandle,
                Replies = message.Replies
                .Where(r => r.ParentReplyId == null)
                .Select(MapToReplyDTO)
                .ToList()
            };
        }

        private PostReplyDTO MapToReplyDTO(PostReply reply)
        {
            return new PostReplyDTO
            {
                ReplyId = reply.ReplyId,
                Content = reply.Content,
                CreatedTime = reply.CreatedTime,
                UserId = reply.UserId,
                UserHandle = reply.UserHandle,
                ParentMessageId = reply.ParentReplyId,
                Replies = reply.Replies?.Select(MapToReplyDTO).ToList() ?? new()
            };
        }

    }
}
