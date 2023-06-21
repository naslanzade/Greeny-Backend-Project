using Greeny.Models;

namespace Greeny.Services.Interface
{
    public interface ITagService
    {
        Task<IEnumerable<Tag>> GetAllAsync();
    }
}
