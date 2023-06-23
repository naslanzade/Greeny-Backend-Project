using Greeny.Areas.Admin.ViewModels.AboutImage;
using Greeny.Areas.Admin.ViewModels.Advert;
using Greeny.Models;

namespace Greeny.Services.Interface
{
    public interface IAdvertService
    {
        Task<Advert> GetFirstHomeAdvertAsync();

        Task<List<Advert>> GetAllAdvertForHomeAsync();

        Task<List<AdvertVM>> GetAllMappedDatas();

        Task<List<Advert>> GetAllAsync();

        Task<Advert> GetByIdAsync(int id);

        AdvertDetailVM GetMappedData(Advert info);

        Task CreateAsync(List<IFormFile> images);

        Task DeleteAsync(int id);

        Task EditAsync(Advert image, IFormFile newImage);
    }
}
