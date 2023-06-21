using Greeny.Models;

namespace Greeny.ViewModels
{
    public class ProductDetailVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal ActualPrice { get; set; }
        public decimal DiscountPrice { get; set; }
        public byte Percent { get; set; }
        public List<ProductImage> Images { get; set; }
        public string BrandName { get; set; }
        public List<ProductTag> Tags { get; set; }
        public int SkuCode { get; set; }
       
    }
}
