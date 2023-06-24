using System.ComponentModel.DataAnnotations;

namespace Greeny.Areas.Admin.ViewModels.Tag
{
    public class TagCreateVM
    {
        [Required]
        public string Name { get; set; }
    }
}
