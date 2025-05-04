using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ER_Recovery.Domains.Entities
{
    public interface IPost
    {
        int Id { get; }
        string UserId { get; set; }
        ApplicationUser User { get; set; }
        string UserHandle { get; set; }
        string Title { get; set; }
        string Content { get; set; }
        DateTime CreatedTime { get; set; }
    }
}
