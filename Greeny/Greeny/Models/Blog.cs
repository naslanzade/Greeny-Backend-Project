namespace Greeny.Models
{
    public class Blog :BaseEntity
    {
        public string Image { get; set; }

        public string AuthorId { get; set; }

        public Author Author { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }
    }
}
