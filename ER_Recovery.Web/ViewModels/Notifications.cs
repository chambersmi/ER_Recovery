using ER_Recovery.Domains.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ER_Recovery.Domains.Models.ViewModels
{
    public class Notifications
    {
        public string? Message { get; set; }
        public NotificationType Type { get; set; }
    }
}
