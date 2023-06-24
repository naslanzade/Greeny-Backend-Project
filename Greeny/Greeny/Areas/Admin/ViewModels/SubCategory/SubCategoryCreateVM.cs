using System.ComponentModel.DataAnnotations;

namespace Greeny.Areas.Admin.ViewModels.SubCategory
{
    public class SubCategoryCreateVM
    {
        [Required]
        public string Name { get; set; }       
        public int CategoryId { get; set; }
    }
}
