using Fiorello.Helpers;
using Greeny.Areas.Admin.ViewModels.Team;
using Greeny.Areas.Admin.ViewModels.Testimonial;
using Greeny.Data;
using Greeny.Models;
using Greeny.Services.Interface;
using Microsoft.EntityFrameworkCore;

namespace Greeny.Services
{
    public class TestimonialService : ITestimonialService
    {

        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;

        public TestimonialService(AppDbContext context, 
                                  IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        public async Task CreateAsync(TestimonialCreateVM model, List<IFormFile> images)
        {
            foreach (var item in images)
            {
                string fileName = Guid.NewGuid().ToString() + "_" + item.FileName;

                await item.SaveFileAsync(fileName, _env.WebRootPath, "images/testimonial");


                Testimonial team = new()
                {
                    Image = fileName,
                    FullName = model.FullName,
                    PositionId = model.PositionId,
                    Description = model.Description,
                };

                await _context.Testimonials.AddAsync(team);

            }

            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            Testimonial team = await GetByIdAsnyc(id);

            _context.Testimonials.Remove(team);

            await _context.SaveChangesAsync();

            string path = Path.Combine(_env.WebRootPath, "images/testimonial", team.Image);

            if (File.Exists(path))
            {
                File.Delete(path);
            }
        }

        public async Task EditAsync(int teamId, TestimonialEditVM model, IFormFile newImage)
        {
            var team = await GetByIdAsnyc(teamId);

            string oldPath = Path.Combine(_env.WebRootPath, "images/testimonial", newImage.FileName);

            if (File.Exists(oldPath))
            {
                File.Delete(oldPath);
            }

            string fileName = Guid.NewGuid().ToString() + "_" + newImage.FileName;

            await newImage.SaveFileAsync(fileName, _env.WebRootPath, "images/testimonial");

            model.Image = fileName;

            team.FullName = model.FullName;
            team.PositionId = model.PositionId;
            team.Image = fileName;
            team.Description = model.Description;
            await _context.SaveChangesAsync();
        }

        public async Task<List<Testimonial>> GetAllAsync()
        {
            return await _context.Testimonials.Include(m=>m.Position).ToListAsync();
        }

        public async Task<List<Testimonial>> GetAllDatasAsync()
        {
            return await _context.Testimonials.Include(m => m.Position).ToListAsync();
        }

        public async Task<Testimonial> GetByIdAsnyc(int? id)
        {
            return await _context.Testimonials.FirstOrDefaultAsync(m => m.Id == id);
        }

        public TestimonialDetailVM GetMappedData(Testimonial team)
        {
            return new TestimonialDetailVM
            {
                FullName = team.FullName,
                PositionName = team.Position.Name,
                Image = team.Image,
                Description = team.Description,
                CreatedDate = team.CreatedDate.ToString("MMMM dd, yyyy"),
            };
        }

        public async Task<List<TestimonialVM>> GetMappedDatas()
        {
            List<TestimonialVM> list = new();

            List<Testimonial> infos = await GetAllDatasAsync();

            foreach (var info in infos)
            {
                TestimonialVM model = new()
                {
                    Id = info.Id,
                    FullName = info.FullName,
                    Image = info.Image,
                    PositionName = info.Position.Name
                };

                list.Add(model);
            }

            return list;
        }

        public async Task<Testimonial> GetWithIncludes(int? id)
        {
            return await _context.Testimonials.Where(m => m.Id == id)
                                             .Include(m => m.Position)
                                              .FirstOrDefaultAsync();
        }
    }
}
