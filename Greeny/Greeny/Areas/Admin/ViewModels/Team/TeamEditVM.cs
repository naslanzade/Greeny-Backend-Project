using System.ComponentModel.DataAnnotations;

namespace Greeny.Areas.Admin.ViewModels.Team
{
    public class TeamEditVM
    {
        public int Id { get; set; }
        public string? Image { get; set; }
        public IFormFile? NewImage { get; set; }

        [Required]
        public string FullName { get; set; }
        
        public int PositionId { get; set; }
    }
}
