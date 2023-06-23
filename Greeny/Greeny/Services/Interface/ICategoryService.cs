using Greeny.Areas.Admin.ViewModels.Category;
using Greeny.Models;

namespace Greeny.Services.Interface
{
    public interface ICategoryService
    {
        Task<List<Category>> GetAllAsync();
        Task<Category> GetByIdAsync(int id);
        CategoryDetailVM GetMappedData(Category info);
        Task DeleteAsync(int id);
        Task CreateAsync(List<IFormFile> images, CategoryCreateVM newInfo);
        Task EditAsync(CategoryEditVM request, IFormFile newImage);       
        Task<List<Category>> PaginatedDatasAsync(int page, int take);
        Task<int> CountAsync();

        List<CategoryVM> MappedDatas(List<Category> categories);
    }
}
