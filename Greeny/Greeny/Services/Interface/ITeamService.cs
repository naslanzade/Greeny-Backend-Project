using Greeny.Models;

namespace Greeny.Services.Interface
{
    public interface ITeamService
    {
        Task<List<Team>> GetAllAsync();
    }
}
