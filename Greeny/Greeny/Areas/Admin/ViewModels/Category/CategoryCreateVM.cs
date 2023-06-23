using System.ComponentModel.DataAnnotations;

namespace Greeny.Areas.Admin.ViewModels.Category
{
    public class CategoryCreateVM
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public List<IFormFile> Images { get; set; }
    }
}
