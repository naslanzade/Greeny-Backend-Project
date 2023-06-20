namespace Greeny.Models
{
    public class Category :BaseEntity
    {
        public string  Name { get; set; }
        public string Image { get; set; }        
        public ICollection<SubCategory> SubCategory { get; set; }
        public ICollection<Product> Products { get; set; }

    }
}
