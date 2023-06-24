using Greeny.Areas.Admin.ViewModels.Discount;
using Greeny.Areas.Admin.ViewModels.Milestone;
using Greeny.Models;

namespace Greeny.Services.Interface
{
    public interface IMilestoneService
    {
        Task<List<Milestone>> GetAllAsync();

        Task<Milestone> GetByIdAsync(int id);

        MilestoneDetailVM GetMappedData(Milestone milestone);

        Task CreateAsync(MilestoneCreateVM milestone);

        Task DeleteAsync(int id);

        Task EditAsync(MilestoneEditVM milestone);

        Task<List<MilestoneVM>> GetAllMappedDatas();
    }
}
