namespace Greeny.Models
{
    public class Author :BaseEntity
    {
        public string FullName { get; set; }
        public ICollection<Blog> Blog { get; set; }
    }
}
