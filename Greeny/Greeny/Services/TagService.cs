using Greeny.Data;
using Greeny.Models;
using Greeny.Services.Interface;
using Microsoft.EntityFrameworkCore;

namespace Greeny.Services
{
    public class TagService : ITagService
    {
        private readonly AppDbContext _context;

        public TagService(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Tag>> GetAllAsync()
        {
            return await _context.Tags.Include(m=>m.ProductTag).ToListAsync();
        }
    }
}
