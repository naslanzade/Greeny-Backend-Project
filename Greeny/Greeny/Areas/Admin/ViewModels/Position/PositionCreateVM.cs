using System.ComponentModel.DataAnnotations;

namespace Greeny.Areas.Admin.ViewModels.Position
{
    public class PositionCreateVM
    {
        [Required]
        public string Name { get; set; }
    }
}
