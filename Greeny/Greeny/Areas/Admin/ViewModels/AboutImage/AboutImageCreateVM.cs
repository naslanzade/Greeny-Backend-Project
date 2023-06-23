using System.ComponentModel.DataAnnotations;

namespace Greeny.Areas.Admin.ViewModels.AboutImage
{
    public class AboutImageCreateVM
    {
        [Required]
        public List<IFormFile> Image { get; set; }
    }
}
