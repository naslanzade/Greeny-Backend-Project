using Greeny.Areas.Admin.ViewModels.Author;
using Greeny.Areas.Admin.ViewModels.Country;
using Greeny.Data;
using Greeny.Models;
using Greeny.Services.Interface;
using Microsoft.EntityFrameworkCore;

namespace Greeny.Services
{
    public class CountryService : ICountryService
    {

        private readonly AppDbContext _context;

        public CountryService(AppDbContext context)
        {
            _context = context;
        }
        public async Task CreateAsync(CountryCreateVM country)
        {
            Country newCountry = new()
            {
                Name = country.Name,

            };
            await _context.Countries.AddAsync(newCountry);

            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            Country country = await GetByIdAsync(id);

            _context.Remove(country);
            await _context.SaveChangesAsync();
        }

        public async Task EditAsync(CountryEditVM country)
        {
            Country newCountry = new()
            {
                Id = country.Id,
                Name = country.Name,
            };

            _context.Update(newCountry);

            await _context.SaveChangesAsync();
        }

        public async Task<List<Country>> GetAllAsync()
        {
            return await _context.Countries.ToListAsync();
        }

        public async Task<List<CountryVM>> GetAllMappedDatas()
        {
            List<CountryVM> list = new();

            List<Country> infos = await GetAllAsync();

            foreach (var info in infos)
            {
                CountryVM model = new()
                {
                    Id = info.Id,
                    Name = info.Name,

                };

                list.Add(model);
            }

            return list;
        }

        public async Task<Country> GetByIdAsync(int id)
        {
            return await _context.Countries.FirstOrDefaultAsync(m => m.Id == id);
        }

        public CountryDetailVM GetMappedData(Country country)
        {
            CountryDetailVM model = new()
            {
                Name = country.Name,
                CreatedDate = country.CreatedDate.ToString("MM.dd.yyyy"),

            };

            return model;
        }
    }
}
