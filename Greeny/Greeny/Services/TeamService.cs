using Greeny.Data;
using Greeny.Models;
using Greeny.Services.Interface;
using Microsoft.EntityFrameworkCore;

namespace Greeny.Services
{
    public class TeamService : ITeamService
    {

        private readonly AppDbContext _context;

        public TeamService(AppDbContext context)
        {
            
            _context = context;
        }
        public async Task<List<Team>> GetAllAsync()
        {
            return await _context.Teams.Include(m => m.Position).ToListAsync();
        }
    }
}
