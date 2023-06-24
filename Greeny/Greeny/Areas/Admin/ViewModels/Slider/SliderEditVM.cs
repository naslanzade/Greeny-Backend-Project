using System.ComponentModel.DataAnnotations;

namespace Greeny.Areas.Admin.ViewModels.Slider
{
    public class SliderEditVM
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }
        public string? SliderImage { get; set; }
        public IFormFile? NewImage { get; set; }
    }
}
