using Greeny.Data;
using Greeny.Models;
using Greeny.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Greeny.Controllers
{
    public class ContactController : Controller
    {
        private readonly AppDbContext _context;

        public ContactController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            List<Branch> branches=await _context.Branches.Where(m=>!m.SoftDeleted).Include(m=>m.Country).ToListAsync();
            BgImage bgImage = await _context.BgImages.Where(m => !m.SoftDeleted).FirstOrDefaultAsync();

            ContactVM contactVM = new()
            {
                BgImage=bgImage,
                Branch=branches

            };

            return View(contactVM);
        }
    }
}
