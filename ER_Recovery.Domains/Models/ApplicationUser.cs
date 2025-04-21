using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ER_Recovery.Domains.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string Nickname { get; set; } = "Anonymous";
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string City { get; set; } = null!;
        public DateTime? Birthdate { get; set; }
        public DateTime? SobrietyDate { get; set; }
    }
}
