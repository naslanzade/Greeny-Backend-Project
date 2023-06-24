using Greeny.Areas.Admin.ViewModels.Category;
using Greeny.Areas.Admin.ViewModels.Slider;
using Greeny.Models;

namespace Greeny.Services.Interface
{
    public interface ISliderService
    {
        Task<List<Slider>> GetAllAsync();      
        Task<Slider> GetByIdAsync(int id);
        SliderDetailVM GetMappedData(Slider info);
        Task DeleteAsync(int id);
        Task CreateAsync(List<IFormFile> images, SliderCreateVM newInfo);
        Task EditAsync(SliderEditVM request, IFormFile newImage);
        Task<List<SliderVM>> MappedDatas();
    }
}
