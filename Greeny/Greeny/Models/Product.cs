using System.ComponentModel.DataAnnotations.Schema;

namespace Greeny.Models
{
    public class Product :BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public bool IsStock { get; set; }
        public int SaleCount { get; set; }
        public int RateCount { get; set; }
        public int ProductCount { get; set; }
        public int CategoryId { get; set; }       
        public Category Category { get; set; }

        [ForeignKey("SubCategory")]
        public int SubCategoryId { get; set; }
        public SubCategory SubCategory { get; set; }
        public int DiscountId { get; set; }
        public Disocunt Disocunt { get; set; }
        public int BrandId { get; set; }
        public Brand Brand { get; set; }
        public int SkuCode { get; set; }
        public ICollection<ProductImage> Images { get; set; }
        public ICollection<ProductTag> ProductTags { get; set; }

    }
}
