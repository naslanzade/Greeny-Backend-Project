using Greeny.Data;
using Greeny.Models;
using Greeny.Services.Interface;
using Microsoft.EntityFrameworkCore;

namespace Greeny.Services
{
    public class CategoryService : ICategoryService
    {

        private readonly AppDbContext _context;

        public CategoryService( AppDbContext context)
        {
            _context = context;
        }
        public async Task<List<Category>> GetAllAsync()
        {
            return await _context.Categories.ToListAsync();
        }
    }
}
