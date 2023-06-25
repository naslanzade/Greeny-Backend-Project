using Fiorello.Helpers;
using Greeny.Areas.Admin.ViewModels.Branch;
using Greeny.Data;
using Greeny.Models;
using Greeny.Services.Interface;
using Microsoft.EntityFrameworkCore;


namespace Greeny.Services
{
    public class BranchService : IBranchService
    {

        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;


        public BranchService(AppDbContext context,
                             IWebHostEnvironment env)
        {
            
            _context = context;
            _env = env;
        }

        public async Task CreateAsync(BranchCreateVM model, List<IFormFile> images)
        {
            foreach (var item in images)
            {
                string fileName = Guid.NewGuid().ToString() + "_" + item.FileName;

                await item.SaveFileAsync(fileName, _env.WebRootPath, "images/branch");


                Branch branch = new()
                {
                    Image = fileName,
                    City = model.City,
                    Address=model.Address,
                    CountryId=model.CountryId
                };

                await _context.Branches.AddAsync(branch);

            }

            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            Branch branch = await GetByIdAsnyc(id);

            _context.Branches.Remove(branch);

            await _context.SaveChangesAsync();

            string path = Path.Combine(_env.WebRootPath, "images/branch", branch.Image);

            if (File.Exists(path))
            {
                File.Delete(path);
            }
        }

        public async Task EditAsync(int branchId, BranchEditVM model, IFormFile newImage)
        {
            var branch = await GetByIdAsnyc(branchId);

            string oldPath = Path.Combine(_env.WebRootPath, "images/branch", newImage.FileName);

            if (File.Exists(oldPath))
            {
                File.Delete(oldPath);
            }

            string fileName = Guid.NewGuid().ToString() + "_" + newImage.FileName;

            await newImage.SaveFileAsync(fileName, _env.WebRootPath, "images/branch");

            model.Image = fileName;

            branch.City = model.City;
            branch.Address=model.Address;
            branch.CountryId = model.CountryId;
            branch.Image = fileName;
            await _context.SaveChangesAsync();
        }

        public async Task<List<Branch>> GetAllAsync()
        {
            return await _context.Branches.Include(m=>m.Country).ToListAsync();
        }

        public async Task<List<Branch>> GetAllDatasAsync()
        {
            return await _context.Branches.Include(m=>m.Country).ToListAsync();
        }

        public async Task<Branch> GetByIdAsnyc(int? id)
        {
            return await _context.Branches.FirstOrDefaultAsync(m=>m.Id==id);
        }
      

        public BranchDetailVM GetMappedData(Branch branch)
        {
            return new BranchDetailVM
            {
                City = branch.City,
                CountyName = branch.Country.Name,
                Address=branch.Address,
                Image=branch.Image,
                CreatedDate = branch.CreatedDate.ToString("MMMM dd, yyyy"),
            };
        }

        public async Task<List<BranchVM>> GetMappedDatas()
        {
            List<BranchVM> list = new();

            List<Branch> infos = await GetAllDatasAsync();

            foreach (var info in infos)
            {
                BranchVM model = new()
                {
                    Id = info.Id,
                    City = info.City,
                    Image = info.Image,
                    CountyName=info.Country.Name
                };

                list.Add(model);
            }

            return list;
        }

        public async Task<Branch> GetWithIncludes(int? id)
        {
            return await _context.Branches.Where(m => m.Id == id)
                                               .Include(m => m.Country)
                                                .FirstOrDefaultAsync();
        }
    }
}
