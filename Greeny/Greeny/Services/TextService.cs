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
        public async Task<Text> GetAllAsync()
        {
            return await _context.Texts.FirstOrDefaultAsync();
        }
    }
}
