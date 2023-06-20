using Greeny.Models;

namespace Greeny.Services.Interface
{
    public interface ICategoryService
    {
        Task<List<Category>> GetAllAsync();
    }
}
