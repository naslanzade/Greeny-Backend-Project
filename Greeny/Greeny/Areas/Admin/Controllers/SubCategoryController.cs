using Fiorello.Helpers;
using Greeny.Areas.Admin.ViewModels.SubCategory;
using Greeny.Data;
using Greeny.Helpers;
using Greeny.Models;
using Greeny.Services;
using Greeny.Services.Interface;
using Greeny.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Greeny.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SubCategoryController : Controller
    {

        private readonly ISubCategoryService _subCategoryService;
        private readonly ISettingService _settingService;
        private readonly AppDbContext _context;
        private readonly ICategoryService _categoryService;

        public SubCategoryController(ISubCategoryService subCategoryService,
                                     ISettingService settingService,
                                     AppDbContext context,
                                     ICategoryService categoryService)
        {
            
            _subCategoryService = subCategoryService;
            _settingService = settingService;
            _context = context;
            _categoryService = categoryService;
        }

        
        public async Task<IActionResult> Index(int page=1)
        {
            var settingDatas = _settingService.GetAll();
            int take = int.Parse(settingDatas["AdminSubCategoryPagination"]);

            var paginatedDatas = await _subCategoryService.GetPaginatedDatasAsync(page, take);

            int pageCount = await GetCountAsync(take);

            if (page > pageCount)
            {
                return NotFound();
            }

            List<SubCategoryVM> mappedDatas = _subCategoryService.GetMappedDatas(paginatedDatas);

            Paginate<SubCategoryVM> result = new(mappedDatas, page, pageCount);

            return View(result);
        }

        private async Task<int> GetCountAsync(int take)
        {
            int count = await _subCategoryService.GetCountAsync();
            decimal result = Math.Ceiling((decimal)count / take);

            return (int)result;

        }

        [HttpGet]        
        public async Task<IActionResult> Detail(int? id)
        {
            if (id is null) return BadRequest();

            SubCategory subCategory = await _subCategoryService.GetWithIncludes(id);

            if (subCategory == null) return NotFound();

            return View(_subCategoryService.GetMappedData(subCategory));


        }


        private async Task<SelectList> GetCategories()        {
            List<Category> categories = await _categoryService.GetAllAsync();
            return new SelectList(categories, "Id", "Name");
        }

        private async Task GetCategory()
        {
            ViewBag.categories = await GetCategories();

        }


        [HttpGet]       
        public async Task<IActionResult> Create()
        {
            await GetCategory();
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]       
        public async Task<IActionResult> Create(SubCategoryCreateVM request)
        {
            await GetCategory();

            if (!ModelState.IsValid)
            {
                return View();
            }        

            await _subCategoryService.CreateAsync(request);
            return RedirectToAction(nameof(Index));
        }



        [HttpGet]       
        public async Task<IActionResult> Edit(int? id)
        {
            await GetCategory();

            if (id is null) return BadRequest();

            SubCategory subCategory = await _subCategoryService.GetWithIncludes(id);

            if (subCategory is null) return NotFound();

            SubCategoryEditVM response = new()
            {
                Name = subCategory.Name,                
                CategoryId = subCategory.CategoryId,                
            };

            return View(response);
        }



        [HttpPost]
        [ValidateAntiForgeryToken]      
        public async Task<IActionResult> Edit(int ? id, SubCategoryEditVM request)
        {
            await GetCategory();

            if (id is null) return BadRequest();

            SubCategory subCategory = await _subCategoryService.GetWithIncludes(id);  
            
            if (subCategory is null) return NotFound();

            if (subCategory.Name.Trim() == request.Name)
            {
                return RedirectToAction(nameof(Index));
            }

            await _subCategoryService.EditAsync((int)id,request);

            return RedirectToAction(nameof(Index));


        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var subCategory = await _subCategoryService.GetWithIncludes(id);

            await _subCategoryService.DeleteAsync(id);

            return RedirectToAction(nameof(Index));
        }








    }
}
