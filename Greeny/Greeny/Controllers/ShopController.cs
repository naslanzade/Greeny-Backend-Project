using Fiorello.Helpers;
using Greeny.Models;
using Greeny.Services.Interface;
using Greeny.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Greeny.Controllers
{
    public class ShopController : Controller
    {

        private readonly IProductService _productService;
        private readonly IBrandService _brandService;
        private readonly ITagService _tagService;
        private readonly ICategoryService _categoryService;
        private readonly ISubCategoryService _subCategoryService;
        private readonly IBgImageService _bgImageService;

        public ShopController(IProductService productService,
                              IBrandService brandService,
                              ITagService tagService,
                              ICategoryService categoryService,
                              ISubCategoryService subCategoryService,
                              IBgImageService bgImageService)
        {
            _productService = productService;
            _brandService = brandService;
            _tagService = tagService;
            _categoryService = categoryService;
            _subCategoryService = subCategoryService;
            _bgImageService = bgImageService;

        }

        public async Task<IActionResult> Index()
        {           

            return View();
        }



        
    }
}
