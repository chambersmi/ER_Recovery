using Microsoft.AspNetCore.Identity;

namespace ER_Recovery.Domains.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public string UserHandle { get; set; } = "Anonymous";
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string City { get; set; } = null!;
        public DateTime? Birthdate { get; set; }
        public DateTime? SobrietyDate { get; set; }
    }
}
