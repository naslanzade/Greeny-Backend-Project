using Greeny.Areas.Admin.ViewModels.Discount;
using Greeny.Areas.Admin.ViewModels.Milestone;
using Greeny.Data;
using Greeny.Models;
using Greeny.Services.Interface;
using Microsoft.EntityFrameworkCore;

namespace Greeny.Services
{
    public class MilestoneService : IMilestoneService
    {

        private readonly AppDbContext _context;

        public MilestoneService(AppDbContext context)
        {
         
            _context = context;
        }

        public async Task CreateAsync(MilestoneCreateVM milestone)
        {
            Milestone newMilestone = new()
            {
                Type = milestone.Type,
                Counter = milestone.Counter,

            };
            await _context.Milestones.AddAsync(newMilestone);

            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            Milestone milestone = await GetByIdAsync(id);

            _context.Remove(milestone);
            await _context.SaveChangesAsync();
        }

        public async Task EditAsync(MilestoneEditVM milestone)
        {
            Milestone newMilestone = new()
            {
                Id = milestone.Id,
                Type = milestone.Type,
                Counter = milestone.Counter,
            };

            _context.Update(newMilestone);

            await _context.SaveChangesAsync();
        }

        public async Task<List<Milestone>> GetAllAsync()
        {
            return await _context.Milestones.ToListAsync();
        }

        public async Task<List<MilestoneVM>> GetAllMappedDatas()
        {
            List<MilestoneVM> list = new();

            List<Milestone> infos = await GetAllAsync();

            foreach (var info in infos)
            {
                MilestoneVM model = new()
                {
                    Id = info.Id,
                    Type = info.Type,

                };

                list.Add(model);
            }

            return list;
        }

        public async Task<Milestone> GetByIdAsync(int id)
        {
            return await _context.Milestones.FirstOrDefaultAsync(m => m.Id == id);
        }

        public MilestoneDetailVM GetMappedData(Milestone milestone)
        {
            MilestoneDetailVM model = new()
            {
                Type = milestone.Type,
                Counter = milestone.Counter,
                CreatedDate = milestone.CreatedDate.ToString("MM.dd.yyyy"),

            };

            return model;
        }
    }
}
