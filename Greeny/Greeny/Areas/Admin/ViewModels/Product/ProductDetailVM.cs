namespace Greeny.Areas.Admin.ViewModels.Product
{
    public class ProductDetailVM
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public bool IsStock { get; set; }
        public int SaleCount { get; set; }
        public int RateCount { get; set; }
        public int ProductCount { get; set; }
        public string CategoryName { get; set; }
        public string SubCategoryName { get; set; }
        public string DiscountName { get; set; }
        public IEnumerable<string> TagName { get; set; }
        public string BrandName { get; set; }
        public int SkuCode { get; set; }
        public string CreatedDate { get; set; }
        public IEnumerable<string> Images { get; set; }
    }
}
