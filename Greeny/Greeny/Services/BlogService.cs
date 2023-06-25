using Fiorello.Helpers;
using Greeny.Areas.Admin.ViewModels.Blog;
using Greeny.Areas.Admin.ViewModels.Team;
using Greeny.Data;
using Greeny.Models;
using Greeny.Services.Interface;
using Greeny.ViewModels;
using Microsoft.EntityFrameworkCore;
using static Org.BouncyCastle.Asn1.Cmp.Challenge;

namespace Greeny.Services
{
    public class BlogService : IBlogService
    {

        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;

        public BlogService(AppDbContext context,
                          IWebHostEnvironment env)
        {
            _context = context;
            _env = env;

        }

      
        public async Task<List<Blog>> GetAllAsync()
        {
            return await _context.Blogs.Include(m => m.Author).Take(3).ToListAsync();
        }


        public async Task<List<Blog>> GetBlogsByDate()
        {
            return await _context.Blogs.Include(m => m.Author).
                                          Take(3).
                                          OrderByDescending(m => m.CreatedDate).
                                          ToListAsync();
        }

        public async Task<int> GetCountAsync()
        {
            return await _context.Blogs.CountAsync();
        }

        public List<ViewModels.BlogVM> GetMappedDatas(List<Blog> blogs)
        {
            List<ViewModels.BlogVM> list = new();
            foreach (var blog in blogs)
            {
                list.Add(new ViewModels.BlogVM
                {
                    Description = blog.Description,
                    Image = blog.Image,                   
                    Blogs = _context.Blogs.Include(m => m.Author).ToList(),
                    CreatedDate = blog.CreatedDate.ToString("MMMM dd, yyyy"),

                });

            }

            return list;
        }

        public async Task<List<Blog>> GetPaginatedDatasAsync(int page, int take)
        {
            return await _context.Blogs.Skip((page - 1) * take)
                                          .Take(take)
                                          .ToListAsync();
        }



        //For Admin Panel
        public async Task CreateAsync(Areas.Admin.ViewModels.Blog.BlogCreateVM model, List<IFormFile> images)
        {
            foreach (var item in images)
            {
                string fileName = Guid.NewGuid().ToString() + "_" + item.FileName;

                await item.SaveFileAsync(fileName, _env.WebRootPath, "images/blog");


                Blog blog = new()
                {
                    Image = fileName,
                    Title = model.Title,
                    AuthorId = model.AuthorId,
                    Description= model.Description,
                };

                await _context.Blogs.AddAsync(blog);

            }

            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            Blog blog = await GetByIdAsnyc(id);

            _context.Blogs.Remove(blog);

            await _context.SaveChangesAsync();

            string path = Path.Combine(_env.WebRootPath, "images/blog", blog.Image);

            if (File.Exists(path))
            {
                File.Delete(path);
            }
        }

        public async Task EditAsync(int blogId, Areas.Admin.ViewModels.Blog.BlogEditVM model, IFormFile newImage)
        {
            var blog = await GetByIdAsnyc(blogId);

            string oldPath = Path.Combine(_env.WebRootPath, "images/blog", newImage.FileName);

            if (File.Exists(oldPath))
            {
                File.Delete(oldPath);
            }

            string fileName = Guid.NewGuid().ToString() + "_" + newImage.FileName;

            await newImage.SaveFileAsync(fileName, _env.WebRootPath, "images/blog");

            model.Image = fileName;

            blog.Title = model.Title;
            blog.AuthorId = model.AuthorId;
            blog.Description= model.Description;
            blog.Image = fileName;
            await _context.SaveChangesAsync();
        }


        public async Task<List<Blog>> GetAllDatasAsync()
        {
            return await _context.Blogs.Include(m => m.Author).ToListAsync();
        }


        public async Task<Blog> GetByIdAsnyc(int? id)
        {
            return await _context.Blogs.FirstOrDefaultAsync(m => m.Id == id);
        }


        public BlogDetailVM GetMappedData(Blog blog)
        {
            return new BlogDetailVM
            {
                Title = blog.Title,
                AuthorName = blog.Author.FullName,
                Image = blog.Image,
                Description = blog.Description,
                CreatedDate = blog.CreatedDate.ToString("MMMM dd, yyyy"),
            };
        }


        public async Task<List<Areas.Admin.ViewModels.Blog.BlogVM>> GetMappedDatas()
        {
            List<Areas.Admin.ViewModels.Blog.BlogVM> list = new();

            List<Blog> infos = await GetAllDatasAsync();

            foreach (var info in infos)
            {
                Areas.Admin.ViewModels.Blog.BlogVM model = new()
                {
                    Id = info.Id,
                    Title = info.Title,
                    Image = info.Image,
                    AuthorName = info.Author.FullName
                };

                list.Add(model);
            }

            return list;
        }

        public async Task<Blog> GetWithIncludes(int? id)
        {
            return await _context.Blogs.Where(m => m.Id == id)
                                             .Include(m => m.Author)
                                              .FirstOrDefaultAsync();
        }

      

       

    }
}
