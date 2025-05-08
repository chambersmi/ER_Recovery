using System.ComponentModel.DataAnnotations;

namespace ER_Recovery.Domains.Entities
{
    public class PostReply
    {
        [Key]
        public int ReplyId { get; set; }
        public string Content { get; set; } = null!;
        public string UserId { get; set; } = null!;
        public string UserHandle { get; set; } = null!;
        public DateTime CreatedTime { get; set; }

        public int ParentMessageId { get; set; }
        public MessageBoard Message { get; set; } = null!;

        public int? ParentReplyId { get; set; }
        public PostReply? ParentMessageReply { get; set; }

        public ICollection<PostReply> Replies { get; set; } = new List<PostReply>();
    }
}