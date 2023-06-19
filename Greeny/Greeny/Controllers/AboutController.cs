using Greeny.Data;
using Greeny.Models;
using Greeny.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Greeny.Controllers
{
    public class AboutController : Controller
    {


        private readonly AppDbContext _context;


        public AboutController(AppDbContext context)
        {
            
            _context = context;
        }


        public async Task< IActionResult> Index()
        {
            List<AboutImage> images= await _context.AboutImages.Where(m=>!m.SoftDeleted).ToListAsync();
            Text text=await _context.Texts.Where(m=>!m.SoftDeleted).FirstOrDefaultAsync();
            List<Milestone> milestones= await _context.Milestones.Where(m => !m.SoftDeleted).ToListAsync();
            List<Team> teams=await _context.Teams.Where(m=>!m.SoftDeleted).Include(m=>m.Position).ToListAsync();


            AboutVM aboutVM = new()
            {
                Images=images,
                Milestones=milestones,
                Texts=text,
                Teams=teams 
            };

            return View(aboutVM);
        }
    }
}
