using Fiorello.Helpers;
using Greeny.Areas.Admin.ViewModels.AboutImage;
using Greeny.Areas.Admin.ViewModels.Advert;
using Greeny.Data;
using Greeny.Models;
using Greeny.Services.Interface;
using Microsoft.EntityFrameworkCore;

namespace Greeny.Services
{
    public class AdvertService : IAdvertService
    {
        private readonly IWebHostEnvironment _env;
        private readonly AppDbContext _context;

        public AdvertService(AppDbContext context, 
                            IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        public async Task CreateAsync(List<IFormFile> images)
        {
            foreach (var item in images)
            {
                string fileName = Guid.NewGuid().ToString() + "_" + item.FileName;

                await item.SaveFileAsync(fileName, _env.WebRootPath, "images/promo");

                Advert advert = new()
                {
                    Name = fileName,

                };

                await _context.Adverts.AddAsync(advert);
            }

            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            Advert advert = await GetByIdAsync(id);

            _context.Adverts.Remove(advert);

            await _context.SaveChangesAsync();

            string path = Path.Combine(_env.WebRootPath, "images/promo", advert.Name);

            if (File.Exists(path))
            {
                File.Delete(path);
            }
        }

        public async Task EditAsync(Advert image, IFormFile newImage)
        {
            string oldPath = Path.Combine(_env.WebRootPath, "images/promo", image.Name);

            if (File.Exists(oldPath))
            {
                File.Delete(oldPath);
            }

            string fileName = Guid.NewGuid().ToString() + "_" + newImage.FileName;

            await newImage.SaveFileAsync(fileName, _env.WebRootPath, "images/promo");

            image.Name = fileName;

            await _context.SaveChangesAsync();
        }

        public async Task<List<Advert>> GetAllAdvertForHomeAsync()
        {
            return await _context.Adverts.Skip(1).Take(2).ToListAsync();
        }

        public async Task<List<Advert>> GetAllAsync()
        {
            return await _context.Adverts.ToListAsync();
        }

        public async Task<List<AdvertVM>> GetAllMappedDatas()
        {
            List<AdvertVM> lits = new();

            List<Advert> infos = await GetAllAsync();

            foreach (var info in infos)
            {
                AdvertVM model = new()
                {
                    Id = info.Id,
                    Image=info.Name
                    
                };

                lits.Add(model);
            }

            return lits;
        }

        public async Task<Advert> GetByIdAsync(int id)
        {
            return await _context.Adverts.FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task<Advert> GetFirstHomeAdvertAsync()
        {
            return await _context.Adverts.FirstOrDefaultAsync();
        }

        public AdvertDetailVM GetMappedData(Advert info)
        {
            AdvertDetailVM model = new()
            {
                Image = info.Name,
                CreatedDate = info.CreatedDate.ToString("MM.dd.yyyy"),

            };

            return model;
        }
    }
}
