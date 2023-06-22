using Greeny.Models;
using Greeny.ViewModels;

namespace Greeny.Services.Interface
{
    public interface IBlogService
    {
        Task<List<Blog>> GetAllAsync();

        Task<List<Blog>> GetPaginatedDatasAsync(int page, int take);

        List<BlogVM> GetMappedDatas(List<Blog> blogs);

        Task<int> GetCountAsync();

        Task<List<Blog>> GetBlogsByDate();
    }
}
