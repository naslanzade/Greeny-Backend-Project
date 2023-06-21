using Greeny.Data;
using Greeny.Models;
using Greeny.Services.Interface;
using Greeny.ViewModels;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using static Org.BouncyCastle.Asn1.Cmp.Challenge;

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


        public Task<List<Product>> GetPaginatedDatasAsync(int page, int take)
        {
            throw new NotImplementedException();
        }

        public async Task<int> GetCountAsync()
        {
            return await _context.Products.CountAsync();
        }

        public List<ShopVM> GetMappedDatas(List<Product> products)
        {
            List<ShopVM> list = new();
            foreach (var product in products)
            {
                list.Add(new ShopVM
                {
                 
                 


                });

            }

            return list;
        }

    }
}
