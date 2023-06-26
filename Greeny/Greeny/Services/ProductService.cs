using Fiorello.Helpers;
using Greeny.Areas.Admin.ViewModels.Product;
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
        private readonly IWebHostEnvironment _env;


        public ProductService(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
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

        public List<ViewModels.ProductVM> GetMappedDatas(List<Product> products)
        {
            List<ViewModels.ProductVM> list = new();

            foreach (var product in products)
            {
                list.Add(new ViewModels.ProductVM
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






        //AdminProductPagination

        public async Task<List<Product>> PaginatedDatasAsync(int page, int take)
        {
            return await _context.Products.Include(m => m.Images)
                                         .Include(m => m.Category)
                                         .Include(m=>m.SubCategory)
                                         .Include(m=>m.Brand)
                                         .Include(m=>m.Disocunt)
                                         .Include(m=>m.ProductTags)
                                         .ThenInclude(m=>m.Tag)
                                         .Skip((page - 1) * take)
                                         .Take(take)
                                         .ToListAsync();
        }

        public List<Areas.Admin.ViewModels.Product.ProductVM> MappedDatas(List<Product> products)
        {
            List<Areas.Admin.ViewModels.Product.ProductVM> list = new();
            foreach (var product in products)
            {
                list.Add(new Areas.Admin.ViewModels.Product.ProductVM
                {
                    Id = product.Id,
                    Name = product.Name,                    
                    Images = product.Images.Where(m => m.IsMain).FirstOrDefault().Image,                    
                    Price = product.Price,
                    SkuCode=product.SkuCode,
                    IsStock=product.IsStock,
                    RateCount=product.RateCount,
                    SaleCount=product.SaleCount,
                    ProductCount=product.ProductCount,
                    BrandName=product.Brand.Name,
                    CategoryName=product.Category.Name,
                });

            }

            return list;
        }

        public async Task<int> CountAsync()
        {
            return await _context.Products.CountAsync();
        }

        public async Task<Product> GetWithIncludesAsync(int? id)
        {
            return await _context.Products.Include(m => m.Images)
                                          .Include(m => m.Category)
                                         .Include(m => m.SubCategory)
                                         .Include(m => m.Brand)
                                         .Include(m => m.Disocunt)
                                         .Include(m => m.ProductTags)
                                         .ThenInclude(m => m.Tag)
                                          .FirstOrDefaultAsync(m => m.Id == id);
        }

        public Areas.Admin.ViewModels.Product.ProductDetailVM GetMappedData(Product product)
        {
            return new Areas.Admin.ViewModels.Product.ProductDetailVM
            {

                Name = product.Name,
                Price = product.Price,
                CategoryName = product.Category.Name,
                CreatedDate = product.CreatedDate.ToString("MMMM dd, yyyy"),
                Images = product.Images.Select(m => m.Image),
                BrandName = product.Brand.Name,
                DiscountName=product.Disocunt.Name,
                SubCategoryName=product.SubCategory.Name,
                SkuCode=product.SkuCode,
                RateCount=product.RateCount,
                SaleCount=product.SaleCount,
                ProductCount=product.ProductCount,
                TagName= (IEnumerable<string>)product.ProductTags.Select(t=>t.Tag),

            };
        }

        public async Task CreateAsync(ProductCreateVM model)
        {
            List<ProductImage> images = new();

            foreach (var item in model.Images)
            {
                string fileName = Guid.NewGuid().ToString() + "_" + item.FileName;
                await item.SaveFileAsync(fileName, _env.WebRootPath, "images/product");
                images.Add(new ProductImage { Image = fileName });
            }

            images.FirstOrDefault().IsMain = true;

            Product product = new()
            {
                Name = model.Name,
                Description = model.Description,
                Price = model.Price,
                CategoryId = model.CategoryId,
                Images = images,
                BrandId=model.BrandId,
                DiscountId=model.DiscountId,
                SaleCount=model.SaleCount,
                RateCount=model.RateCount,
                ProductCount=model.ProductCount,
                SkuCode=model.SkuCode,
                SubCategoryId=model.SubCategoryId,
                
            };

            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();
            foreach (var id in model.TagId)
            {
                ProductTag productTag = new()
                {
                    Product = product,
                    Tag = await _context.Tags.FirstOrDefaultAsync(m => m.Id == id),
                    ProductId = product.Id,
                    TagId = id
                };
                await _context.ProductTags.AddAsync(productTag);
                await _context.SaveChangesAsync();
            }
        }

        public async Task DeleteAsync(int id)
        {
            var product = await _context.Products.Include(m => m.Images)
                                          .Include(m => m.Category)
                                         .Include(m => m.SubCategory)
                                         .Include(m => m.Brand)
                                         .Include(m => m.Disocunt)
                                         .Include(m => m.ProductTags)
                                         .ThenInclude(m => m.Tag).FirstOrDefaultAsync(m => m.Id == id);

            _context.Products.Remove(product);

            await _context.SaveChangesAsync();

            foreach (var item in product.Images)
            {
                string path = Path.Combine(_env.WebRootPath, "images/product", item.Image);

                if (File.Exists(path))
                {
                    File.Delete(path);
                }
            }
        }
    }
}
