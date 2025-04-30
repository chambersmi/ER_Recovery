using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ER_Recovery.Application.DTOs
{
    public class EditMessageBoardDTO
    {
        public int MessageId { get; set; }
        public string? UserId { get; set; }
        public string Title { get; set; } = null!;
        public string Content { get; set; } = null!;
    }
}
