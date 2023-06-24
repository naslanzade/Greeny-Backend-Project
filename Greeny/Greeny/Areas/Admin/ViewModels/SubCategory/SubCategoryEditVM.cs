using System.ComponentModel.DataAnnotations;

namespace Greeny.Areas.Admin.ViewModels.SubCategory
{
    public class SubCategoryEditVM
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }      
        public int CategoryId { get; set; }
    }
}
