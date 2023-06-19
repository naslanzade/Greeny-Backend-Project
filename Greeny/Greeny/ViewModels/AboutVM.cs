using Greeny.Models;

namespace Greeny.ViewModels
{
    public class AboutVM
    {
        public List<Milestone> Milestones { get; set; }
        public List<AboutImage> Images { get; set; }
        public Text Texts { get; set; }
        public List<Team> Teams { get; set; }
        public List<Testimonial> Testimonials { get; set; }
        public BgImage BgImage { get; set; }



    }
}
