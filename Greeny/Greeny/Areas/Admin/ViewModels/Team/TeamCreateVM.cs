using System.ComponentModel.DataAnnotations;

namespace Greeny.Areas.Admin.ViewModels.Team
{
    public class TeamCreateVM
    {
        [Required]
        public List<IFormFile> Images { get; set; }
        [Required]
        public string FullName { get; set; }       
        public int PositionId { get; set; }
    }
}
