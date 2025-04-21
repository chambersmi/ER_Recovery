using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ER_Recovery.Domains.Models.DTOs
{
    public class UserDTO
    {
        public ApplicationUser User { get; set; }
        public List<string> Roles { get; set; } = new();
    }
}
