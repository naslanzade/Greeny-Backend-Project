using Greeny.Areas.Admin.ViewModels.Author;
using Greeny.Areas.Admin.ViewModels.Tag;
using Greeny.Data;
using Greeny.Models;
using Greeny.Services.Interface;
using Microsoft.EntityFrameworkCore;

namespace Greeny.Services
{
    public class TagService : ITagService
    {
        private readonly AppDbContext _context;

        public TagService(AppDbContext context)
        {
            _context = context;
        }
               

        public async Task CreateAsync(TagCreateVM tag)
        {
            Tag newTag = new()
            {
                Name = tag.Name,

            };
            await _context.Tags.AddAsync(newTag);

            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            Tag tag = await GetByIdAsync(id);

            _context.Remove(tag);
            await _context.SaveChangesAsync();
        }

        public async Task EditAsync(TagEditVM tag)
        {
            Tag newTag = new()
            {
                Id = tag.Id,
                Name = tag.Name,
            };

            _context.Update(newTag);

            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Tag>> GetAllAsync()
        {
            return await _context.Tags.Include(m=>m.ProductTag).ToListAsync();
        }

        public async Task<List<Tag>> GetAllDatasAsync()
        {
            return await _context.Tags.ToListAsync();
        }

        public async Task<List<TagVM>> GetAllMappedDatas()
        {
            List<TagVM> list = new();

            List<Tag> infos = await GetAllDatasAsync();

            foreach (var info in infos)
            {
                TagVM model = new()
                {
                    Id = info.Id,
                    Name = info.Name,

                };

                list.Add(model);
            }

            return list;
        }

        public async Task<Tag> GetByIdAsync(int id)
        {
            return await _context.Tags.FirstOrDefaultAsync(m => m.Id == id);
        }

        public TagDetailVM GetMappedData(Tag tag)
        {
            TagDetailVM model = new()
            {
                Name = tag.Name,
                CreatedDate = tag.CreatedDate.ToString("MM.dd.yyyy"),

            };

            return model;
        }


        //For Pagination in Admin Panel
        public async Task<List<Tag>> PaginatedDatasAsync(int page, int take)
        {
            return await _context.Tags.Skip((page - 1) * take)
                                         .Take(take)
                                         .ToListAsync(); 
        }

        public async Task<int> CountAsync()
        {
            return await _context.Tags.CountAsync();
        }

        public List<TagVM> MappedDatas(List<Tag> tags)
        {
            List<TagVM> list = new();
            foreach (var tag in tags)
            {
                list.Add(new TagVM
                {
                    Id = tag.Id,
                    Name = tag.Name,
                   

                });

            }

            return list;
        }
    }
}
