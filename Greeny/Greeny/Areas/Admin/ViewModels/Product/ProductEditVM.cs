using Greeny.Models;
using System.ComponentModel.DataAnnotations;

namespace Greeny.Areas.Admin.ViewModels.Product
{
    public class ProductEditVM
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public decimal Price { get; set; }
        public int CategoryId { get; set; }
        public List<IFormFile> newImages { get; set; }
        public List<ProductImage> Images { get; set; }
        public int BrandId { get; set; }
        public int DiscountId { get; set; }
        public int SubCategoryId { get; set; }
        public bool IsStock { get; set; }
        public int SaleCount { get; set; }
        public int RateCount { get; set; }
        public int ProductCount { get; set; }
        public int SkuCode { get; set; }
        public List<int> TagId { get; set; }
    }
}
