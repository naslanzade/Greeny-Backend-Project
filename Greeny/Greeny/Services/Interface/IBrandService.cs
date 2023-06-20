using Greeny.Models;
using Greeny.ViewModels;

namespace Greeny.Services.Interface
{
    public interface IBrandService
    {
        Task<List<Brand>> GetAllAsync();

        Task<List<Brand>> GetPaginatedDatasAsync(int page, int take);

         List<BrandVM> GetMappedDatas(List<Brand> brands);

        Task<int> GetCountAsync();
    }
}
