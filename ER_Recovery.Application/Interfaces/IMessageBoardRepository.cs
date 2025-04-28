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
    }
}
