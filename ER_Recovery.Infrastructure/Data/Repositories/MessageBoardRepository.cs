using ER_Recovery.Application.DTOs;
using ER_Recovery.Application.Interfaces;
using ER_Recovery.Domains.Entities;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ER_Recovery.Infrastructure.Data.Repositories
{
    public class MessageBoardRepository : IMessageBoardRepository
    {
        private readonly AppDbContext _context;
        private readonly ILogger<MessageBoardRepository> _logger;
        
        public MessageBoardRepository(AppDbContext context, ILogger<MessageBoardRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<List<MessageBoard>> GetAllMessagesAsync()
        {
            return await _context.MessageBoard
                .Include(m => m.User)
                .ToListAsync();
        }

        public async Task<MessageBoard> GetMessageByIdAsync(int id)
        {
                        
            var message = await _context.MessageBoard.FirstOrDefaultAsync(x => x.MessageId == id);

            if(message == null)
            {
                throw new KeyNotFoundException($"Message with ID: {id} not found.");
            }

            return message;

        }

        public async Task<bool> DeleteMessageById(int id)
        {
            var message = await _context.MessageBoard.FirstOrDefaultAsync(x => x.MessageId == id);

            if (message != null)
            {
                _context.MessageBoard.Remove(message);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<MessageBoard> PostMessageAsync(MessageBoard message)
        {
            await _context.MessageBoard.AddAsync(message);
            await _context.SaveChangesAsync();

            return message;
        }
    }
}
