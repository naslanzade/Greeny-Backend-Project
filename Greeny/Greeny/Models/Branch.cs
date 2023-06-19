namespace Greeny.Models
{
    public class Branch :BaseEntity
    {
        public string Image { get; set; }
        public string City { get; set; }
        public string  Address { get; set; }
        public int CountryId { get; set; }
        public Country Country { get; set; }
    }
}
