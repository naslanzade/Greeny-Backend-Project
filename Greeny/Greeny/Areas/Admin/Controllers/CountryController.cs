using Greeny.Areas.Admin.ViewModels.Author;
using Greeny.Areas.Admin.ViewModels.Country;
using Greeny.Data;
using Greeny.Models;
using Greeny.Services;
using Greeny.Services.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Greeny.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CountryController : Controller
    {

        private readonly ICountryService _countryService;
        private readonly AppDbContext _context;

        public CountryController(ICountryService countryService,
                                 AppDbContext context)
        {
            _countryService = countryService;
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            return View(await _countryService.GetAllMappedDatas());
        }


        [HttpGet]
        public async Task<IActionResult> Detail(int? id)
        {
            if (id is null) return BadRequest();

            Country dbCountry = await _countryService.GetByIdAsync((int)id);
            if (dbCountry is null) return NotFound();

            return View(_countryService.GetMappedData(dbCountry));

        }


        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CountryCreateVM request)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            await _countryService.CreateAsync(request);

            return RedirectToAction(nameof(Index));
        }



        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id is null) return BadRequest();

            Country dbCountry = await _countryService.GetByIdAsync((int)id);

            if (dbCountry is null) return NotFound();

            return View(new CountryEditVM
            {
                Name = dbCountry.Name,

            });
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, CountryEditVM request)
        {
            if (id is null) return BadRequest();

            Country existCountry = await _context.Countries.AsNoTracking().FirstOrDefaultAsync(m => m.Id == id);

            if (existCountry is null) return NotFound();

            if (existCountry.Name.Trim() == request.Name)
            {
                return RedirectToAction(nameof(Index));
            }

            await _countryService.EditAsync(request);

            return RedirectToAction(nameof(Index));

        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            await _countryService.DeleteAsync(id);

            return RedirectToAction(nameof(Index));
        }
    }
}
