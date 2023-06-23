using Fiorello.Helpers;
using Greeny.Areas.Admin.ViewModels.Brand;
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
        private readonly IWebHostEnvironment _env;


        public BrandService(AppDbContext context, 
                           IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }


        //For Pagination and Brand Page
        public async Task<List<Brand>> GetAllAsync()
        {
            return await _context.Brands.Take(6).ToListAsync();
        }

        public List<ViewModels.BrandVM> GetMappedDatas(List<Brand> brands)
        {
            List<ViewModels.BrandVM> list = new();
            foreach (var brand in brands)
            {
                list.Add(new ViewModels.BrandVM
                {
                    Name = brand.Name,
                    Image = brand.Image,

                });

            }

            return list;
        }

        public async Task<int> GetCountAsync()
        {
            return await _context.Brands.CountAsync();
        }

        public async Task<List<Brand>> GetPaginatedDatasAsync(int page, int take)
        {
            return await _context.Brands.Skip((page - 1) * take)
                                          .Take(take)
                                          .ToListAsync();
        }




        //For AdminPanel
        public async Task CreateAsync(List<IFormFile> images, BrandCreateVM newInfo)
        {
            foreach (var item in images)
            {
                string fileName = Guid.NewGuid().ToString() + "_" + item.FileName;

                await item.SaveFileAsync(fileName, _env.WebRootPath, "images/brand");


                Brand brand = new()
                {
                    Image=fileName,
                    Name = newInfo.Name,
                };

                await _context.Brands.AddAsync(brand);

            }

            await _context.SaveChangesAsync();
        }        
        public async Task DeleteAsync(int id)
        {
            Brand brand = await GetByIdAsync(id);

            _context.Brands.Remove(brand);

            await _context.SaveChangesAsync();

            string path = Path.Combine(_env.WebRootPath, "images/brand", brand.Image);

            if (File.Exists(path))
            {
                File.Delete(path);
            }
        }

        public async Task EditAsync(BrandEditVM request, IFormFile newImage)
        {
            string oldPath = Path.Combine(_env.WebRootPath, "images/brand", newImage.FileName);

            if (File.Exists(oldPath))
            {
                File.Delete(oldPath);
            }

            string fileName = Guid.NewGuid().ToString() + "_" + newImage.FileName;

            await newImage.SaveFileAsync(fileName, _env.WebRootPath, "images/brand");

            request.Image = fileName;

            Brand brand = new()
            {
                Id = request.Id,
                Name=request.Name,
                Image = request.Image
            };

            _context.Update(brand);

            await _context.SaveChangesAsync();
        }
       
        public async Task<List<Brand>> GetAllDatasAsync()
        {
            return await _context.Brands.ToListAsync();
        }

        public async Task<List<Areas.Admin.ViewModels.Brand.BrandVM>> GetAllMappedDatas()
        {
            List<Areas.Admin.ViewModels.Brand.BrandVM> list = new();

            List<Brand> infos = await GetAllDatasAsync();

            foreach (Brand info in infos)
            {
                Areas.Admin.ViewModels.Brand.BrandVM model = new()
                {
                    Id = info.Id,
                    Name=info.Name,
                    Image=info.Image,
                };

                list.Add(model);
            }


            return list;
        }

        public async Task<Brand> GetByIdAsync(int id)
        {
            return await _context.Brands.FindAsync(id);
        }

        public BrandDetailVM GetMappedData(Brand info)
        {
            BrandDetailVM model = new()
            {            
                Id = info.Id,
                Name = info.Name,
                Image = info.Image,
                CreatedDate = info.CreatedDate.ToString("MM.dd.yyyy"),

            };
            return model;
        }


        //For AdminPanel Pagination
        public async Task<List<Brand>> PaginatedDatasAsync(int page, int take)
        {
            return await _context.Brands.Skip((page - 1) * take)
                                          .Take(take)
                                          .ToListAsync();
        }

        public List<Areas.Admin.ViewModels.Brand.BrandVM> MappedDatas(List<Brand> brands)
        {
            List<Areas.Admin.ViewModels.Brand.BrandVM> list = new();
            foreach (var brand in brands)
            {
                list.Add(new Areas.Admin.ViewModels.Brand.BrandVM
                {
                    Id=brand.Id,
                    Name = brand.Name,
                    Image = brand.Image,

                });

            }

            return list;
        }

        public async Task<int> CountAsync()
        {
            return await _context.Brands.CountAsync();
        }
    }
}
