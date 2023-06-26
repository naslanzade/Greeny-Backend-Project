using Greeny.Areas.Admin.ViewModels.Advert;
using Greeny.Areas.Admin.ViewModels.Author;
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
    public class AuthorController : Controller
    {

        private readonly IAuthorService _authorService;
        private readonly AppDbContext _context;


        public AuthorController(IAuthorService authorService,
                                AppDbContext context)
        {
            _authorService = authorService;
            _context = context;
        }

        [Authorize(Roles = "SuperAdmin,Admin")]
        public async Task<IActionResult> Index()
        {
            return View(await _authorService.GetAllMappedDatas());
        }


        [HttpGet]
        [Authorize(Roles = "SuperAdmin,Admin")]
        public async Task<IActionResult> Detail(int? id)
        {
            if (id is null) return BadRequest();

            Author dbAuthor = await _authorService.GetByIdAsync((int)id);
            if (dbAuthor is null) return NotFound();

            return View(_authorService.GetMappedData(dbAuthor));

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
        public async Task<IActionResult> Create(AuthorCreateVM request)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            await _authorService.CreateAsync(request);

            return RedirectToAction(nameof(Index));
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "SuperAdmin")]
        public async Task<IActionResult> Delete(int id)
        {
            await _authorService.DeleteAsync(id);

            return RedirectToAction(nameof(Index));
        }



        [HttpGet]
        [Authorize(Roles = "SuperAdmin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id is null) return BadRequest();

            Author dbAuthor = await _authorService.GetByIdAsync((int)id);

            if (dbAuthor is null) return NotFound();

            return View(new AuthorEditVM
            {
               FullName = dbAuthor.FullName,

            });
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "SuperAdmin")]
        public async Task<IActionResult> Edit(int? id, AuthorEditVM request)
        {
            if (id is null) return BadRequest();

            Author existAuthor =  await _context.Authors.AsNoTracking().FirstOrDefaultAsync(m => m.Id == id);

            if (existAuthor is null) return NotFound();

            if (existAuthor.FullName.Trim() == request.FullName)
            {
                return RedirectToAction(nameof(Index));
            }

            await _authorService.EditAsync(request);

            return RedirectToAction(nameof(Index));

        }
    }
}
