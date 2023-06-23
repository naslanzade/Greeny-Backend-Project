using Fiorello.Helpers;
using Greeny.Areas.Admin.ViewModels.Advert;
using Greeny.Areas.Admin.ViewModels.BgImage;
using Greeny.Data;
using Greeny.Models;
using Greeny.Services.Interface;
using Microsoft.EntityFrameworkCore;

namespace Greeny.Services
{
    public class BgImageService : IBgImageService
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;


        public BgImageService(AppDbContext context, 
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

                await item.SaveFileAsync(fileName, _env.WebRootPath, "images/");

                BgImage bgImage = new()
                {
                    Image = fileName,

                };

                await _context.BgImages.AddAsync(bgImage);
            }

            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            BgImage bgImage = await GetByIdAsync(id);

            _context.BgImages.Remove(bgImage);

            await _context.SaveChangesAsync();

            string path = Path.Combine(_env.WebRootPath, "images/", bgImage.Image);

            if (File.Exists(path))
            {
                File.Delete(path);
            }
        }

        public async Task EditAsync(BgImage image, IFormFile newImage)
        {
            string oldPath = Path.Combine(_env.WebRootPath, "images/", image.Image);

            if (File.Exists(oldPath))
            {
                File.Delete(oldPath);
            }

            string fileName = Guid.NewGuid().ToString() + "_" + newImage.FileName;

            await newImage.SaveFileAsync(fileName, _env.WebRootPath, "images/");

            image.Image = fileName;

            await _context.SaveChangesAsync();
        }

        public async Task<BgImage> GetAllAsync()
        {
            return await _context.BgImages.FirstOrDefaultAsync();
        }

        public async Task<List<BgImage>> GetAllDataAsync()
        {
            return await _context.BgImages.ToListAsync();
        }

        public async Task<List<BgImageVM>> GetAllMappedDatas()
        {
            List<BgImageVM> list = new();

            List<BgImage> infos = await GetAllDataAsync();

            foreach (var info in infos)
            {
                BgImageVM model = new()
                {
                    Id = info.Id,
                    Image = info.Image
                };

                list.Add(model);
            }

            return list;
        }

        public async Task<BgImage> GetByIdAsync(int id)
        {
            return await _context.BgImages.FirstOrDefaultAsync(m => m.Id == id);
        }

        public BgImageDetailVM GetMappedData(BgImage info)
        {
            BgImageDetailVM model = new()
            {
                Image = info.Image,
                CreatedDate = info.CreatedDate.ToString("MM.dd.yyyy"),

            };

            return model;
        }
    }
}
