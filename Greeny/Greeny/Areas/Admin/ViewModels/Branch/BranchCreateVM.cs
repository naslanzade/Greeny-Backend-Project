using System.ComponentModel.DataAnnotations;

namespace Greeny.Areas.Admin.ViewModels.Branch
{
    public class BranchCreateVM
    {
        [Required]
        public List<IFormFile> Images { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public string Address { get; set; }
        public int CountryId { get; set; }
    }
}
