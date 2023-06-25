using Greeny.Data;
using Greeny.Models;
using Greeny.Services.Interface;
using Greeny.ViewModels;
using Microsoft.EntityFrameworkCore;


namespace Greeny.Services
{
    public class ProductService : IProductService
    {
        private readonly AppDbContext _context;

        public ProductService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Product>> GetAllDatasAsync()
        {
            return await _context.Products.Include(m => m.Images).
                                          Include(m => m.Disocunt).
                                          Include(m => m.SubCategory).
                                          Include(m => m.Category).
                                          Include(m => m.Brand).
                                          Include(m => m.ProductTags).
                                          ThenInclude(m => m.Tag).
                                          ToListAsync();
        }   

        public async Task<IEnumerable<Product>> GetNewProductsAsync()
        {
            return await _context.Products.Include(m => m.Images).
                                           Include(m => m.Disocunt).
                                           Include(m => m.SubCategory).
                                           Include(m => m.Category).
                                           Include(m => m.Brand).
                                           Include(m => m.ProductTags).
                                           ThenInclude(m => m.Tag).
                                           Take(10).
                                           OrderByDescending(m=>m.CreatedDate).
                                           ToListAsync();
        }      

        public async Task<Product> GetProductDetailAsync(int? id)
        {
            return await _context.Products.Include(m => m.Images).
                                        Include(m => m.Disocunt).
                                        Include(m => m.SubCategory).
                                        Include(m => m.Category).
                                        Include(m => m.Brand).
                                        Include(m => m.ProductTags).
                                        ThenInclude(m => m.Tag).                                       
                                        FirstOrDefaultAsync(m=>m.Id==id);
        }        

        public async Task<IEnumerable<Product>> GetProductsByRatingAsync()
        {
            return await _context.Products.Include(m => m.Images).
                                           Include(m => m.Disocunt).
                                           Include(m => m.SubCategory).
                                           Include(m => m.Category).
                                           Include(m => m.Brand).
                                           Include(m => m.ProductTags).
                                           ThenInclude(m => m.Tag).
                                           Take(6).
                                           OrderByDescending(m => m.RateCount).
                                           ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetProductsBySaleAsync()
        {
            return await _context.Products.Include(m => m.Images).
                                          Include(m => m.Disocunt).
                                          Include(m => m.SubCategory).
                                          Include(m => m.Category).
                                          Include(m => m.Brand).
                                          Include(m => m.ProductTags).
                                          ThenInclude(m => m.Tag).
                                          Take(10).
                                          OrderByDescending(m => m.SaleCount).
                                          ToListAsync();
        }


        public async Task<List<Product>> GetPaginatedDatasAsync(int page, int take)
        {
            return await _context.Products.Skip((page - 1) * take)
                                          .Take(take)
                                          .ToListAsync();
        }

        public async Task<int> GetCountAsync()
        {
            return await _context.Products.CountAsync();
        }

        public List<ProductVM> GetMappedDatas(List<Product> products)
        {
            List<ProductVM> list = new();

            foreach (var product in products)
            {
                list.Add(new ProductVM
                {
                 
                    Id = product.Id,
                    Name = product.Name,
                    Description = product.Description,
                    Price=product.Price,
                    Image =product.Images?.Where(m=>m.IsMain).FirstOrDefault()?.Image,
                    Products= _context.Products.Include(m=>m.Images).ToList(),

                });

            }

            return list;
        }

        public async Task<List<Product>> GetAllBySearchText(string searchText)
        {
            var products = await _context.Products.Include(p => p.Images)                                                  
                                                  .Where(p => p.Name.ToLower().Contains(searchText.ToLower()))
                                                  .ToListAsync();
            return products;
        }

        public async Task<Product> GetByIdAsnyc(int? id)
        {
            return await _context.Products.FindAsync(id);
        }

        public async Task<Product> GetByIdWithImageAsnyc(int? id)
        {
            return await _context.Products.Include(m => m.Images).FirstOrDefaultAsync(m => m.Id == id);
        }
    }
}
