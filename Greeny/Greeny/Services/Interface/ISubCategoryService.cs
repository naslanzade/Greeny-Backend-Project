using Greeny.Areas.Admin.ViewModels.SubCategory;
using Greeny.Models;
using Greeny.ViewModels;

namespace Greeny.Services.Interface
{
    public interface ISubCategoryService 
    {
        Task<List<SubCategory>> GetAllAsync();
        Task<SubCategory> GetByIdAsnyc(int? id);      
        Task<List<SubCategory>> GetPaginatedDatasAsync(int page, int take);
        List<SubCategoryVM> GetMappedDatas(List<SubCategory> subCategories);       
        SubCategoryDetailVM GetMappedData(SubCategory subCategory);
        Task<int> GetCountAsync();
        Task CreateAsync(SubCategoryCreateVM model);
        Task EditAsync(int subCategoryId, SubCategoryEditVM model);
        Task DeleteAsync(int id);

        Task<SubCategory> GetWithIncludes(int? id);
       
    }
}
