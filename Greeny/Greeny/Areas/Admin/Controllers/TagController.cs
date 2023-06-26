using Fiorello.Helpers;
using Greeny.Areas.Admin.ViewModels.Author;
using Greeny.Areas.Admin.ViewModels.Tag;
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
    public class TagController : Controller
    {
        private readonly ITagService _tagService;
        private readonly AppDbContext _context;
        private readonly ISettingService _settingService;

        public TagController(ITagService tagService,
                             AppDbContext context,
                             ISettingService settingService)
        {
            _context = context;
            _tagService = tagService;
            _settingService = settingService;

        }



        [Authorize(Roles = "SuperAdmin,Admin")]
        public async Task<IActionResult> Index(int page=1)
        {
            var settingDatas = _settingService.GetAll();
            int take = int.Parse(settingDatas["AdminTagPagination"]);

            var paginatedDatas = await _tagService.PaginatedDatasAsync(page, take);

            int pageCount = await GetCountAsync(take);

            if (page > pageCount)
            {
                return NotFound();
            }

            List<TagVM> mappedDatas = _tagService.MappedDatas(paginatedDatas);

            Paginate<TagVM> result = new(mappedDatas, page, pageCount);

            return View(result);
        }


        private async Task<int> GetCountAsync(int take)
        {
            int count = await _tagService.CountAsync();
            decimal result = Math.Ceiling((decimal)count / take);

            return (int)result;

        }


        [HttpGet]
        [Authorize(Roles = "SuperAdmin,Admin")]
        public async Task<IActionResult> Detail(int? id)
        {
            if (id is null) return BadRequest();

            Tag dbTag = await _tagService.GetByIdAsync((int)id);
            if (dbTag is null) return NotFound();

            return View(_tagService.GetMappedData(dbTag));

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
        public async Task<IActionResult> Create(TagCreateVM request)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            await _tagService.CreateAsync(request);

            return RedirectToAction(nameof(Index));
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "SuperAdmin")]
        public async Task<IActionResult> Delete(int id)
        {
            await _tagService.DeleteAsync(id);

            return RedirectToAction(nameof(Index));
        }


        [HttpGet]
        [Authorize(Roles = "SuperAdmin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id is null) return BadRequest();

            Tag dbTag = await _tagService.GetByIdAsync((int)id);

            if (dbTag is null) return NotFound();

            return View(new TagEditVM
            {
                Name = dbTag.Name,

            });
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "SuperAdmin")]
        public async Task<IActionResult> Edit(int? id, TagEditVM request)
        {
            if (id is null) return BadRequest();

            Tag existTag = await _context.Tags.AsNoTracking().FirstOrDefaultAsync(m => m.Id == id);

            if (existTag is null) return NotFound();

            if (existTag.Name.Trim() == request.Name)
            {
                return RedirectToAction(nameof(Index));
            }

            await _tagService.EditAsync(request);

            return RedirectToAction(nameof(Index));

        }
    }
}
