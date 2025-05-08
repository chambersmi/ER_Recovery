using ER_Recovery.Domains.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ER_Recovery.Application.Interfaces
{
    public interface IMessageBoardRepository
    {
        Task<List<MessageBoard>> GetAllMessagesAsync();
        Task<bool> DeleteMessageById(int id);
        Task<MessageBoard> PostMessageAsync(MessageBoard message);
        Task<MessageBoard> GetMessageByIdAsync(int id);
        Task<MessageBoard> UpdateMessageAsync(MessageBoard messageBoard);
        //Task<List<MessageBoard>> GetMessagesByParentIdAsync(int parentId);
        Task<PostReply> PostReplyAsync(PostReply reply);
        Task<List<PostReply>> GetRepliesByParentMessageIdAsync(int messageId);
    }
}
