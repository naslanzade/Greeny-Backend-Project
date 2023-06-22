using Greeny.Models;
using Greeny.Responses;
using Greeny.ViewModels;

namespace Greeny.Services.Interface
{
    public interface IWishlistService
    {
        List<WishlistVM> GetAll();
        void AddProduct(List<WishlistVM> wishlist, Product product);
        Task<WishlistDeleteResponse> DeleteProduct(int? id);
        int GetCount();

    }
}
