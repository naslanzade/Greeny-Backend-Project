using Greeny.Data;
using Greeny.Models;
using Greeny.Services.Interface;
using Greeny.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace Greeny.Controllers
{
    public class CartController : Controller
    {
        private readonly IProductService _productService;
        private readonly IHttpContextAccessor _accessor;
        private readonly IBasketService _basketService;       
        private readonly UserManager<AppUser> _userManager;

        public CartController(IHttpContextAccessor accessor, 
                              IBasketService basketService, 
                              IProductService productService,                              
                              UserManager<AppUser> userManager)
        {


            _accessor = accessor;
            _basketService = basketService;
            _productService = productService;
            _userManager = userManager;
        }


        public async Task<IActionResult> Index()
        {
            AppUser user = await _userManager.GetUserAsync(User);

            if (user == null) return RedirectToAction("SignIn", "Account");

            List<BasketDetailVM> basketList = new();

            if (_accessor.HttpContext.Request.Cookies["basket"] != null)
            {
                List<BasketVM> basketDatas = JsonConvert.DeserializeObject<List<BasketVM>>(_accessor.HttpContext.Request.Cookies["basket"]);
                foreach (var item in basketDatas)
                {
                    var dbProduct = await _productService.GetByIdWithImageAsnyc(item.Id);


                    if (dbProduct != null)
                    {
                        BasketDetailVM basketDetail = new()
                        {
                            Id= dbProduct.Id,
                            Name = dbProduct.Name,
                            Image = dbProduct.Images.Where(m => m.IsMain).FirstOrDefault().Image,                           
                            Count = item.Count,
                            Price=dbProduct.Price,
                            TotalPrice= dbProduct.Price * item.Count,

                        };

                        basketList.Add(basketDetail);

                    }

                }

            }
            return View(basketList);
        }


        [HttpPost]
        [ActionName("Delete")]
        public async Task<IActionResult> DeleteProduct(int? id)
        {
            return Ok(await _basketService.DeleteProduct(id));
        }


        [HttpPost]
        public async Task<IActionResult> AddBasket(int? id)
        {
            var user = await _userManager.GetUserAsync(User);

            if (user == null) return RedirectToAction("SignIn", "Account");

            if (id is null) return BadRequest();

            Product product = await _productService.GetByIdAsnyc(id);

            if (product is null) NotFound();

            List<BasketVM> basket = _basketService.GetAll();

            _basketService.AddProduct(basket, product);

            return Ok(basket.Sum(m => m.Count));
        }
    }
}
