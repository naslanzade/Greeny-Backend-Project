using Fiorello.Helpers;
using Greeny.Areas.Admin.ViewModels.Category;
using Greeny.Areas.Admin.ViewModels.Milestone;
using Greeny.Areas.Admin.ViewModels.Slider;
using Greeny.Data;
using Greeny.Models;
using Greeny.Services.Interface;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Crypto.Operators;

namespace Greeny.Services
{
    public class SliderService : ISliderService
    {

        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;

        public SliderService(AppDbContext context,
                            IWebHostEnvironment env)
        {
         
            _context = context;
            _env = env;
        }

        public async Task CreateAsync(List<IFormFile> images, SliderCreateVM newInfo)
        {
            foreach (var item in images)
            {
                string fileName = Guid.NewGuid().ToString() + "_" + item.FileName;

                await item.SaveFileAsync(fileName, _env.WebRootPath, "images/home/index");


                Slider slider = new()
                {
                    SliderImage = fileName,
                    Title = newInfo.Title,
                    Description=newInfo.Description,
                };

                await _context.Sliders.AddAsync(slider);

            }

            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            Slider slider = await GetByIdAsync(id);

            _context.Sliders.Remove(slider);

            await _context.SaveChangesAsync();

            string path = Path.Combine(_env.WebRootPath, "images/home/index", slider.SliderImage);

            if (File.Exists(path))
            {
                File.Delete(path);
            }
        }

        public async Task EditAsync(SliderEditVM request, IFormFile newImage)
        {
            string oldPath = Path.Combine(_env.WebRootPath, "images/home/index", newImage.FileName);

            if (File.Exists(oldPath))
            {
                File.Delete(oldPath);
            }

            string fileName = Guid.NewGuid().ToString() + "_" + newImage.FileName;

            await newImage.SaveFileAsync(fileName, _env.WebRootPath, "images/home/index");

            request.SliderImage = fileName;

            Slider slider = new()
            {
                Id = request.Id,
                Title = request.Title,
                Description = request.Description,
                SliderImage = request.SliderImage
            };

            _context.Update(slider);

            await _context.SaveChangesAsync();
        }

        public async Task<List<Slider>> GetAllAsync()
        {
            return await _context.Sliders.ToListAsync();
        }

        public async Task<Slider> GetByIdAsync(int id)
        {
            return await _context.Sliders.FindAsync(id);
        }

        public SliderDetailVM GetMappedData(Slider info)
        {
            SliderDetailVM model = new()
            {
                Id = info.Id,
                Title = info.Title,
                SliderImage = info.SliderImage,
                Description=info.Description,
                CreatedDate = info.CreatedDate.ToString("MM.dd.yyyy"),

            };
            return model;
        }

        public async Task<List<SliderVM>> MappedDatas()
        {
            List<SliderVM> list = new();

            List<Slider> infos = await GetAllAsync();

            foreach (var info in infos)
            {
                SliderVM model = new()
                {
                    Id = info.Id,
                    Title = info.Title,
                    SliderImage= info.SliderImage,
                };

                list.Add(model);
            }

            return list;
        }
    }
}
