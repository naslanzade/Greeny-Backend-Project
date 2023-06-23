using System.ComponentModel.DataAnnotations;

namespace Greeny.Areas.Admin.ViewModels.Country
{
    public class CountryCreateVM
    {
        [Required]
        public string Name { get; set; }
    }
}
