using Greeny.Areas.Admin.ViewModels.Country;
using Greeny.Areas.Admin.ViewModels.Discount;
using Greeny.Data;
using Greeny.Models;
using Greeny.Services.Interface;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Metrics;

namespace Greeny.Services
{
    public class DiscountService : IDiscountService
    {

        private readonly AppDbContext _context;

        public DiscountService(AppDbContext context)
        {
            _context = context;
        }


        public async Task CreateAsync(DiscountCreatVM discount)
        {
            Disocunt newDiscount = new()
            {
                Name = discount.Name,
                Discount=discount.Discount,

            };
            await _context.Disocunts.AddAsync(newDiscount);

            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            Disocunt disocunt = await GetByIdAsync(id);

            _context.Remove(disocunt);
            await _context.SaveChangesAsync();
        }

        public async Task EditAsync(DiscountEditVM discount)
        {
            Disocunt newDiscount = new()
            {
                Id = discount.Id,
                Name = discount.Name,
                Discount=discount.Discount,
            };

            _context.Update(newDiscount);

            await _context.SaveChangesAsync();
        }

        public async Task<List<Disocunt>> GetAllAsync()
        {
            return await _context.Disocunts.ToListAsync();
        }

        public async Task<List<DiscountVM>> GetAllMappedDatas()
        {
            List<DiscountVM> list = new();

            List<Disocunt> infos = await GetAllAsync();

            foreach (var info in infos)
            {
                DiscountVM model = new()
                {
                    Id = info.Id,
                    Name = info.Name,                  

                };

                list.Add(model);
            }

            return list;
        }

        public async Task<Disocunt> GetByIdAsync(int id)
        {
            return await _context.Disocunts.FirstOrDefaultAsync(m => m.Id == id);
        }

        public DiscountDetailVM GetMappedData(Disocunt discount)
        {
            DiscountDetailVM model = new()
            {
                Name = discount.Name,
                Discount=discount.Discount,
                CreatedDate = discount.CreatedDate.ToString("MM.dd.yyyy"),

            };

            return model;
        }
    }
}
