using Greeny.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Greeny.Controllers
{
    public class ProfileController : Controller
    {       
        private readonly SignInManager<AppUser> _signInManager;
       
        public ProfileController( SignInManager<AppUser> signInManager)
        {

            
            _signInManager = signInManager;
            
        }
       
        public IActionResult Index()
        {
            return View();
        }


        [HttpGet]
        public async Task<IActionResult> LogOut()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}
