using System.ComponentModel.DataAnnotations;

namespace Greeny.Areas.Admin.ViewModels.Brand
{
    public class BrandCreateVM
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public List<IFormFile> Images { get; set; }

    }
}
