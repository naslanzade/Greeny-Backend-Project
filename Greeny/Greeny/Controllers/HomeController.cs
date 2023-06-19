
using Greeny.Data;
using Greeny.Models;
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

        public HomeController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            List<Slider> slider=await _context.Sliders.Where(m=>!m.SoftDeleted).ToListAsync();

            HomeVM homeVM = new()
            {
              Sliders = slider,

            };

            return View(homeVM);
        }

      
    }
}