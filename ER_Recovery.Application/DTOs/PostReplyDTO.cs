using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ER_Recovery.Application.DTOs
{
    public class PostReplyDTO
    {
        public int ReplyId { get; set; }
        public string Content { get; set; } = null!;
        public DateTime CreatedTime { get; set; }
        public string UserId { get; set; } = null!;
        public string UserHandle { get; set; } = null!;
        public int? ParentReplyId { get; set; }
        public List<PostReplyDTO> Replies { get; set; } = new();
    }
}
