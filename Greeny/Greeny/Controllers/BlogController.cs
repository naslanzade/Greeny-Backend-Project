using Fiorello.Helpers;
using Greeny.Services;
using Greeny.Services.Interface;
using Greeny.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Greeny.Controllers
{

    //BlogPagination
    public class BlogController : Controller
    {
        private readonly IBlogService _blogService;
        private readonly IBgImageService _bgImageService;
        private readonly ISettingService _settingService;


        public BlogController(IBlogService blogService,
                              ISettingService settingService,
                              IBgImageService bgImageService)
        {
            
            _blogService = blogService;
            _bgImageService = bgImageService;
            _settingService = settingService;

        }



        public async Task<IActionResult> Index(int page=1)
        {
            var settingDatas = _settingService.GetAll();
            int take = int.Parse(settingDatas["BlogPagination"]);

            var paginatedDatas = await _blogService.GetPaginatedDatasAsync(page, take);

            int pageCount = await GetCountAsync(take);

            if (page > pageCount)
            {
                return NotFound();
            }

            List<BlogVM> mappedDatas = _blogService.GetMappedDatas(paginatedDatas);

            Paginate<BlogVM> result = new(mappedDatas, page, pageCount);

            return View(result);
        }



        private async Task<int> GetCountAsync(int take)
        {
            int count = await _blogService.GetCountAsync();
            decimal result = Math.Ceiling((decimal)count / take);

            return (int)result;

        }
    }
}
