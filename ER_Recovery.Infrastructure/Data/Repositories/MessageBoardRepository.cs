using ER_Recovery.Application.Interfaces;
using ER_Recovery.Domains.Entities;
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
            return await _context.MessageBoard.ToListAsync();
        }        
    }
}
