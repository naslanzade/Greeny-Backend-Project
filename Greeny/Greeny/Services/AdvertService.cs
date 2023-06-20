using Greeny.Data;
using Greeny.Models;
using Greeny.Services.Interface;
using Microsoft.EntityFrameworkCore;

namespace Greeny.Services
{
    public class AdvertService : IAdvertService
    {

        private readonly AppDbContext _context;

        public AdvertService(AppDbContext context)
        {
            _context = context;
        }
        public async Task<List<Advert>> GetAllAdvertForHomeAsync()
        {
            return await _context.Adverts.Skip(1).Take(2).ToListAsync();
        }

        public async Task<Advert> GetFirstHomeAdvertAsync()
        {
            return await _context.Adverts.FirstOrDefaultAsync();
        }
    }
}
