using Greeny.Areas.Admin.ViewModels.Author;
using Greeny.Areas.Admin.ViewModels.SubCategory;
using Greeny.Data;
using Greeny.Models;
using Greeny.Services.Interface;
using Greeny.ViewModels;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;

namespace Greeny.Services
{
    public class SubCategoryService : ISubCategoryService
    {

        private readonly AppDbContext _context;

        public SubCategoryService(AppDbContext context)
        {
            _context = context;
        }

        public async Task CreateAsync(SubCategoryCreateVM model)
        {
            SubCategory newSubcategory = new()
            {
                Name = model.Name,               
                CategoryId = model.CategoryId,
            };

            await _context.SubCategories.AddAsync(newSubcategory);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            SubCategory subCategory =await GetByIdAsnyc(id);

            _context.Remove(subCategory);
            await _context.SaveChangesAsync();
        }

        public async Task EditAsync(int subCategoryId,SubCategoryEditVM model)
        {
            var subCategory = await GetByIdAsnyc(subCategoryId);

            subCategory.Name = model.Name;
            subCategory.CategoryId = model.CategoryId;
            await _context.SaveChangesAsync();
        }

        public async Task<List<SubCategory>> GetAllAsync()
        {
            return await _context.SubCategories.ToListAsync();
        }

        public async Task<SubCategory> GetByIdAsnyc(int? id)
        {
            return await _context.SubCategories.FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task<int> GetCountAsync()
        {
            return await _context.SubCategories.CountAsync();
        }

        public SubCategoryDetailVM GetMappedData(SubCategory subCategory)
        {
            return new SubCategoryDetailVM
            {
                Name = subCategory.Name,
                CategoryName = subCategory.Category.Name,
                CreatedDate = subCategory.CreatedDate.ToString("MMMM dd, yyyy"),
                

            };
        }

        public List<SubCategoryVM> GetMappedDatas(List<SubCategory> subCategories)
        {
            List<SubCategoryVM> list = new();
            foreach (var item in subCategories)
            {
                list.Add(new SubCategoryVM
                {
                    Id = item.Id,
                    Name = item.Name,                  
                    CategoryName = item.Category.Name,
                });

            }

            return list;
        }

        public async Task<List<SubCategory>> GetPaginatedDatasAsync(int page, int take)
        {
            return await _context.SubCategories.Include(m => m.Category)
                                          .Skip((page - 1) * take)
                                          .Take(take)
                                          .ToListAsync();
        }

        public async Task<SubCategory> GetWithIncludes(int? id)
        {
            return await _context.SubCategories.Where(m => m.Id == id)
                                               .Include(m => m.Category)
                                                .FirstOrDefaultAsync();
        }
    }
}
