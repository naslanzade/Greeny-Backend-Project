using System.ComponentModel.DataAnnotations;

namespace Greeny.Areas.Admin.ViewModels.BgImage
{
    public class BgImageCreateVM
    {
        [Required]
        public List<IFormFile> Image { get; set; }
    }
}
