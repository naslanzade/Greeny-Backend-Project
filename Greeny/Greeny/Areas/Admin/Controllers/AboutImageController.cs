using Fiorello.Helpers;
using Greeny.Areas.Admin.ViewModels.AboutImage;
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
    public class AboutImageController : Controller
    {

        private readonly IAboutImageService _aboutImageService;
        private readonly IWebHostEnvironment _env;
        private readonly AppDbContext _context;


        public AboutImageController(IAboutImageService aboutImageService,
                                    IWebHostEnvironment env,
                                    AppDbContext context)
        {
            _aboutImageService = aboutImageService;
            _env = env;
            _context = context;
        }


        [Authorize(Roles = "SuperAdmin,Admin")]
        public async Task<IActionResult> Index()
        {
            return View(await _aboutImageService.GetAllMappedDatas());
        }

        [HttpGet]

        [Authorize(Roles = "SuperAdmin,Admin")]
        public async Task<IActionResult> Detail(int ? id)
        {
            if (id is null) return BadRequest();

            AboutImage dbAboutImage = await _aboutImageService.GetByIdAsync((int)id);
            if (dbAboutImage is null) return NotFound();

            return View(_aboutImageService.GetMappedData(dbAboutImage));

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
        public async Task<IActionResult> Create(AboutImageCreateVM request)
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

            await _aboutImageService.CreateAsync(request.Image);

            return RedirectToAction(nameof(Index));
        }



        [HttpPost]
        [ValidateAntiForgeryToken]

        [Authorize(Roles = "SuperAdmin")]
        public async Task<IActionResult> Delete(int id)
        {

            await _aboutImageService.DeleteAsync(id);

            return RedirectToAction(nameof(Index));
        }


        [HttpGet]

        [Authorize(Roles = "SuperAdmin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id is null) return BadRequest();

            AboutImage dbAboutImage = await _aboutImageService.GetByIdAsync((int)id);

            if (dbAboutImage is null) return NotFound();

            return View(new AboutEditVM
            {
                Image = dbAboutImage.Images,
            });

        }


        [HttpPost]
        [ValidateAntiForgeryToken]

        [Authorize(Roles = "SuperAdmin")]
        public async Task<IActionResult> Edit(int? id, AboutEditVM request)
        {
            if (id is null) return BadRequest();

            AboutImage dbAboutImg = await _aboutImageService.GetByIdAsync((int)id);

            if (dbAboutImg is null) return NotFound();


            if (!request.NewImage.CheckFileType("image/"))
            {
                ModelState.AddModelError("NewImage", "Please select only image file");
                request.Image = dbAboutImg.Images;
                return View(request);
            }

            if (request.NewImage.CheckFileSize(200))
            {
                ModelState.AddModelError("NewImage", "Image size must be max 200KB");
                request.Image = dbAboutImg.Images;
                return View(request);
            }

            if (request.NewImage is null) return RedirectToAction(nameof(Index));


            await _aboutImageService.EditAsync(dbAboutImg, request.NewImage);


            return RedirectToAction(nameof(Index));
        }




    }
}
