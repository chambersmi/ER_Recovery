using ER_Recovery.Application.DTOs;

namespace ER_Recovery.Application.Interfaces
{
    public interface IMessageBoardService
    {
        Task<List<MessageBoardDTO>> GetAllMessagesAsync();
    }
}