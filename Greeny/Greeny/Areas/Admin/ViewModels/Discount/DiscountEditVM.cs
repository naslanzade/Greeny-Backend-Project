using System.ComponentModel.DataAnnotations;

namespace Greeny.Areas.Admin.ViewModels.Discount
{
    public class DiscountEditVM
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public byte Discount { get; set; }
    }
}
