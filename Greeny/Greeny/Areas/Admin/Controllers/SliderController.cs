using Fiorello.Helpers;
using Greeny.Areas.Admin.ViewModels.Brand;
using Greeny.Areas.Admin.ViewModels.Slider;
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
    public class SliderController : Controller
    {
        private readonly ISliderService _sliderService;
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;
       

        public SliderController(ISliderService sliderService,
                                AppDbContext context,
                                IWebHostEnvironment env)
                                
        {
            _sliderService = sliderService;
            _context = context;
            _env = env;
            
        }

        [Authorize(Roles = "SuperAdmin,Admin")]
        public async Task<IActionResult> Index()
        {
            return View(await _sliderService.MappedDatas());
        }


        [HttpGet]
        [Authorize(Roles = "SuperAdmin,Admin")]
        public async Task<IActionResult> Detail(int? id)
        {
            if (id is null) return BadRequest();

            Slider dbSlider = await _sliderService.GetByIdAsync((int)id);
            if (dbSlider is null) return NotFound();

            return View(_sliderService.GetMappedData(dbSlider));
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
        public async Task<IActionResult> Create(SliderCreateVM request)
        {
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

            await _sliderService.CreateAsync(request.Images, request);

            return RedirectToAction(nameof(Index));
        }


        [HttpGet]
        [Authorize(Roles = "SuperAdmin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id is null) return BadRequest();

            Slider dbSlider = await _sliderService.GetByIdAsync((int)id);

            if (dbSlider is null) return NotFound();

            return View(new SliderEditVM
            {
                SliderImage = dbSlider.SliderImage,
                Title = dbSlider.Title,
                Description = dbSlider.Description,

            });
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "SuperAdmin")]
        public async Task<IActionResult> Edit(int? id, SliderEditVM request)
        {
            if (id is null) return BadRequest();

            Slider existSlider = await _context.Sliders.AsNoTracking().FirstOrDefaultAsync(m => m.Id == id);

            if (existSlider is null) return NotFound();

            if (existSlider.Title.Trim() == request.Title)
            {
                return RedirectToAction(nameof(Index));
            }
            if (existSlider.Description.Trim() == request.Description)
            {
                return RedirectToAction(nameof(Index));
            }

            if (!request.NewImage.CheckFileType("image/"))
            {
                ModelState.AddModelError("NewImage", "Please select only image file");
                request.SliderImage = existSlider.SliderImage;
                return View(request);
            }

            if (request.NewImage.CheckFileSize(200))
            {
                ModelState.AddModelError("NewImage", "Image size must be max 200KB");
                request.SliderImage = existSlider.SliderImage;
                return View(request);
            }

            await _sliderService.EditAsync(request, request.NewImage);


            return RedirectToAction(nameof(Index));
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "SuperAdmin")]
        public async Task<IActionResult> Delete(int id)
        {
            await _sliderService.DeleteAsync(id);

            return RedirectToAction(nameof(Index));
        }

    }
}
