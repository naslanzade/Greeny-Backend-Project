using Greeny.Areas.Admin.ViewModels.Advert;
using Greeny.Areas.Admin.ViewModels.BgImage;
using Greeny.Models;

namespace Greeny.Services.Interface
{
    public interface IBgImageService
    {
        Task<BgImage> GetAllAsync();

        Task<List<BgImage>> GetAllDataAsync();

        Task<List<BgImageVM>> GetAllMappedDatas();

        Task<BgImage> GetByIdAsync(int id);

        BgImageDetailVM GetMappedData(BgImage info);

        Task CreateAsync(List<IFormFile> images);

        Task DeleteAsync(int id);

        Task EditAsync(BgImage image, IFormFile newImage);
    }
}
