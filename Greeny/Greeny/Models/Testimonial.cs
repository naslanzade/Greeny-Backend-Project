namespace Greeny.Models
{
    public class Testimonial :BaseEntity
    {

        public string FullName { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public int PositionId { get; set; }
        public Position Position { get; set; }
    }
}
