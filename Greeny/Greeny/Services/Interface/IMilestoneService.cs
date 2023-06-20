using Greeny.Models;

namespace Greeny.Services.Interface
{
    public interface IMilestoneService
    {
        Task<List<Milestone>> GetAllAsync();
    }
}
