using Greeny.Models;
using Greeny.Services;
using Greeny.Services.Interface;
using Greeny.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Greeny.Controllers
{
    public class WishlistController : Controller
    {
        private readonly IWishlistService _wishlistService;
        private readonly IHttpContextAccessor _accessor;
        private readonly IProductService _productService;
        private readonly UserManager<AppUser> _userManager;

        public WishlistController (IHttpContextAccessor accessor,
                              IWishlistService wishlistService,
                              IProductService productService,
                              UserManager<AppUser> userManager)
        {


            _accessor = accessor;
            _wishlistService = wishlistService;
            _productService = productService;
            _userManager = userManager;
        }
        public async Task<IActionResult> Index()
        {
            List<WishlistDetailVM> wishList = new();

            if (_accessor.HttpContext.Request.Cookies["wishlist"] != null)
            {
                List<WishlistVM> wishlistDatas = JsonConvert.DeserializeObject<List<WishlistVM>>(_accessor.HttpContext.Request.Cookies["wishlist"]);
                foreach (var item in wishlistDatas)
                {
                    var dbProduct = await _productService.GetByIdWithImageAsnyc(item.Id);


                    if (dbProduct != null)
                    {
                        WishlistDetailVM wishlistDetail = new()
                        {
                            Id = dbProduct.Id,
                            Name = dbProduct.Name,
                            Image = dbProduct.Images.Where(m => m.IsMain).FirstOrDefault().Image,                           
                            Price = dbProduct.Price,
                            StockStatus=dbProduct.IsStock,
                            Description= dbProduct.Description,                          

                        };

                        wishList.Add(wishlistDetail);

                    }

                }

            }
            return View(wishList);
        }




        [HttpPost]
        [ActionName("Delete")]
        public async Task<IActionResult> DeleteProduct(int? id)
        {
            return Ok(await _wishlistService.DeleteProduct(id));
        }


        [HttpPost]
        public async Task<IActionResult> AddBasket(int? id)
        {
            if (id is null) return BadRequest();

            Product product = await _productService.GetByIdAsnyc(id);

            if (product is null) NotFound();

            List<WishlistVM> wishlist = _wishlistService.GetAll();

            _wishlistService.AddProduct(wishlist, product);

            return Ok(wishlist.Sum(m => m.Count));
        }

    }
}
