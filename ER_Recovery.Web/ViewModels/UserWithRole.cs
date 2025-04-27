using ER_Recovery.Domains.Entities;
using ER_Recovery.Domains.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ER_Recovery.Domains.Models.ViewModels
{
    public class UserWithRole
    {
        public string? UserId { get; set; }
        public string UserHandle { get; set; } = "Anonymous";
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string City { get; set; } = null!;
        public DateTime? Birthdate { get; set; }
        public DateTime? SobrietyDate { get; set; }
        public IList<string> Roles { get; set; } = new List<string>();
        public string SelectedRole { get; set; } = string.Empty;
    }
}
