namespace Greeny.Models
{
    public class Country :BaseEntity
    {
        public string Name { get; set; }

        public ICollection<Branch> Branch { get; set; }
    }
}
