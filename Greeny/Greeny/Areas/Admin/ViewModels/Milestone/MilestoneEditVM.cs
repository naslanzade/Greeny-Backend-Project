using System.ComponentModel.DataAnnotations;

namespace Greeny.Areas.Admin.ViewModels.Milestone
{
    public class MilestoneEditVM
    {
        public int Id { get; set; }

        [Required]
        public string Type { get; set; }

        [Required]
        public int Counter { get; set; }
    }
}
