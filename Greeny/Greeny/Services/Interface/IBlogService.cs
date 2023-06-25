using Greeny.Areas.Admin.ViewModels.Blog;
using Greeny.Models;


namespace Greeny.Services.Interface
{
    public interface IBlogService
    {
        Task<List<Blog>> GetAllAsync();

        Task<List<Blog>> GetPaginatedDatasAsync(int page, int take);

        List<ViewModels.BlogVM> GetMappedDatas(List<Blog> blogs);

        Task<int> GetCountAsync();

        Task<List<Blog>> GetBlogsByDate();

        //For Admin Panel
        Task<List<Blog>> GetAllDatasAsync();
        Task<Blog> GetByIdAsnyc(int? id);
        Task<List<Areas.Admin.ViewModels.Blog.BlogVM>> GetMappedDatas();
        BlogDetailVM GetMappedData(Blog blog);
        Task CreateAsync(BlogCreateVM model, List<IFormFile> images);
        Task EditAsync(int blogId, BlogEditVM model, IFormFile newImage);
        Task DeleteAsync(int id);
        Task<Blog> GetWithIncludes(int? id);
    }
}
