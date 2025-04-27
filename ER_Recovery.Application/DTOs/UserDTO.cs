using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ER_Recovery.Domains.Models.DTOs
{
    public class UserDTO
    {
        public string? UserId { get; set; }
        public string UserHandle { get; set; } = "Anonymous";
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string City { get; set; } = null!;
        public DateTime? Birthdate { get; set; }
        public DateTime? SobrietyDate { get; set; }
        public List<string> Roles { get; set; } = new();
    }
}
