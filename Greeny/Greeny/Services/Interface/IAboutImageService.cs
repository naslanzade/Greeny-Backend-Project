using Greeny.Models;

namespace Greeny.Services.Interface
{
    public interface IAboutImageService
    {
        Task<List<AboutImage>> GetAllAsync();

    }
}
