using Greeny.Areas.Admin.ViewModels.Branch;
using Greeny.Areas.Admin.ViewModels.Team;
using Greeny.Models;

namespace Greeny.Services.Interface
{
    public interface ITeamService
    {
        Task<List<Team>> GetAllAsync();
        Task<List<Team>> GetAllDatasAsync();
        Task<Team> GetByIdAsnyc(int? id);
        Task<List<TeamVM>> GetMappedDatas();
        TeamDetailVM GetMappedData(Team team);
        Task CreateAsync(TeamCreateVM model, List<IFormFile> images);
        Task EditAsync(int teamId, TeamEditVM model, IFormFile newImage);
        Task DeleteAsync(int id);
        Task<Team> GetWithIncludes(int? id);
    }
}
