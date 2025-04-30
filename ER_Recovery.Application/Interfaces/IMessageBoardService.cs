using ER_Recovery.Application.DTOs;
using ER_Recovery.Domains.Entities;

namespace ER_Recovery.Application.Interfaces
{
    public interface IMessageBoardService
    {
        Task<List<MessageBoardDTO>> GetAllMessagesAsync();
        Task<bool> DeleteMessageAsync(int id);
        Task<MessageBoardDTO> PostMessageAsync(AddMessageBoardDTO dto, string userId, string userHandle);
        Task<MessageBoardDTO> GetMessageByIdAsync(int id);
        Task<EditMessageBoardDTO> EditMessageAsync(EditMessageBoardDTO dto);

    }
}