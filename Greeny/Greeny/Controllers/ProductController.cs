using Greeny.Data;
using Greeny.Models;
using Greeny.Services.Interface;
using Greeny.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Greeny.Controllers
{
    public class ProductController : Controller
    {
               
        private readonly IProductService _productService;
    

        public ProductController(IProductService productService,IBgImageService bgImageService)
        {
            
            _productService = productService;
          
        }



        public async Task<IActionResult> Index(int? id)
        {
            if (id is null) return BadRequest();

            Product product = await _productService.GetProductDetailAsync(id);

            if (product is null) return NotFound();

            ProductDetailVM model = new()
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                BrandName = product.Brand.Name,
                ActualPrice = product.Price,
                DiscountPrice = product.Price - (product.Price * product.Disocunt.Discount) / 100,
                Images = product.Images.ToList(),
                Tags = product.ProductTags.ToList(),
                SkuCode = product.SkuCode,
               
            };


            return View(model);
        }
    }
}
