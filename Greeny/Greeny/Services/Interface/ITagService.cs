using Greeny.Areas.Admin.ViewModels.Author;
using Greeny.Areas.Admin.ViewModels.Tag;
using Greeny.Models;

namespace Greeny.Services.Interface
{
    public interface ITagService
    {
        Task<IEnumerable<Tag>> GetAllAsync();

        Task<List<TagVM>> GetAllMappedDatas();

        Task<List<Tag>> GetAllDatasAsync();

        Task<Tag> GetByIdAsync(int id);

        TagDetailVM GetMappedData(Tag tag);

        Task CreateAsync(TagCreateVM tag);

        Task DeleteAsync(int id);

        Task EditAsync(TagEditVM tag);

        Task<List<Tag>> PaginatedDatasAsync(int page, int take);
       
        Task<int> CountAsync();

        List<TagVM> MappedDatas(List<Tag> tags);
    }
}
