using System.ComponentModel.DataAnnotations;

namespace Greeny.Areas.Admin.ViewModels.Discount
{
    public class DiscountCreatVM
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public byte Discount { get; set; }


    }
}
