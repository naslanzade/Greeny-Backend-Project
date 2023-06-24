using System.ComponentModel.DataAnnotations;

namespace Greeny.Areas.Admin.ViewModels.Text
{
    public class TextCreateVM
    {
        [Required]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }
    }
}
