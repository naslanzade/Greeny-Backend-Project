using Fiorello.Helpers;
using Greeny.Areas.Admin.ViewModels.AboutImage;
using Greeny.Data;
using Greeny.Models;
using Greeny.Services.Interface;
using Microsoft.EntityFrameworkCore;

namespace Greeny.Services
{
    public class AboutImageService : IAboutImageService
    {

        private readonly IWebHostEnvironment _env;
        private readonly AppDbContext _context;


        public AboutImageService(AppDbContext context, 
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

                await item.SaveFileAsync(fileName, _env.WebRootPath, "images/about");

                AboutImage aboutImage = new()
                {
                    Images = fileName,
                  
                };

                await _context.AboutImages.AddAsync(aboutImage);

            }

            await _context.SaveChangesAsync();
        }       

        public async Task<List<AboutImage>> GetAllAsync()
        {
           return await _context.AboutImages.ToListAsync();
        }

        public async Task<List<AboutImageVM>> GetAllMappedDatas()
        {
            List<AboutImageVM> lits = new();

            List<AboutImage> infos= await GetAllAsync();

            foreach (var info in infos) 
            {
                AboutImageVM model = new()
                {
                    Id = info.Id,
                    Image = info.Images
                };

                lits.Add(model);
            }

            return lits;
        }

        public async Task<AboutImage> GetByIdAsync(int id)
        {
            return await _context.AboutImages.FirstOrDefaultAsync(m => m.Id == id);
        }

        public AboutImageDetailVM GetMappedData(AboutImage info)
        {
            AboutImageDetailVM model = new()
            {
                Image=info.Images,
                CreatedDate= info.CreatedDate.ToString("MM.dd.yyyy"),

            };

            return model;
        }

        public  async Task DeleteAsync(int id)
        {
            AboutImage aboutImg = await GetByIdAsync(id);

            _context.AboutImages.Remove(aboutImg);

            await _context.SaveChangesAsync();

            string path = Path.Combine(_env.WebRootPath, "images/about", aboutImg.Images);

            if (File.Exists(path))
            {
                File.Delete(path);
            }
        }

        public async Task EditAsync(AboutImage image, IFormFile newImage)
        {
            string oldPath = Path.Combine(_env.WebRootPath, "images/about", image.Images);

            if (File.Exists(oldPath))
            {
                File.Delete(oldPath);
            }

            string fileName = Guid.NewGuid().ToString() + "_" + newImage.FileName;

            await newImage.SaveFileAsync(fileName, _env.WebRootPath, "images/about");

            image.Images = fileName;

            await _context.SaveChangesAsync();
        }
    }

}
