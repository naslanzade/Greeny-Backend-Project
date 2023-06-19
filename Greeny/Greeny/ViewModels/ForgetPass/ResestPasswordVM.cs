using System.ComponentModel.DataAnnotations;

namespace Greeny.ViewModels.ForgetPass
{
    public class ResestPasswordVM
    {
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password), Compare(nameof(Password))]
        public string ConfirmPassword { get; set; }
        public string  UserId { get; set; }
        public string  Token { get; set; }
    }
}
