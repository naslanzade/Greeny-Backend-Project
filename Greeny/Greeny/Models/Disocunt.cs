namespace Greeny.Models
{
    public class Disocunt :BaseEntity
    {
        public string Name { get; set; }
        public byte Discount { get; set; }
        public ICollection<Product> Products { get; set; }
    }
}
