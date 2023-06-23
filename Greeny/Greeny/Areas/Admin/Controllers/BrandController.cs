using Fiorello.Helpers;
using Greeny.Areas.Admin.ViewModels.Brand;
using Greeny.Data;
using Greeny.Helpers;
using Greeny.Models;
using Greeny.Services;
using Greeny.Services.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Greeny.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class BrandController : Controller
    {
        private readonly IBrandService _brandService;
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;
        private readonly ISettingService _settingService;

        public BrandController(IBrandService brandService,
                                AppDbContext context,
                                IWebHostEnvironment env,
                                ISettingService settingService)
        {
            _brandService = brandService;
            _context = context;
            _env = env;
            _settingService = settingService;
        }
        public async Task<IActionResult> Index(int page=1)
        {
            var settingDatas = _settingService.GetAll();
            int take = int.Parse(settingDatas["BrandPagination"]);

            var paginatedDatas = await _brandService.PaginatedDatasAsync(page, take);

            int pageCount = await GetCountAsync(take);

            if (page > pageCount)
            {
                return NotFound();
            }

            List<BrandVM> mappedDatas = _brandService.MappedDatas(paginatedDatas);

            Paginate<BrandVM> result = new(mappedDatas, page, pageCount);

            return View(result);
        }



        private async Task<int> GetCountAsync(int take)
        {
            int count = await _brandService.CountAsync();
            decimal result = Math.Ceiling((decimal)count / take);

            return (int)result;

        }

        
        [HttpGet]
        public async Task<IActionResult> Detail(int? id)
        {
            if (id is null) return BadRequest();

            Brand dbBrand = await _brandService.GetByIdAsync((int)id);
            if (dbBrand is null) return NotFound();

            return View(_brandService.GetMappedData(dbBrand));
        }


        [HttpGet]       
        public IActionResult Create()
        {

            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]       
        public async Task<IActionResult> Create(BrandCreateVM request)
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

            await _brandService.CreateAsync(request.Images, request);

            return RedirectToAction(nameof(Index));
        }


        [HttpPost]
        [ValidateAntiForgeryToken]      
        public async Task<IActionResult> Delete(int id)
        {
            await _brandService.DeleteAsync(id);

            return RedirectToAction(nameof(Index));
        }



        [HttpGet]
       
        public async Task<IActionResult> Edit(int? id)
        {
            if (id is null) return BadRequest();

            Brand dbBrand = await _brandService.GetByIdAsync((int)id);

            if (dbBrand is null) return NotFound();

            return View(new BrandEditVM
            {
                Image = dbBrand.Image,
                Name = dbBrand.Name,

            });
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
       
        public async Task<IActionResult> Edit(int? id, BrandEditVM request)
        {
            if (id is null) return BadRequest();

            Brand existBrand = await _context.Brands.AsNoTracking().FirstOrDefaultAsync(m => m.Id == id);

            if (existBrand is null) return NotFound();

            if (existBrand.Name.Trim() == request.Name)
            {
                return RedirectToAction(nameof(Index));
            }
          
            if (!request.NewImage.CheckFileType("image/"))
            {
                ModelState.AddModelError("NewImage", "Please select only image file");
                request.Image = existBrand.Name;
                return View(request);
            }

            if (request.NewImage.CheckFileSize(200))
            {
                ModelState.AddModelError("NewImage", "Image size must be max 200KB");
                request.Image = existBrand.Name;
                return View(request);
            }

            await _brandService.EditAsync(request, request.NewImage);


            return RedirectToAction(nameof(Index));
        }

    }
}
