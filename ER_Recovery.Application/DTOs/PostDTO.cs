using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ER_Recovery.Application.DTOs
{
    public class PostDTO
    {
        public int PostId { get; set; }
        public string UserId { get; set; } = null!;
        public string UserHandle { get; set; } = null!;
        public string Title { get; set; } = null!;
        public string Content { get; set; } = null!;
        public DateTime CreatedTime { get; set; } = DateTime.UtcNow!;
    }
}
