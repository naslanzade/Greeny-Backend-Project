using Greeny.Areas.Admin.ViewModels.Discount;
using Greeny.Areas.Admin.ViewModels.Milestone;
using Greeny.Data;
using Greeny.Models;
using Greeny.Services;
using Greeny.Services.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Greeny.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class MilestoneController : Controller
    {

        private readonly IMilestoneService _milestoneService;
        private readonly AppDbContext _context;

        public MilestoneController(IMilestoneService milestoneService,
                                   AppDbContext context)
        {
            _context = context;
            _milestoneService = milestoneService;
        }
        public async Task<IActionResult> Index()
        {
            return View(await _milestoneService.GetAllMappedDatas());
        }


        [HttpGet]
        public async Task<IActionResult> Detail(int? id)
        {
            if (id is null) return BadRequest();

            Milestone dbMilestone = await _milestoneService.GetByIdAsync((int)id);
            if (dbMilestone is null) return NotFound();

            return View(_milestoneService.GetMappedData(dbMilestone));

        }



        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(MilestoneCreateVM request)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            await _milestoneService.CreateAsync(request);

            return RedirectToAction(nameof(Index));
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            await _milestoneService.DeleteAsync(id);

            return RedirectToAction(nameof(Index));
        }


        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id is null) return BadRequest();

            Milestone dbMilestone = await _milestoneService.GetByIdAsync((int)id);

            if (dbMilestone is null) return NotFound();

            return View(new MilestoneEditVM
            {
                Type = dbMilestone.Type,
                Counter = dbMilestone.Counter,

            });
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, MilestoneEditVM request)
        {
            if (id is null) return BadRequest();

            Milestone existMilestone = await _context.Milestones.AsNoTracking().FirstOrDefaultAsync(m => m.Id == id);

            if (existMilestone is null) return NotFound();

            if (existMilestone.Type.Trim() == request.Type)
            {
                return RedirectToAction(nameof(Index));
            }

            await _milestoneService.EditAsync(request);

            return RedirectToAction(nameof(Index));

        }


    }
}
