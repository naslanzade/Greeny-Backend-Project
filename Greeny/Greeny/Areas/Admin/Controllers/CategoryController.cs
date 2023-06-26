using Fiorello.Helpers;
using Greeny.Areas.Admin.ViewModels.Brand;
using Greeny.Areas.Admin.ViewModels.Category;
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
    public class CategoryController : Controller
    {

        private readonly ICategoryService _categoryService;
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;
        private readonly ISettingService _settingService;

        public CategoryController(ICategoryService categoryService, 
                                  AppDbContext context, 
                                  IWebHostEnvironment env,
                                  ISettingService settingService)
        {

            _categoryService = categoryService;
            _context = context;
            _env = env;
            _settingService = settingService;
        }



        [Authorize(Roles = "SuperAdmin,Admin")]
        public async Task<IActionResult> Index(int page=1)
        {
            var settingDatas = _settingService.GetAll();
            int take = int.Parse(settingDatas["AdminCategoryPagination"]);

            var paginatedDatas = await _categoryService.PaginatedDatasAsync(page, take);

            int pageCount = await GetCountAsync(take);

            if (page > pageCount)
            {
                return NotFound();
            }

            List<CategoryVM> mappedDatas = _categoryService.MappedDatas(paginatedDatas);

            Paginate<CategoryVM> result = new(mappedDatas, page, pageCount);

            return View(result);
        }


        private async Task<int> GetCountAsync(int take)
        {
            int count = await _categoryService.CountAsync();
            decimal result = Math.Ceiling((decimal)count / take);

            return (int)result;

        }


        [HttpGet]
        [Authorize(Roles = "SuperAdmin,Admin")]
        public async Task<IActionResult> Detail(int? id)
        {
            if (id is null) return BadRequest();

            Category dbCategory = await _categoryService.GetByIdAsync((int)id);
            if (dbCategory is null) return NotFound();

            return View(_categoryService.GetMappedData(dbCategory));
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
        public async Task<IActionResult> Create(CategoryCreateVM request)
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

            await _categoryService.CreateAsync(request.Images, request);

            return RedirectToAction(nameof(Index));
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "SuperAdmin")]
        public async Task<IActionResult> Delete(int id)
        {
            await _categoryService.DeleteAsync(id);

            return RedirectToAction(nameof(Index));
        }



        [HttpGet]
        [Authorize(Roles = "SuperAdmin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id is null) return BadRequest();

            Category dbCategory = await _categoryService.GetByIdAsync((int)id);

            if (dbCategory is null) return NotFound();

            return View(new CategoryEditVM
            {
                Image = dbCategory.Image,
                Name = dbCategory.Name,

            });
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "SuperAdmin")]
        public async Task<IActionResult> Edit(int? id, CategoryEditVM request)
        {
            if (id is null) return BadRequest();

            Category existCategory = await _context.Categories.AsNoTracking().FirstOrDefaultAsync(m => m.Id == id);

            if (existCategory is null) return NotFound();

            if (existCategory.Name.Trim() == request.Name)
            {
                return RedirectToAction(nameof(Index));
            }

            if (!request.NewImage.CheckFileType("image/"))
            {
                ModelState.AddModelError("NewImage", "Please select only image file");
                request.Image = existCategory.Name;
                return View(request);
            }

            if (request.NewImage.CheckFileSize(200))
            {
                ModelState.AddModelError("NewImage", "Image size must be max 200KB");
                request.Image = existCategory.Name;
                return View(request);
            }

            await _categoryService.EditAsync(request, request.NewImage);


            return RedirectToAction(nameof(Index));
        }
    }
}
