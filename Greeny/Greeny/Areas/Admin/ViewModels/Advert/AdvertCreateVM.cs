using System.ComponentModel.DataAnnotations;

namespace Greeny.Areas.Admin.ViewModels.Advert
{
    public class AdvertCreateVM
    {
        [Required]
        public List<IFormFile> Image { get; set; }
    }
}
