using Microsoft.AspNetCore.Identity;

namespace Greeny.Models
{
    public class AppUser :IdentityUser
    {
        public string FullName { get; set; }
        public string  Image { get; set; }
    }
}
