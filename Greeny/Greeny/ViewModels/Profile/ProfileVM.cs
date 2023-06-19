using System.ComponentModel.DataAnnotations;

namespace Greeny.ViewModels.Profile
{
    public class ProfileVM
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Email { get; set; }
    }
}
