using Greeny.Models;
using Greeny.ViewModels;

namespace Greeny.Services.Interface
{
    public interface IProductService
    {
       
        Task<IEnumerable<Product>> GetNewProductsAsync();
        Task<IEnumerable<Product>> GetProductsByRatingAsync();
        Task<IEnumerable<Product>> GetProductsBySaleAsync();      
        Task<Product> GetProductDetailAsync(int? id);       
        Task<List<Product>> GetAllDatasAsync();
        Task<int> GetCountAsync();
        Task<List<Product>> GetPaginatedDatasAsync(int page, int take);
         List<ShopVM> GetMappedDatas(List<Product> products);







    }
}
