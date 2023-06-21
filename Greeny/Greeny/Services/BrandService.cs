using Greeny.Data;
using Greeny.Models;
using Greeny.Services.Interface;
using Greeny.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace Greeny.Services
{
    public class BrandService : IBrandService
    {
        private readonly AppDbContext _context;

        public BrandService(AppDbContext context)
        {
            _context = context;
            
        }


        public async Task<List<Brand>> GetAllAsync()
        {
            return await _context.Brands.Take(6).ToListAsync();
        }

        public async Task<int> GetCountAsync()
        {
            return await _context.Brands.CountAsync();
        }

        public  List<BrandVM> GetMappedDatas(List<Brand> brands)
        {
            List<BrandVM> list = new();
            foreach (var brand in brands)
            {
                list.Add(new BrandVM
                {                    
                    Name = brand.Name,                  
                    Image = brand.Image,

                });

            }

            return list;
        }

        public async Task<List<Brand>> GetPaginatedDatasAsync(int page, int take)
        {
            return await _context.Brands.Skip((page - 1) * take)
                                          .Take(take)
                                          .ToListAsync();
        }
    }
}
