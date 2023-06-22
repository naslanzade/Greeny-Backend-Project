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

        public BlogService(AppDbContext context)
        {
            _context = context;

        }

        
        public async Task<List<Blog>> GetAllAsync()
        {
            return await _context.Blogs.Include(m=>m.Author).Take(3).ToListAsync();
        }

        public async  Task<List<Blog>> GetBlogsByDate()
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

        public  List<BlogVM> GetMappedDatas(List<Blog> blogs)
        {
            List<BlogVM> list = new();
            foreach (var blog in blogs)
            {
                list.Add(new BlogVM
                {
                    Description = blog.Description,
                    Image=blog.Image,
                    AuthorName=blog.AuthorId,
                    Blogs=_context.Blogs.Include(m=>m.Author).ToList(),
                    CreatedDate=blog.CreatedDate.ToString("MMMM dd, yyyy"),                  

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
    }
}
