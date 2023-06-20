using Greeny.Models;

namespace Greeny.Services.Interface
{
    public interface IAdvertService
    {
        Task<Advert> GetFirstHomeAdvertAsync();

        Task<List<Advert>> GetAllAdvertForHomeAsync();
    }
}
