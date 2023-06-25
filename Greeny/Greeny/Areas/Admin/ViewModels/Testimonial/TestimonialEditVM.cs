using System.ComponentModel.DataAnnotations;

namespace Greeny.Areas.Admin.ViewModels.Testimonial
{
    public class TestimonialEditVM
    {
        public int Id { get; set; }
        public string? Image { get; set; }
        public IFormFile? NewImage { get; set; }

        [Required]
        public string FullName { get; set; }

        [Required]
        public string Description { get; set; }

        public int PositionId { get; set; }
    }
}
