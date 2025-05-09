using ER_Recovery.Domains.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ER_Recovery.Application.DTOs
{
    public class MessageBoardDTO
    {

        public int MessageId { get; set; }
        public string UserId { get; set; } = null!;
        public string UserHandle { get; set; } = null!;

        public string Title { get; set; } = null!;
        public string Content { get; set; } = null!;
        public DateTime CreatedTime { get; set; }
        public List<PostReplyDTO> Replies { get; set; } = new();

    }
}
