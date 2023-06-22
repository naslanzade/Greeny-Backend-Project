using Fiorello.Helpers;
using Greeny.Services.Interface;
using Greeny.ViewModels;
using Microsoft.AspNetCore.Mvc;



namespace Greeny.Controllers
{
    public class ShopController : Controller
    {

        private readonly IProductService _productService;        
        private readonly ISettingService _settingService;

        public ShopController(IProductService productService,
                             ISettingService settingService)
        {
            _productService = productService;         
            _settingService = settingService;
        }

        public async Task<IActionResult> Index(int page=1)
        {
            var settingDatas = _settingService.GetAll();
            int take = int.Parse(settingDatas["ShopPagination"]);

            var paginatedDatas = await _productService.GetPaginatedDatasAsync(page, take);

            int pageCount = await GetCountAsync(take);

            if (page > pageCount)
            {
                return NotFound();
            }

            List<ProductVM> mappedDatas = _productService.GetMappedDatas(paginatedDatas);

            Paginate<ProductVM> result = new(mappedDatas, page, pageCount);

            return View(result);
        }


        private async Task<int> GetCountAsync(int take)
        {
            int count = await _productService.GetCountAsync();
            decimal result = Math.Ceiling((decimal)count / take);

            return (int)result;

        }


        public async Task<IActionResult> Search(string searchText)
        {
            if (string.IsNullOrEmpty(searchText))
            {
                return Ok();
            }
            var products = await _productService.GetAllBySearchText(searchText);

            return View(products);
        }




    }
}
