using Greeny.Areas.Admin.ViewModels.Team;
using Greeny.Areas.Admin.ViewModels.Testimonial;
using Greeny.Models;

namespace Greeny.Services.Interface
{
    public interface ITestimonialService
    {
        Task<List<Testimonial>> GetAllAsync();

        Task<List<Testimonial>> GetAllDatasAsync();
        Task<Testimonial> GetByIdAsnyc(int? id);
        Task<List<TestimonialVM>> GetMappedDatas();
        TestimonialDetailVM GetMappedData(Testimonial team);
        Task CreateAsync(TestimonialCreateVM model, List<IFormFile> images);
        Task EditAsync(int teamId, TestimonialEditVM model, IFormFile newImage);
        Task DeleteAsync(int id);
        Task<Testimonial> GetWithIncludes(int? id);
    }
}

