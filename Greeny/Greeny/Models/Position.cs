namespace Greeny.Models
{
    public class Position :BaseEntity
    {
        public string  Name { get; set; }

        public ICollection<Team> Team { get; set; }

        public ICollection<Testimonial> Testimonial { get; set; }
    }
}
