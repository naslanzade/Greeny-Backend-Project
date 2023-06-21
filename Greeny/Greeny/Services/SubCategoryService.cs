using Greeny.Data;
using Greeny.Models;
using Greeny.Services.Interface;
using Microsoft.EntityFrameworkCore;

namespace Greeny.Services
{
    public class SubCategoryService : ISubCategoryService
    {

        private readonly AppDbContext _context;

        public SubCategoryService(AppDbContext context)
        {
            _context = context;
        }
        public async Task<List<SubCategory>> GetAllAsync()
        {
            return await _context.SubCategories.ToListAsync();
        }
    }
}
