
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

        public HomeController(AppDbContext context,
                              ISliderService sliderService,
                              ICategoryService categoryService,
                              IBrandService brandService,
                              IAdvertService advertService)
        {
            _context = context;
            _sliderService = sliderService;
            _categoryService = categoryService;
            _brandService = brandService;
            _advertService = advertService;
        }

        public async Task<IActionResult> Index()
        {

            HomeVM homeVM = new()
            {
                Sliders = await _sliderService.GetAllAsync(),
                Categories = await _categoryService.GetAllAsync(),
                Brands=await _brandService.GetAllAsync(),
                Advert=await _advertService.GetFirstHomeAdvertAsync(),
                Adverts=await _advertService.GetAllAdvertForHomeAsync(),

            };

            return View(homeVM);
        }

      
    }
}