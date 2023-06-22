using Greeny.Models;

namespace Greeny.ViewModels
{
    public class HomeVM
    {
        public List<Slider> Sliders { get; set; }
        public List<Category> Categories { get; set; }
        public List<Brand> Brands { get; set; }
        public Advert Advert { get; set; }
        public List<Advert> Adverts { get; set; }
        public IEnumerable<Product> ProductByDate { get; set; }
        public IEnumerable<Product> ProductByRate { get; set; }
        public IEnumerable<Product> ProductBySale { get; set; }

        public IEnumerable<Blog> BlogsByDate { get; set; }



    }
}
