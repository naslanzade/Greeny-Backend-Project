using Greeny.Data;
using Greeny.Models;
using Greeny.Services.Interface;
using Microsoft.EntityFrameworkCore;

namespace Greeny.Services
{
    public class MilestoneService : IMilestoneService
    {

        private readonly AppDbContext _context;

        public MilestoneService(AppDbContext context)
        {
         
            _context = context;
        }
        public async Task<List<Milestone>> GetAllAsync()
        {
            return await _context.Milestones.ToListAsync();
        }
    }
}
