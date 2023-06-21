using Greeny.Models;

namespace Greeny.Services.Interface
{
    public interface ISubCategoryService 
    {
        Task<List<SubCategory>> GetAllAsync();
    }
}
