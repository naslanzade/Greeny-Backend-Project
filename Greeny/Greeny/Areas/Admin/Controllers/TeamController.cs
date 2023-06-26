using Fiorello.Helpers;
using Greeny.Areas.Admin.ViewModels.Team;
using Greeny.Data;
using Greeny.Helpers;
using Greeny.Models;
using Greeny.Services.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Greeny.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class TeamController : Controller
    {
        private readonly ITeamService _teamService;
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;
        private readonly IPositionService _positionService;


        public TeamController(ITeamService teamService,
                                  AppDbContext context,
                                  IWebHostEnvironment env,
                                  IPositionService positionService)
        {

            _teamService = teamService;
            _context = context;
            _env = env;
            _positionService = positionService;
        }

        [Authorize(Roles = "SuperAdmin,Admin")]
        public async Task<IActionResult> Index()
        {
            return View(await _teamService.GetMappedDatas());
        }


        [HttpGet]
        [Authorize(Roles = "SuperAdmin,Admin")]
        public async Task<IActionResult> Detail(int? id)
        {
            if (id is null) return BadRequest();

            Team team = await _teamService.GetWithIncludes(id);
            if (team == null) return NotFound();

            return View(_teamService.GetMappedData(team));


        }



        private async Task<SelectList> GetPositions()
        {
            List<Position> positions = await _positionService.GetAllAsync();
            return new SelectList(positions, "Id", "Name");
        }

        private async Task GetPosition()
        {
            ViewBag.positions = await GetPositions();

        }



        [HttpGet]
        [Authorize(Roles = "SuperAdmin")]
        public async Task<IActionResult> Create()
        {
            await GetPosition();
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "SuperAdmin")]
        public async Task<IActionResult> Create(TeamCreateVM request)
        {
            await GetPosition();

            if (!ModelState.IsValid)
            {
                return View();
            }

            foreach (var item in request.Images)
            {

                if (!item.CheckFileType("image/"))
                {
                    ModelState.AddModelError("image", "Please select only image file");
                    return View();
                }

                if (item.CheckFileSize(200))
                {
                    ModelState.AddModelError("image", "Image size must be max 200KB");
                    return View();
                }
            }

            await _teamService.CreateAsync(request, request.Images);
            return RedirectToAction(nameof(Index));
        }


        [HttpGet]
        [Authorize(Roles = "SuperAdmin")]
        public async Task<IActionResult> Edit(int? id)
        {
            await GetPosition();

            if (id is null) return BadRequest();

            Team team = await _teamService.GetWithIncludes(id);

            if (team is null) return NotFound();

            TeamEditVM response = new()
            {
                FullName = team.FullName,
                Image = team.Image,
                PositionId = team.PositionId,
            };

            return View(response);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "SuperAdmin")]
        public async Task<IActionResult> Edit(int? id, TeamEditVM request)
        {
            if (id is null) return BadRequest();

            Team existTeam = await _teamService.GetWithIncludes(id);

            if (existTeam is null) return NotFound();

            if (existTeam.FullName.Trim() == request.FullName)
            {
                return RedirectToAction(nameof(Index));
            }

            if (!request.NewImage.CheckFileType("image/"))
            {
                ModelState.AddModelError("NewImage", "Please select only image file");
                request.Image = existTeam.Image;
                return View(request);
            }

            if (request.NewImage.CheckFileSize(200))
            {
                ModelState.AddModelError("NewImage", "Image size must be max 200KB");
                request.Image = existTeam.Image;
                return View(request);
            }

            await _teamService.EditAsync((int)id, request, request.NewImage);


            return RedirectToAction(nameof(Index));
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "SuperAdmin")]
        public async Task<IActionResult> Delete(int id)
        {
            var team = await _teamService.GetWithIncludes(id);

            await _teamService.DeleteAsync(id);

            return RedirectToAction(nameof(Index));

        }
    }
}
