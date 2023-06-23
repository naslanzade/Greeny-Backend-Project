using Greeny.Areas.Admin.ViewModels.Brand;
using Greeny.Models;
using Greeny.ViewModels;
using BrandVM = Greeny.Areas.Admin.ViewModels.Brand.BrandVM;

namespace Greeny.Services.Interface
{
    public interface IBrandService
    {
        Task<List<Brand>> GetAllAsync();

        Task<List<Brand>> GetPaginatedDatasAsync(int page, int take);

         List<ViewModels.BrandVM> GetMappedDatas(List<Brand> brands);

        Task<int> GetCountAsync();

        Task<List<BrandVM>> GetAllMappedDatas();
        Task<List<Brand>> GetAllDatasAsync();
        Task<Brand> GetByIdAsync(int id);
        BrandDetailVM GetMappedData(Brand info);
        Task DeleteAsync(int id);
        Task CreateAsync(List<IFormFile> images, BrandCreateVM newInfo);
        Task EditAsync(BrandEditVM request, IFormFile newImage);

        Task<List<Brand>> PaginatedDatasAsync(int page, int take);
        List<BrandVM> MappedDatas(List<Brand> brands);

        Task<int> CountAsync();




    }
}
