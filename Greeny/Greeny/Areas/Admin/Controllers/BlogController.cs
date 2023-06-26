using Fiorello.Helpers;
using Greeny.Areas.Admin.ViewModels.Author;
using Greeny.Areas.Admin.ViewModels.Blog;
using Greeny.Areas.Admin.ViewModels.Team;
using Greeny.Data;
using Greeny.Helpers;
using Greeny.Models;
using Greeny.Services;
using Greeny.Services.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Greeny.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class BlogController : Controller
    {

        private readonly IBlogService _blogService;
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;
        private readonly IAuthorService _authorService;


        public BlogController(IBlogService blogService,
                              AppDbContext context,
                              IWebHostEnvironment env,
                              IAuthorService authorService)
        {

            _blogService = blogService;
            _context = context;
            _env = env;
            _authorService = authorService;
        }

        [Authorize(Roles = "SuperAdmin,Admin")]
        public async Task<IActionResult> Index()
        {
            return View(await _blogService.GetMappedDatas());
        }


        [HttpGet]
        [Authorize(Roles = "SuperAdmin,Admin")]
        public async Task<IActionResult> Detail(int? id)
        {
            if (id is null) return BadRequest();

            Blog blog = await _blogService.GetWithIncludes(id);
            if (blog == null) return NotFound();

            return View(_blogService.GetMappedData(blog));
        }



        private async Task<SelectList> GetAuthors()
        {
            List<Author> authors = await _authorService.GetAllAsync();
            return new SelectList(authors, "Id", "FullName");
        }

        private async Task GetAuthor()
        {
            ViewBag.authors = await GetAuthors();

        }



        [HttpGet]
        [Authorize(Roles = "SuperAdmin")]
        public async Task<IActionResult> Create()
        {
            await GetAuthor();
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "SuperAdmin")]
        public async Task<IActionResult> Create(BlogCreateVM request)
        {
            await GetAuthor();

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

            await _blogService.CreateAsync(request, request.Images);
            return RedirectToAction(nameof(Index));
        }


        [HttpGet]
        [Authorize(Roles = "SuperAdmin")]
        public async Task<IActionResult> Edit(int? id)
        {
            await GetAuthor();

            if (id is null) return BadRequest();

            Blog blog = await _blogService.GetWithIncludes(id);

            if (blog is null) return NotFound();

            BlogEditVM response = new()
            {
                Title = blog.Title,
                Image = blog.Image,
                AuthorId = blog.AuthorId,
                Description= blog.Description,
            };

            return View(response);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "SuperAdmin")]
        public async Task<IActionResult> Edit(int? id, BlogEditVM request)
        {
            if (id is null) return BadRequest();

            Blog existBlog = await _blogService.GetWithIncludes(id);

            if (existBlog is null) return NotFound();

            if (existBlog.Title.Trim() == request.Title)
            {
                return RedirectToAction(nameof(Index));
            }
            if (existBlog.Description.Trim() == request.Description)
            {
                return RedirectToAction(nameof(Index));
            }

            if (!request.NewImage.CheckFileType("image/"))
            {
                ModelState.AddModelError("NewImage", "Please select only image file");
                request.Image = existBlog.Image;
                return View(request);
            }

            if (request.NewImage.CheckFileSize(200))
            {
                ModelState.AddModelError("NewImage", "Image size must be max 200KB");
                request.Image = existBlog.Image;
                return View(request);
            }

            await _blogService.EditAsync((int)id, request, request.NewImage);


            return RedirectToAction(nameof(Index));
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "SuperAdmin")]
        public async Task<IActionResult> Delete(int id)
        {
            var team = await _blogService.GetWithIncludes(id);

            await _blogService.DeleteAsync(id);

            return RedirectToAction(nameof(Index));

        }


    }
}
