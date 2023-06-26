using Fiorello.Helpers;
using Greeny.Areas.Admin.ViewModels.Product;
using Greeny.Data;
using Greeny.Helpers;
using Greeny.Models;
using Greeny.Services.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Greeny.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        private readonly ISettingService _settingService;
        private readonly ICategoryService _categoryService;
        private readonly AppDbContext _context;
        private readonly ISubCategoryService _subCategoryService;
        private readonly IDiscountService _discountService;
        private readonly IBrandService _brandService;
        private readonly ITagService _tagService;

        public ProductController(IProductService productService, 
                                ISettingService settingService, 
                                ICategoryService categoryService, 
                                AppDbContext context,
                                ISubCategoryService subCategoryService,
                                IDiscountService discountService,
                                IBrandService brandService,
                                ITagService tagService)
        {
            _productService = productService;
            _settingService = settingService;
            _categoryService = categoryService;
            _context = context;
            _subCategoryService = subCategoryService;
            _discountService = discountService;
            _brandService = brandService;
            _tagService= tagService;
        }

        [Authorize(Roles = "SuperAdmin,Admin")]
        public async Task<IActionResult> Index(int page=1)
        {
            var settingDatas = _settingService.GetAll();
            int take = int.Parse(settingDatas["AdminProductPagination"]);

            var paginatedDatas = await _productService.PaginatedDatasAsync(page, take);

            int pageCount = await GetCountAsync(take);

            if (page > pageCount)
            {
                return NotFound();
            }

            List<ProductVM> mappedDatas = _productService.MappedDatas(paginatedDatas);

            Paginate<ProductVM> result = new(mappedDatas, page, pageCount);

            return View(result);
        }

        private async Task<int> GetCountAsync(int take)
        {
            int count = await _productService.CountAsync();
            decimal result = Math.Ceiling((decimal)count / take);

            return (int)result;

        }


        [HttpGet]
        [Authorize(Roles = "SuperAdmin,Admin")]
        public async Task<IActionResult> Detail(int? id)
        {
            if (id is null) return BadRequest();

            Product product = await _productService.GetWithIncludesAsync(id);

            if (product == null) return NotFound();

            return View(_productService.GetMappedData(product));
         

        }


        private async Task Get()
        {
            ViewBag.categories = await GetCategories();
            ViewBag.subcategories= await GetSubCategories();
            ViewBag.discounts=await GetDiscounts();
            ViewBag.brands=await GetBrands();
            ViewBag.tags=await GetTags();


        }

        private async Task<SelectList> GetCategories()
        {
            List<Category> categories = await _categoryService.GetAllAsync();
            return new SelectList(categories, "Id", "Name");
        }

        private async Task<SelectList> GetSubCategories()
        {
            List<SubCategory> categories = await _subCategoryService.GetAllAsync();
            return new SelectList(categories, "Id", "Name");
        }

        private async Task<SelectList> GetDiscounts()
        {
            List<Disocunt> disocunts = await _discountService.GetAllAsync();
            return new SelectList(disocunts, "Id", "Name");
        }

        private async Task<SelectList> GetBrands()
        {
            List<Brand> brands = await _brandService.GetAllAsync();
            return new SelectList(brands, "Id", "Name");
        }

        private async Task<SelectList> GetTags()
        {
            IEnumerable<Tag> tags = await _tagService.GetAllAsync();
            return new SelectList(tags, "Id", "Name");
        }




        [HttpGet]
        [Authorize(Roles = "SuperAdmin")]
        public async Task<IActionResult> Create()
        {
            await Get();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "SuperAdmin")]
        public async Task<IActionResult> Create(ProductCreateVM request)
        {
            await Get();

            if (!ModelState.IsValid)
            {
                return View();
            }

            foreach (var item in request.Images)
            {
                if (!item.CheckFileType("image/"))
                {
                    ModelState.AddModelError("Image", "Please select only image file");
                    return View();
                }


                if (item.CheckFileSize(200))
                {
                    ModelState.AddModelError("Image", "Image size must be max 200 KB");
                    return View();
                }
            }

            await _productService.CreateAsync(request);
            return RedirectToAction(nameof(Index));
        }


        [HttpPost]
        [Authorize(Roles = "SuperAdmin")]
        public async Task<IActionResult> Delete(int id)
        {

            await _productService.DeleteAsync(id);

            return RedirectToAction(nameof(Index));
        }

    }
}
