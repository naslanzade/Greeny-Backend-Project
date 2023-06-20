using Greeny.Data;
using Greeny.Models;
using Greeny.Services.Interface;
using Microsoft.EntityFrameworkCore;

namespace Greeny.Services
{
    public class TestimonialService : ITestimonialService
    {

        private readonly AppDbContext _context;

        public TestimonialService(AppDbContext context)
        {
            _context = context;
        }
        public async Task<List<Testimonial>> GetAllAsync()
        {
            return await _context.Testimonials.Include(m=>m.Position).ToListAsync();
        }
    }
}
