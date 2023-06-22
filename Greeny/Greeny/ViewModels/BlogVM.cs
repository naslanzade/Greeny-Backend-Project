using Greeny.Models;

namespace Greeny.ViewModels
{
    public class BlogVM
    {
        public string Name { get; set; }

        public string Image { get; set; }

        public string Description { get; set; }

        public string AuthorName { get; set; }       

        public string CreatedDate { get; set; }

        public List<Blog> BlogByDate { get; set; }

        public BgImage BgImage { get; set; }

        public List<Blog> Blogs { get; set; }
    }
}
