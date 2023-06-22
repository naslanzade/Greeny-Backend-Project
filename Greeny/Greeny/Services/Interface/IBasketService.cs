using Greeny.Data;
using Greeny.Models;
using Greeny.Responses;
using Greeny.ViewModels;

namespace Greeny.Services.Interface
{
    public interface IBasketService
    {
        List<BasketVM> GetAll();
        void AddProduct(List<BasketVM> basket, Product product);
        Task<BasketDeleteResponse> DeleteProduct(int? id);
        int GetCount();


    }
}
