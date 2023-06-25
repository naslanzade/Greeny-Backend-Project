using Greeny.Areas.Admin.ViewModels.Branch;
using Greeny.Areas.Admin.ViewModels.SubCategory;
using Greeny.Models;

namespace Greeny.Services.Interface
{
    public interface IBranchService
    {
        Task<List<Branch>> GetAllAsync();
        Task<List<Branch>> GetAllDatasAsync();
        Task<Branch> GetByIdAsnyc(int? id);       
        Task<List<BranchVM>> GetMappedDatas();
        BranchDetailVM GetMappedData(Branch branch);       
        Task CreateAsync(BranchCreateVM model, List<IFormFile> images);
        Task EditAsync(int branchId, BranchEditVM model, IFormFile newImage);
        Task DeleteAsync(int id);
        Task<Branch> GetWithIncludes(int? id);
    }
}
