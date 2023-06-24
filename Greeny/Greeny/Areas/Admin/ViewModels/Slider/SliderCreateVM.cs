using System.ComponentModel.DataAnnotations;

namespace Greeny.Areas.Admin.ViewModels.Slider
{
    public class SliderCreateVM
    {
        [Required]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public List<IFormFile> Images { get; set; }
    }
}
