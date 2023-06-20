namespace Greeny.Models
{
    public class Tag :BaseEntity
    {
        public string Name { get; set; }
        public ICollection<ProductTag> ProductTag { get; set; }

    }
}
