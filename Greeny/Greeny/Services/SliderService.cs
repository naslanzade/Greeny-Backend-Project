using Greeny.Data;
using Greeny.Models;
using Greeny.Services.Interface;
using Microsoft.EntityFrameworkCore;

namespace Greeny.Services
{
    public class SliderService : ISliderService
    {

        private readonly AppDbContext _context;

        public SliderService(AppDbContext context)
        {
         
            _context = context;
        }
        public async Task<List<Slider>> GetAllAsync()
        {
            return await _context.Sliders.ToListAsync();
        }
    }
}
