using Fiorello.Helpers;
using Greeny.Areas.Admin.ViewModels.Branch;
using Greeny.Data;
using Greeny.Models;
using Greeny.Services.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;


namespace Greeny.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class BranchController : Controller
    {


        private readonly IBranchService _branchService;
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;
        private readonly ICountryService _countryService;
       

        public BranchController(IBranchService branchService,
                                  AppDbContext context,
                                  IWebHostEnvironment env,
                                  ICountryService countryService)
        {

            _branchService = branchService;
            _context = context;
            _env = env;
            _countryService = countryService;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _branchService.GetMappedDatas());
        }


        [HttpGet]
        public async Task<IActionResult> Detail(int? id)
        {
            if (id is null) return BadRequest();

            Branch branch = await _branchService.GetWithIncludes(id);

            if (branch == null) return NotFound();

            return View(_branchService.GetMappedData(branch));


        }


        private async Task<SelectList> GetCountries()
        {
            List<Country> countries = await _countryService.GetAllAsync();
            return new SelectList(countries, "Id", "Name");
        }

        private async Task GetCountry()
        {
            ViewBag.countries = await GetCountries();

        }


        [HttpGet]
        public async Task<IActionResult> Create()
        {
            await GetCountry();
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BranchCreateVM request)
        {
            await GetCountry();

            if (!ModelState.IsValid)
            {
                return View();
            }

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

            await _branchService.CreateAsync(request,request.Images);
            return RedirectToAction(nameof(Index));
        }


        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            await GetCountry();

            if (id is null) return BadRequest();

            Branch branch = await _branchService.GetWithIncludes(id);

            if (branch is null) return NotFound();

            BranchEditVM response = new()
            {
                City = branch.City,
                Image=branch.Image,
                CountryId = branch.CountryId,
                Address= branch.Address,
            };

            return View(response);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, BranchEditVM request)
        {
            if (id is null) return BadRequest();

            Branch existBranch= await _branchService.GetWithIncludes(id);

            if (existBranch is null) return NotFound();

            if (existBranch.City.Trim() == request.City)
            {
                return RedirectToAction(nameof(Index));
            }
            if (existBranch.Address.Trim() == request.Address)
            {
                return RedirectToAction(nameof(Index));
            }

            if (!request.NewImage.CheckFileType("image/"))
            {
                ModelState.AddModelError("NewImage", "Please select only image file");
                request.Image = existBranch.Image;
                return View(request);
            }

            if (request.NewImage.CheckFileSize(200))
            {
                ModelState.AddModelError("NewImage", "Image size must be max 200KB");
                request.Image = existBranch.Image;
                return View(request);
            }

            await _branchService.EditAsync((int)id,request, request.NewImage);


            return RedirectToAction(nameof(Index));
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var barnch = await _branchService.GetWithIncludes(id);

            await _branchService.DeleteAsync(id);

            return RedirectToAction(nameof(Index));
        }






    }
}
