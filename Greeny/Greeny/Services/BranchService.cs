using Greeny.Data;
using Greeny.Models;
using Greeny.Services.Interface;
using Microsoft.EntityFrameworkCore;

namespace Greeny.Services
{
    public class BranchService : IBranchService
    {

        private readonly AppDbContext _context;

        public BranchService(AppDbContext context)
        {
            
            _context = context;
        }

        public async Task<List<Branch>> GetAllAsync()
        {
            return await _context.Branches.Include(m=>m.Country).ToListAsync();
        }
    }
}
