using Greeny.Data;
using Greeny.Models;
using Greeny.Services.Interface;
using Microsoft.EntityFrameworkCore;

namespace Greeny.Services
{
    public class AboutImageService : IAboutImageService
    {


        private readonly AppDbContext _context;


        public AboutImageService(AppDbContext context)
        {

            _context = context;
        }
        public async Task<List<AboutImage>> GetAllAsync()
        {
           return await _context.AboutImages.ToListAsync();
        }
    }
}
