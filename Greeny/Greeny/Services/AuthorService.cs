using Greeny.Areas.Admin.ViewModels.Advert;
using Greeny.Areas.Admin.ViewModels.Author;
using Greeny.Data;
using Greeny.Models;
using Greeny.Services.Interface;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Asn1.Ocsp;

namespace Greeny.Services
{
    public class AuthorService : IAuthorService
    {

        private readonly AppDbContext _context;

        public AuthorService(AppDbContext context)
        {
            _context = context;
        }
        public async Task CreateAsync(AuthorCreateVM author)
        {
            Author newAuthor = new()
            {
                FullName = author.FullName,

            };
            await _context.Authors.AddAsync(newAuthor);

            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            Author author =await GetByIdAsync(id);

            _context.Remove(author);
            await _context.SaveChangesAsync();
        }

        public async Task EditAsync(AuthorEditVM author)
        {
            Author newAuthor = new()
            {
                Id = author.Id,
                FullName = author.FullName,
            };

            _context.Update(newAuthor);

            await _context.SaveChangesAsync();
        }

        public async Task<List<Author>> GetAllAsync()
        {
            return await _context.Authors.ToListAsync();
        }

        public async Task<List<AuthorVM>> GetAllMappedDatas()
        {
            List<AuthorVM> lits = new();

            List<Author> infos = await GetAllAsync();

            foreach (var info in infos)
            {
                AuthorVM model = new()
                {
                    Id = info.Id,
                    FullName=info.FullName,

                };

                lits.Add(model);
            }

            return lits;
        }

        public async Task<Author> GetByIdAsync(int id)
        {
            return await _context.Authors.FirstOrDefaultAsync(m => m.Id == id);
        }

        public AuthorDetailVM GetMappedData(Author info)
        {
            AuthorDetailVM model = new()
            {
                FullName = info.FullName,
                CreatedDate = info.CreatedDate.ToString("MM.dd.yyyy"),

            };

            return model;
        }
    }
}
