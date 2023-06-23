using Greeny.Areas.Admin.ViewModels.AboutImage;
using Greeny.Models;

namespace Greeny.Services.Interface
{
    public interface IAboutImageService
    {
        Task<List<AboutImage>> GetAllAsync();

        Task<List<AboutImageVM>> GetAllMappedDatas();

        Task<AboutImage> GetByIdAsync(int id);

        AboutImageDetailVM GetMappedData(AboutImage info);

        Task CreateAsync(List<IFormFile> images);

        Task DeleteAsync(int id);

        Task EditAsync(AboutImage image, IFormFile newImage);
    }
}
