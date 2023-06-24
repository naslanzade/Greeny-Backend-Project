using System.ComponentModel.DataAnnotations;

namespace Greeny.Areas.Admin.ViewModels.Position
{
    public class PositionEditVM
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
    }
}
