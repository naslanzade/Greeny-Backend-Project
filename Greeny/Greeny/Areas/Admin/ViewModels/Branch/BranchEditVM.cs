using System.ComponentModel.DataAnnotations;

namespace Greeny.Areas.Admin.ViewModels.Branch
{
    public class BranchEditVM
    {
        public int Id { get; set; }
        public string? Image { get; set; }
        public IFormFile? NewImage { get; set; }

        [Required]
        public string City { get; set; }
        [Required]
        public string Address { get; set; }
        public int CountryId { get; set; }
    }
}
