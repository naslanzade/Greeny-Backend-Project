using System.ComponentModel.DataAnnotations;

namespace Greeny.Areas.Admin.ViewModels.Blog
{
    public class BlogEditVM
    {
        public int Id { get; set; }
        public string? Image { get; set; }
        public IFormFile? NewImage { get; set; }

        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }

        public string AuthorId { get; set; }
        public string AuthorId1 { get; set; }
    }
}
