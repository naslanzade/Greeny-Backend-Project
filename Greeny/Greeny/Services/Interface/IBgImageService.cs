using Greeny.Models;

namespace Greeny.Services.Interface
{
    public interface IBgImageService
    {
        Task<BgImage> GetAllAsync();
    }
}
