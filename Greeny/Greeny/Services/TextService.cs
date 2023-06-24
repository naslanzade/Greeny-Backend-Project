using Greeny.Areas.Admin.ViewModels.Author;
using Greeny.Areas.Admin.ViewModels.Text;
using Greeny.Data;
using Greeny.Models;
using Greeny.Services.Interface;
using Microsoft.EntityFrameworkCore;

namespace Greeny.Services
{
    public class TextService : ITextService
    {

        private readonly AppDbContext _context;

        public TextService(AppDbContext context)
        {
            
            _context = context;
        }

        public async Task CreateAsync(TextCreateVM text)
        {
            Text newText = new()
            {
                Title = text.Title,
                Description = text.Description,

            };
            await _context.Texts.AddAsync(newText);

            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            Text text = await GetByIdAsync(id);

            _context.Remove(text);
            await _context.SaveChangesAsync();
        }

        public async Task EditAsync(TextEditVM text)
        {
            Text newText = new()
            {
                Id = text.Id,
                Title = text.Title,
                Description = text.Description,
            };

            _context.Update(newText);

            await _context.SaveChangesAsync();
        }

        public async Task<Text> GetAllAsync()
        {
            return await _context.Texts.FirstOrDefaultAsync();
        }

        public async Task<List<Text>> GetAllDatasAsync()
        {
            return await _context.Texts.ToListAsync();
        }

        public async Task<List<TextVM>> GetAllMappedDatas()
        {
            List<TextVM> lits = new();

            List<Text> infos = await GetAllDatasAsync();

            foreach (var info in infos)
            {
                TextVM model = new()
                {
                    Id = info.Id,
                    Title = info.Title,

                };

                lits.Add(model);
            }

            return lits;
        }

        public async Task<Text> GetByIdAsync(int id)
        {
            return await _context.Texts.FirstOrDefaultAsync(m => m.Id == id);
        }

        public TextDetailVM GetMappedData(Text info)
        {
            TextDetailVM model = new()
            {
                Title = info.Title,
                Description= info.Description,
                CreatedDate = info.CreatedDate.ToString("MM.dd.yyyy"),

            };

            return model;
        }
    }
}
