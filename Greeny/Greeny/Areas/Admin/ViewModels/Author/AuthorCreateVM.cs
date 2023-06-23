using System.ComponentModel.DataAnnotations;

namespace Greeny.Areas.Admin.ViewModels.Author
{
    public class AuthorCreateVM
    {

        [Required]
        public string FullName { get; set; }
    }
}
