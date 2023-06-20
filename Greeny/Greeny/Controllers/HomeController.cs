
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

        public HomeController(AppDbContext context,
                              ISliderService sliderService)
        {
            _context = context;
            _sliderService = sliderService;
        }

        public async Task<IActionResult> Index()
        {
            
            HomeVM homeVM = new()
            {
              Sliders = await _sliderService.GetAllAsync(),

            };

            return View(homeVM);
        }

      
    }
}