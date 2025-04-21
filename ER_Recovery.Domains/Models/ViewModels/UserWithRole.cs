using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ER_Recovery.Domains.Models.ViewModels
{
    public class UserWithRole
    {
        public ApplicationUser User { get; set; }
        public IList<string> Roles { get; set; } = new List<string>();
        public string SelectedRole { get; set; } = string.Empty;
    }
}
