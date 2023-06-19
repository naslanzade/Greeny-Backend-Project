namespace Greeny.Models
{
    public class Team :BaseEntity
    {
        public string  FullName { get; set; }
        public int PositionId { get; set; }
        public Position  Position { get; set; }
        public string Image { get; set; }
    }
}
