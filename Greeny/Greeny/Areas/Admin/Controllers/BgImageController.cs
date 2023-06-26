using Fiorello.Helpers;
using Greeny.Areas.Admin.ViewModels.Advert;
using Greeny.Areas.Admin.ViewModels.BgImage;
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
    public class BgImageController : Controller
    {

        private readonly IBgImageService _bgImageService;
        private readonly IWebHostEnvironment _env;
        private readonly AppDbContext _context;


        public BgImageController(IBgImageService bgImageService,
                                    IWebHostEnvironment env,
                                    AppDbContext context)
        {
            _bgImageService = bgImageService;
            _env = env;
            _context = context;
        }

        [Authorize(Roles = "SuperAdmin,Admin")]
        public async Task<IActionResult> Index()
        {
            return View(await _bgImageService.GetAllMappedDatas());
        }


        [HttpGet]
        [Authorize(Roles = "SuperAdmin,Admin")]
        public async Task<IActionResult> Detail(int? id)
        {
            if (id is null) return BadRequest();

            BgImage dbBgImage = await _bgImageService.GetByIdAsync((int)id);
            if (dbBgImage is null) return NotFound();

            return View(_bgImageService.GetMappedData(dbBgImage));
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

            await _bgImageService.CreateAsync(request.Image);

            return RedirectToAction(nameof(Index));
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "SuperAdmin")]
        public async Task<IActionResult> Delete(int id)
        {
            await _bgImageService.DeleteAsync(id);

            return RedirectToAction(nameof(Index));
        }


        [HttpGet]
        [Authorize(Roles = "SuperAdmin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id is null) return BadRequest();

            BgImage dbBgImage = await _bgImageService.GetByIdAsync((int)id);

            if (dbBgImage is null) return NotFound();

            return View(new BgImageEditVM
            {
                Image = dbBgImage.Image,
            });

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "SuperAdmin")]
        public async Task<IActionResult> Edit(int? id, BgImageEditVM request)
        {
            if (id is null) return BadRequest();

            BgImage dbBgImge = await _bgImageService.GetByIdAsync((int)id);

            if (dbBgImge is null) return NotFound();


            if (!request.NewImage.CheckFileType("image/"))
            {
                ModelState.AddModelError("NewImage", "Please select only image file");
                request.Image = dbBgImge.Image;
                return View(request);
            }

            if (request.NewImage.CheckFileSize(200))
            {
                ModelState.AddModelError("NewImage", "Image size must be max 200KB");
                request.Image = dbBgImge.Image;
                return View(request);
            }

            if (request.NewImage is null) return RedirectToAction(nameof(Index));


            await _bgImageService.EditAsync(dbBgImge, request.NewImage);


            return RedirectToAction(nameof(Index));
        }







    }
}
