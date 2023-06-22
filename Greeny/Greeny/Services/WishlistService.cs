using Greeny.Data;
using Greeny.Models;
using Greeny.Responses;
using Greeny.Services.Interface;
using Greeny.ViewModels;
using Newtonsoft.Json;

namespace Greeny.Services
{
    public class WishlistService : IWishlistService
    {
        private readonly AppDbContext _context;
        private readonly IHttpContextAccessor _accessor;
        private readonly IProductService _productService;

        public WishlistService(AppDbContext context,
                             IHttpContextAccessor accessor,
                             IProductService productService)
        {
            _context = context;
            _accessor = accessor;
            _productService = productService;
        }

        public void AddProduct(List<WishlistVM> wishlist, Product product)
        {
            WishlistVM existProduct = wishlist.FirstOrDefault(m => m.Id == product.Id);

            if (existProduct is null)
            {
                wishlist.Add(new WishlistVM
                {
                    Id = product.Id,
                    Count = 1
                });
            }
            else
            {
                existProduct.Count++;
            }

            _accessor.HttpContext.Response.Cookies.Append("wishlist", JsonConvert.SerializeObject(wishlist));
        }

        public async Task<WishlistDeleteResponse> DeleteProduct(int? id)
        {
            List<WishlistVM> wishlistDatas = JsonConvert.DeserializeObject<List<WishlistVM>>(_accessor.HttpContext.Request.Cookies["wishlist"]);

            var data = wishlistDatas.FirstOrDefault(m => m.Id == id);

            wishlistDatas.Remove(data);

            _accessor.HttpContext.Response.Cookies.Append("wishlist", JsonConvert.SerializeObject(wishlistDatas));

            decimal total = 0;
            foreach (var wishlistdata in wishlistDatas)
            {

                Product dbProduct = await _productService.GetByIdAsnyc(wishlistdata.Id);
                total += (dbProduct.Price * wishlistdata.Count);

            }
            int count = wishlistDatas.Sum(m => m.Count);


            return new WishlistDeleteResponse { Count = count, Total = total };
        }

        public List<WishlistVM> GetAll()
        {
            List<WishlistVM> wishlist;

            if (_accessor.HttpContext.Request.Cookies["wishlist"] != null)
            {
                wishlist = JsonConvert.DeserializeObject<List<WishlistVM>>(_accessor.HttpContext.Request.Cookies["wishlist"]);
            }
            else
            {
                wishlist = new List<WishlistVM>();
            }

            return wishlist;
        }

        public int GetCount()
        {
            List<WishlistVM> wishlist;

            if (_accessor.HttpContext.Request.Cookies["wishlist"] != null)
            {
                wishlist = JsonConvert.DeserializeObject<List<WishlistVM>>(_accessor.HttpContext.Request.Cookies["wishlist"]);
            }
            else
            {
                wishlist = new List<WishlistVM>();
            }

            return wishlist.Sum(m => m.Count);
        }
    }
}
