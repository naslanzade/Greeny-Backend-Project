using Greeny.Areas.Admin.ViewModels.Country;
using Greeny.Areas.Admin.ViewModels.Discount;
using Greeny.Data;
using Greeny.Helpers;
using Greeny.Models;
using Greeny.Services;
using Greeny.Services.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Greeny.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class DiscountController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IDiscountService _discountService;


        public DiscountController(AppDbContext context,
                                 IDiscountService discountService)
        {
            _context = context;
            _discountService = discountService;
        }

        [Authorize(Roles = "SuperAdmin,Admin")]
        public async Task<IActionResult> Index()
        {
            return View(await _discountService.GetAllMappedDatas());
        }


        [HttpGet]
        [Authorize(Roles = "SuperAdmin,Admin")]
        public async Task<IActionResult> Detail(int? id)
        {
            if (id is null) return BadRequest();

            Disocunt dbDiscount = await _discountService.GetByIdAsync((int)id);
            if (dbDiscount is null) return NotFound();

            return View(_discountService.GetMappedData(dbDiscount));

        }



        [HttpGet]
        [Authorize(Roles = "SuperAdmin")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "SuperAdmin")]
        public async Task<IActionResult> Create(DiscountCreatVM request)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            await _discountService.CreateAsync(request);

            return RedirectToAction(nameof(Index));
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "SuperAdmin")]
        public async Task<IActionResult> Delete(int id)
        {
            await _discountService.DeleteAsync(id);

            return RedirectToAction(nameof(Index));
        }


        [HttpGet]
        [Authorize(Roles = "SuperAdmin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id is null) return BadRequest();

            Disocunt dbDiscount = await _discountService.GetByIdAsync((int)id);

            if (dbDiscount is null) return NotFound();

            return View(new DiscountEditVM
            {
                Name = dbDiscount.Name,
                Discount=dbDiscount.Discount,

            });
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "SuperAdmin")]
        public async Task<IActionResult> Edit(int? id, DiscountEditVM request)
        {
            if (id is null) return BadRequest();

            Disocunt existDiscount = await _context.Disocunts.AsNoTracking().FirstOrDefaultAsync(m => m.Id == id);

            if (existDiscount is null) return NotFound();

            if (existDiscount.Name.Trim() == request.Name)
            {
                return RedirectToAction(nameof(Index));
            }

            await _discountService.EditAsync(request);

            return RedirectToAction(nameof(Index));

        }



    }
}
