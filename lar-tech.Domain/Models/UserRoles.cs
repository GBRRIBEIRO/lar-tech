using lar_tech.Domain.Identity;

namespace lar_tech.Domain.Models
{
    public class UserRoles
    {
        public ApplicationUser ApplicationUser { get; set; }
        public List<string> Roles { get; set; }
    }
}
