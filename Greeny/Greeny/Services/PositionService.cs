using Greeny.Areas.Admin.ViewModels.Author;
using Greeny.Areas.Admin.ViewModels.Position;
using Greeny.Data;
using Greeny.Models;
using Greeny.Services.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Greeny.Services
{
   
    public class PositionService : IPositionService
    {

        private readonly AppDbContext _context;

        public PositionService(AppDbContext context)
        {
            _context = context;
        }


        public async Task CreateAsync(PositionCreateVM position)
        {
            Position newPosition = new()
            {
                Name = position.Name,

            };
            await _context.Positions.AddAsync(newPosition);

            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            Position position = await GetByIdAsync(id);

            _context.Remove(position);
            await _context.SaveChangesAsync();
        }

        public async Task EditAsync(PositionEditVM position)
        {
            Position newPosition = new()
            {
                Id = position.Id,
                Name = position.Name,
            };

            _context.Update(newPosition);

            await _context.SaveChangesAsync();
        }

        public async Task<List<Position>> GetAllAsync()
        {
            return await _context.Positions.ToListAsync();
        }

        public async Task<List<PositionVM>> GetAllMappedDatas()
        {
            List<PositionVM> lits = new();

            List<Position> infos = await GetAllAsync();

            foreach (var info in infos)
            {
                PositionVM model = new()
                {
                    Id = info.Id,
                    Name = info.Name,
                };

                lits.Add(model);
            }

            return lits;
        }

        public async Task<Position> GetByIdAsync(int id)
        {
            return await _context.Positions.FirstOrDefaultAsync(m => m.Id == id);
        }

        public PositionDetailVM GetMappedData(Position info)
        {
            PositionDetailVM model = new()
            {
                Name = info.Name,
                CreatedDate = info.CreatedDate.ToString("MM.dd.yyyy"),

            };

            return model;
        }
    }
}
