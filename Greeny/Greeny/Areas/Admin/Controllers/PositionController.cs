using Greeny.Areas.Admin.ViewModels.Author;
using Greeny.Areas.Admin.ViewModels.Position;
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
    public class PositionController : Controller
    {

        private readonly IPositionService _positionService;
        private readonly AppDbContext _context;

        public PositionController(IPositionService positionService,
                                  AppDbContext context)
        {
            _context = context;
            _positionService = positionService;
        }

        [Authorize(Roles = "SuperAdmin,Admin")]
        public async Task<IActionResult> Index()
        {
            return View(await _positionService.GetAllMappedDatas());
        }


        [HttpGet]
        [Authorize(Roles = "SuperAdmin,Admin")]
        public async Task<IActionResult> Detail(int? id)
        {
            if (id is null) return BadRequest();

            Position dbPosition = await _positionService.GetByIdAsync((int)id);
            if (dbPosition is null) return NotFound();

            return View(_positionService.GetMappedData(dbPosition));

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
        public async Task<IActionResult> Create(PositionCreateVM request)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            await _positionService.CreateAsync(request);

            return RedirectToAction(nameof(Index));
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "SuperAdmin")]
        public async Task<IActionResult> Delete(int id)
        {
            await _positionService.DeleteAsync(id);

            return RedirectToAction(nameof(Index));
        }



        [HttpGet]
        [Authorize(Roles = "SuperAdmin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id is null) return BadRequest();

            Position dbPosition = await _positionService.GetByIdAsync((int)id);

            if (dbPosition is null) return NotFound();

            return View(new PositionEditVM
            {
                Name = dbPosition.Name,

            });
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "SuperAdmin")]
        public async Task<IActionResult> Edit(int? id, PositionEditVM request)
        {
            if (id is null) return BadRequest();

            Position existPosition = await _context.Positions.AsNoTracking().FirstOrDefaultAsync(m => m.Id == id);

            if (existPosition is null) return NotFound();

            if (existPosition.Name.Trim() == request.Name)
            {
                return RedirectToAction(nameof(Index));
            }

            await _positionService.EditAsync(request);

            return RedirectToAction(nameof(Index));

        }













    }
}
