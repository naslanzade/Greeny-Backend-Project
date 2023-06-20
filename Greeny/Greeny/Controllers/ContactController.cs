using Greeny.Data;
using Greeny.Models;
using Greeny.Services.Interface;
using Greeny.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Greeny.Controllers
{
    public class ContactController : Controller
    {
        private readonly IBgImageService _imageService;
        private readonly IBranchService _branchService;

        public ContactController(IBgImageService bgImageService,IBranchService branchService)
        {
            
            _imageService = bgImageService;
            _branchService = branchService;
        }



        public async Task<IActionResult> Index()
        {
            

            ContactVM contactVM = new()
            {
                BgImage= await _imageService.GetAllAsync(),
                Branch= await _branchService.GetAllAsync()

            };

            return View(contactVM);
        }
    }
}
