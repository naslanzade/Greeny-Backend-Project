using Fiorello.Helpers;
using Greeny.Areas.Admin.ViewModels.Brand;
using Greeny.Areas.Admin.ViewModels.Category;
using Greeny.Data;
using Greeny.Models;
using Greeny.Services.Interface;
using Microsoft.EntityFrameworkCore;

namespace Greeny.Services
{
    public class CategoryService : ICategoryService
    {

        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;

        public CategoryService( AppDbContext context, 
                                IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }      

        public async Task CreateAsync(List<IFormFile> images, CategoryCreateVM newInfo)
        {
            foreach (var item in images)
            {
                string fileName = Guid.NewGuid().ToString() + "_" + item.FileName;

                await item.SaveFileAsync(fileName, _env.WebRootPath, "images/suggest");


                Category category = new()
                {
                    Image = fileName,
                    Name = newInfo.Name,
                };

                await _context.Categories.AddAsync(category);

            }

            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            Category category = await GetByIdAsync(id);

            _context.Categories.Remove(category);

            await _context.SaveChangesAsync();

            string path = Path.Combine(_env.WebRootPath, "images/suggest", category.Image);

            if (File.Exists(path))
            {
                File.Delete(path);
            }
        }

        public async Task EditAsync(CategoryEditVM request, IFormFile newImage)
        {
            string oldPath = Path.Combine(_env.WebRootPath, "images/suggest", newImage.FileName);

            if (File.Exists(oldPath))
            {
                File.Delete(oldPath);
            }

            string fileName = Guid.NewGuid().ToString() + "_" + newImage.FileName;

            await newImage.SaveFileAsync(fileName, _env.WebRootPath, "images/suggest");

            request.Image = fileName;

            Category category = new()
            {
                Id = request.Id,
                Name = request.Name,
                Image = request.Image
            };

            _context.Update(category);

            await _context.SaveChangesAsync();
        }

        public async Task<List<Category>> GetAllAsync()
        {
            return await _context.Categories.ToListAsync();
        }
        
        public async Task<Category> GetByIdAsync(int id)
        {
            return await _context.Categories.FindAsync(id);
        }

        public CategoryDetailVM GetMappedData(Category info)
        {
            CategoryDetailVM model = new()
            {
                Id = info.Id,
                Name = info.Name,
                Image = info.Image,
                CreatedDate = info.CreatedDate.ToString("MM.dd.yyyy"),

            };
            return model;
        }

        public async Task<List<Category>> PaginatedDatasAsync(int page, int take)
        {
            return await _context.Categories.Skip((page - 1) * take)
                                         .Take(take)
                                         .ToListAsync();
        }

        public async Task<int> CountAsync()
        {
            return await _context.Categories.CountAsync();
        }

        public List<CategoryVM> MappedDatas(List<Category> categories)
        {
            List<CategoryVM> list = new();
            foreach (var item in categories)
            {
                list.Add(new CategoryVM
                {
                    Id = item.Id,
                    Name = item.Name,
                    Image = item.Image,

                });

            }

            return list;
        }
    }
}
