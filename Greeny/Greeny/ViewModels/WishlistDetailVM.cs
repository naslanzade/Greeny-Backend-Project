using Greeny.Models;

namespace Greeny.ViewModels
{
    public class WishlistDetailVM
    {
        public int Id { get; set; }
        public string Image { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public bool  StockStatus { get; set; }
        public List<Product> Products { get; set; }
    }
}
