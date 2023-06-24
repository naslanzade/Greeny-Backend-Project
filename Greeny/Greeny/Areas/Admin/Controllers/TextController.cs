using Greeny.Areas.Admin.ViewModels.Author;
using Greeny.Areas.Admin.ViewModels.Text;
using Greeny.Data;
using Greeny.Models;
using Greeny.Services;
using Greeny.Services.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Greeny.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class TextController : Controller
    {

        private readonly ITextService _textService;
        private readonly AppDbContext _context;


        public TextController(ITextService textService,
                                AppDbContext context)
        {
            _textService = textService;
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            return View( await _textService.GetAllMappedDatas());
        }

        [HttpGet]
        public async Task<IActionResult> Detail(int? id)
        {
            if (id is null) return BadRequest();

            Text dbText = await _textService.GetByIdAsync((int)id);
            if (dbText is null) return NotFound();

            return View(_textService.GetMappedData(dbText));

        }


        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TextCreateVM request)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            await _textService.CreateAsync(request);

            return RedirectToAction(nameof(Index));
        }



        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id is null) return BadRequest();

            Text dbText = await _textService.GetByIdAsync((int)id);

            if (dbText is null) return NotFound();

            return View(new TextEditVM
            {
                Title = dbText.Title,
                Description = dbText.Description,

            });
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, TextEditVM request)
        {
            if (id is null) return BadRequest();

            Text existText = await _context.Texts.AsNoTracking().FirstOrDefaultAsync(m => m.Id == id);

            if (existText is null) return NotFound();

            if (existText.Title.Trim() == request.Title)
            {
                return RedirectToAction(nameof(Index));
            }
            if (existText.Description.Trim() == request.Description)
            {
                return RedirectToAction(nameof(Index));
            }

            await _textService.EditAsync(request);

            return RedirectToAction(nameof(Index));

        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            await _textService.DeleteAsync(id);

            return RedirectToAction(nameof(Index));
        }
    }
}
