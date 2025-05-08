using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ER_Recovery.Application.DTOs
{
    public class ReplyInputModelDTO
    {
        public string Content { get; set; } = null!;
        public int? ParentMessageId { get; set; }
        public string Title { get; set; } = "Reply";
    }
}
