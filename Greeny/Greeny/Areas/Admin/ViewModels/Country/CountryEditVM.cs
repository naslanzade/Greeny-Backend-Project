using System.ComponentModel.DataAnnotations;

namespace Greeny.Areas.Admin.ViewModels.Country
{
    public class CountryEditVM
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
    }
}
