using Fiorello.Helpers;
using Greeny.Areas.Admin.ViewModels.Team;
using Greeny.Data;
using Greeny.Models;
using Greeny.Services.Interface;
using Microsoft.EntityFrameworkCore;

namespace Greeny.Services
{
    public class TeamService : ITeamService
    {

        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;

        public TeamService(AppDbContext context,
                           IWebHostEnvironment env)
        {
            
            _context = context;
            _env = env;
        }

        public async Task CreateAsync(TeamCreateVM model, List<IFormFile> images)
        {
            foreach (var item in images)
            {
                string fileName = Guid.NewGuid().ToString() + "_" + item.FileName;

                await item.SaveFileAsync(fileName, _env.WebRootPath, "images/team");


                Team team = new()
                {
                    Image = fileName,
                    FullName = model.FullName,                    
                    PositionId = model.PositionId
                };

                await _context.Teams.AddAsync(team);

            }

            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            Team team = await GetByIdAsnyc(id);

            _context.Teams.Remove(team);

            await _context.SaveChangesAsync();

            string path = Path.Combine(_env.WebRootPath, "images/team", team.Image);

            if (File.Exists(path))
            {
                File.Delete(path);
            }
        }

        public async Task EditAsync(int teamId, TeamEditVM model, IFormFile newImage)
        {
            var team = await GetByIdAsnyc(teamId);

            string oldPath = Path.Combine(_env.WebRootPath, "images/team", newImage.FileName);

            if (File.Exists(oldPath))
            {
                File.Delete(oldPath);
            }

            string fileName = Guid.NewGuid().ToString() + "_" + newImage.FileName;

            await newImage.SaveFileAsync(fileName, _env.WebRootPath, "images/team");

            model.Image = fileName;

            team.FullName = model.FullName;           
            team.PositionId = model.PositionId;
            team.Image = fileName;
            await _context.SaveChangesAsync();
        }

        public async Task<List<Team>> GetAllAsync()
        {
            return await _context.Teams.Include(m => m.Position).ToListAsync();
        }

        public async Task<List<Team>> GetAllDatasAsync()
        {
            return await _context.Teams.Include(m => m.Position).ToListAsync();
        }

        public async Task<Team> GetByIdAsnyc(int? id)
        {
            return await _context.Teams.FirstOrDefaultAsync(m => m.Id == id);
        }

        public TeamDetailVM GetMappedData(Team team)
        {
            return new TeamDetailVM
            {
                FullName = team.FullName,
                PositionName = team.Position.Name,                
                Image = team.Image,
                CreatedDate = team.CreatedDate.ToString("MMMM dd, yyyy"),
            };
        }

        public async Task<List<TeamVM>> GetMappedDatas()
        {
            List<TeamVM> list = new();

            List<Team> infos = await GetAllDatasAsync();

            foreach (var info in infos)
            {
                TeamVM model = new()
                {
                    Id = info.Id,
                    FullName = info.FullName,
                    Image = info.Image,
                    PositionName = info.Position.Name
                };

                list.Add(model);
            }

            return list;
        }

        public async Task<Team> GetWithIncludes(int? id)
        {
            return await _context.Teams.Where(m => m.Id == id)
                                              .Include(m => m.Position)
                                               .FirstOrDefaultAsync();
        }
    }
}
