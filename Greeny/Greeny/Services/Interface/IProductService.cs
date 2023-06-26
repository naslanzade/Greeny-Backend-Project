using Greeny.Areas.Admin.ViewModels.Product;
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
        List<ViewModels.ProductVM> GetMappedDatas(List<Product> products);
        Task<List<Product>> GetAllBySearchText(string searchText);
        Task<Product> GetByIdAsnyc(int? id);
        Task<Product> GetByIdWithImageAsnyc(int? id);


        //For Admin Panel
        Task<List<Product>> PaginatedDatasAsync(int page, int take);
        List<Areas.Admin.ViewModels.Product.ProductVM> MappedDatas(List<Product> products);
        Task<int> CountAsync();
        Task<Product> GetWithIncludesAsync(int? id);
        Areas.Admin.ViewModels.Product.ProductDetailVM GetMappedData(Product product);
        Task CreateAsync(ProductCreateVM model);
        Task DeleteAsync(int id);




    }
}
