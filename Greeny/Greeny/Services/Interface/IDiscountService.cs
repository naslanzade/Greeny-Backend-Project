using Greeny.Areas.Admin.ViewModels.Country;
using Greeny.Areas.Admin.ViewModels.Discount;
using Greeny.Models;

namespace Greeny.Services.Interface
{
    public interface IDiscountService
    {
        Task<List<DiscountVM>> GetAllMappedDatas();

        Task<List<Disocunt>> GetAllAsync();

        Task<Disocunt> GetByIdAsync(int id);

        DiscountDetailVM GetMappedData(Disocunt discount);

        Task CreateAsync(DiscountCreatVM discount);

        Task DeleteAsync(int id);

        Task EditAsync(DiscountEditVM discount);
    }
}
