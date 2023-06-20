using Greeny.Data;
using Greeny.Models;
using Greeny.Services.Interface;
using Microsoft.EntityFrameworkCore;

namespace Greeny.Services
{
    public class BgImageService : IBgImageService
    {
        private readonly AppDbContext _context;


        public BgImageService(AppDbContext context)
        {
            _context = context;
        }
        public async Task<BgImage> GetAllAsync()
        {
            return await _context.BgImages.FirstOrDefaultAsync();
        }
    }
}
