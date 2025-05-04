using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ER_Recovery.Domains.Entities
{
    public class MessageBoard : IPost
    {
        [Key]
        public int MessageId { get; set; }
        public int Id => MessageId;
        public string UserId { get; set; } = null!;
        public ApplicationUser User { get; set; } = null!;
        public string UserHandle { get; set; } = null!;
        public string Title { get; set; } = null!;
        public string Content { get; set; } = null!;
        public DateTime CreatedTime { get; set; } = DateTime.UtcNow;
        
    }
}
