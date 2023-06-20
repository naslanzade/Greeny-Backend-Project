using Greeny.Models;

namespace Greeny.Services.Interface
{
    public interface ISliderService
    {
        Task<List<Slider>> GetAllAsync();
    }
}
