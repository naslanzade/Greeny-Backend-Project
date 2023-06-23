using System.ComponentModel.DataAnnotations;

namespace Greeny.Areas.Admin.ViewModels.Author
{
    public class AuthorEditVM
    {
        public int Id { get; set; }

        [Required]
        public string FullName { get; set; }
    }
}
