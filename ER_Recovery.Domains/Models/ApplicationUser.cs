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
        public string Name { get; set; } = "Anonymous";
        public DateTime? Birthdate { get; set; }
        public DateTime? SobrietyDate { get; set; }
    }
}
