
using Greeny.Data;
using Greeny.Models;
using Greeny.Services.Interface;
using Greeny.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Composition;
using System.Diagnostics;
using System.Reflection.Metadata;

namespace Greeny.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _context;
        private readonly ISliderService _sliderService;
        private readonly ICategoryService _categoryService;
        private readonly IBrandService _brandService;
        private readonly IAdvertService _advertService;
        private readonly IProductService _productService;
        private readonly IBlogService _blogService;

        public HomeController(AppDbContext context,
                              ISliderService sliderService,
                              ICategoryService categoryService,
                              IBrandService brandService,
                              IAdvertService advertService,
                              IProductService productService,
                              IBlogService blogService)
        {
            _context = context;
            _sliderService = sliderService;
            _categoryService = categoryService;
            _brandService = brandService;
            _advertService = advertService;
            _productService = productService;
            _blogService = blogService;
        }

        public async Task<IActionResult> Index()
        {          
            int count = await _context.Products.Include(m => m.Images).
                                                Include(m => m.Disocunt).
                                                Include(m => m.SubCategory).
                                                Include(m => m.Category).
                                                Include(m => m.Brand).
                                                Include(m => m.ProductTags).
                                                ThenInclude(m => m.Tag).
                                                CountAsync();
            ViewBag.count = count;

            HomeVM homeVM = new()
            {                
                Sliders = await _sliderService.GetAllAsync(),
                Categories = await _categoryService.GetAllAsync(),
                Brands=await _brandService.GetAllAsync(),
                Advert=await _advertService.GetFirstHomeAdvertAsync(),
                Adverts=await _advertService.GetAllAdvertForHomeAsync(),
                ProductByDate=await _productService.GetNewProductsAsync(),
                ProductByRate=await _productService.GetProductsByRatingAsync(),
                ProductBySale=await _productService.GetProductsBySaleAsync(),
                BlogsByDate=await _blogService.GetAllAsync(),

            };

            return View(homeVM);
        }

        [HttpGet]
        public async Task<IActionResult> ShowMoreOrLess(int skip)
        {
            IEnumerable<Product> products = await _context.Products.Include(m => m.Images).
                                                             Include(m => m.Disocunt).
                                                             Include(m => m.SubCategory).
                                                             Include(m => m.Category).
                                                             Include(m => m.Brand).
                                                             Include(m => m.ProductTags).
                                                             ThenInclude(m => m.Tag).
                                                             Skip(skip).Take(10).
                                                             ToListAsync();

            return PartialView("_ProductsByDatePartial", products);


        }

        [HttpGet]
        public async Task<IActionResult> MoreOrLess(int skip)
        {
            IEnumerable<Product> products = await _context.Products.Include(m => m.Images).
                                                             Include(m => m.Disocunt).
                                                             Include(m => m.SubCategory).
                                                             Include(m => m.Category).
                                                             Include(m => m.Brand).
                                                             Include(m => m.ProductTags).
                                                             ThenInclude(m => m.Tag).
                                                             Skip(skip).Take(6).
                                                             ToListAsync();

            return PartialView("_ProductsByRatePartial", products);


        }


    }
}