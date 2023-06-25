using System.ComponentModel.DataAnnotations;

namespace Greeny.Areas.Admin.ViewModels.Testimonial
{
    public class TestimonialCreateVM
    {
        [Required]
        public List<IFormFile> Images { get; set; }
        [Required]
        public string FullName { get; set; }
        [Required]
        public string Description { get; set; }

        public int PositionId { get; set; }
    }
}
