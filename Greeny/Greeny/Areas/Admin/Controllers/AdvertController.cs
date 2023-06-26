using Fiorello.Helpers;
using Greeny.Areas.Admin.ViewModels.AboutImage;
using Greeny.Areas.Admin.ViewModels.Advert;
using Greeny.Data;
using Greeny.Helpers;
using Greeny.Models;
using Greeny.Services;
using Greeny.Services.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Greeny.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AdvertController : Controller
    {

        private readonly IAdvertService _advertService;
        private readonly IWebHostEnvironment _env;
        private readonly AppDbContext _context;


        public AdvertController(IAdvertService advertService,
                                    IWebHostEnvironment env,
                                    AppDbContext context)
        {
            _advertService = advertService;
            _env = env;
            _context = context;
        }


        [Authorize(Roles = "SuperAdmin,Admin")]
        public async Task<IActionResult> Index()
        {
            return View(await _advertService.GetAllMappedDatas());
        }

        [HttpGet]
        [Authorize(Roles = "SuperAdmin,Admin")]
        public async Task<IActionResult> Detail(int? id)
        {
            if (id is null) return BadRequest();

            Advert dbAdvertImage = await _advertService.GetByIdAsync((int)id);
            if (dbAdvertImage is null) return NotFound();

            return View(_advertService.GetMappedData(dbAdvertImage));

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
        public async Task<IActionResult> Create(AdvertCreateVM request)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }


            foreach (var item in request.Image)
            {

                if (!item.CheckFileType("image/"))
                {
                    ModelState.AddModelError("Image", "Please select only image file");
                    return View();
                }

                if (item.CheckFileSize(200))
                {
                    ModelState.AddModelError("Image", "Image size must be max 200KB");
                    return View();
                }
            }

            await _advertService.CreateAsync(request.Image);

            return RedirectToAction(nameof(Index));
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "SuperAdmin")]
        public async Task<IActionResult> Delete(int id)
        {

            await _advertService.DeleteAsync(id);

            return RedirectToAction(nameof(Index));
        }


        [HttpGet]
        [Authorize(Roles = "SuperAdmin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id is null) return BadRequest();

            Advert dbAdvert = await _advertService.GetByIdAsync((int)id);

            if (dbAdvert is null) return NotFound();

            return View(new AdvertEditVM
            {
                Image = dbAdvert.Name,
            });

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "SuperAdmin")]
        public async Task<IActionResult> Edit(int? id, AdvertEditVM request)
        {
            if (id is null) return BadRequest();

            Advert dbAdvert = await _advertService.GetByIdAsync((int)id);

            if (dbAdvert is null) return NotFound();


            if (!request.NewImage.CheckFileType("image/"))
            {
                ModelState.AddModelError("NewImage", "Please select only image file");
                request.Image = dbAdvert.Name;
                return View(request);
            }

            if (request.NewImage.CheckFileSize(200))
            {
                ModelState.AddModelError("NewImage", "Image size must be max 200KB");
                request.Image = dbAdvert.Name;
                return View(request);
            }

            if (request.NewImage is null) return RedirectToAction(nameof(Index));


            await _advertService.EditAsync(dbAdvert, request.NewImage);


            return RedirectToAction(nameof(Index));
        }
    }
}
