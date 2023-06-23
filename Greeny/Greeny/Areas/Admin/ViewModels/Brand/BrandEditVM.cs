using System.ComponentModel.DataAnnotations;

namespace Greeny.Areas.Admin.ViewModels.Brand
{
    public class BrandEditVM
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
        public string? Image { get; set; }
        public IFormFile? NewImage { get; set; }
    }
}
