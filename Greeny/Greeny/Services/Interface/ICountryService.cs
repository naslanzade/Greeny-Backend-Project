using Greeny.Areas.Admin.ViewModels.Author;
using Greeny.Areas.Admin.ViewModels.Country;
using Greeny.Models;

namespace Greeny.Services.Interface
{
    public interface ICountryService
    {
        Task<List<CountryVM>> GetAllMappedDatas();

        Task<List<Country>> GetAllAsync();

        Task<Country> GetByIdAsync(int id);

        CountryDetailVM GetMappedData(Country country);

        Task CreateAsync(CountryCreateVM country);

        Task DeleteAsync(int id);

        Task EditAsync(CountryEditVM country);
    }
}
