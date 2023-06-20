using Greeny.Models;

namespace Greeny.Services.Interface
{
    public interface ITestimonialService
    {
        Task<List<Testimonial>> GetAllAsync();
    }
}
