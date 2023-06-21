using Fiorello.Helpers;
using Greeny.Models;
using Greeny.Services.Interface;
using Greeny.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Greeny.Controllers
{
    public class BrandController : Controller
    {

        private readonly ISettingService _settingService;
        private readonly IBrandService _brandService;

        public BrandController(ISettingService settingService,
                               IBrandService brandService)
        {
            
            _settingService = settingService;
            _brandService = brandService;
        }


        
        public async Task<IActionResult> Index(int page = 1)
        {
            var settingDatas = _settingService.GetAll();
            int take = int.Parse(settingDatas["BrandPagination"]);

            var paginatedDatas = await _brandService.GetPaginatedDatasAsync(page, take);

            int pageCount = await GetCountAsync(take);

            if (page > pageCount)
            {
                return NotFound();
            }

            List<BrandVM> mappedDatas = _brandService.GetMappedDatas(paginatedDatas);

            Paginate<BrandVM> result = new(mappedDatas, page, pageCount);

            return View(result);
            
        }


        private async Task<int> GetCountAsync(int take)
        {
            int count = await _brandService.GetCountAsync();
            decimal result = Math.Ceiling((decimal)count / take);

            return (int)result;

        }
    }
}
