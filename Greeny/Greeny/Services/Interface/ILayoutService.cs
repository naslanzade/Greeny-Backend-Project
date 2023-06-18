using Greeny.ViewModels;

namespace Greeny.Services.Interface
{
    public interface ILayoutService
    {
        Task<LayoutVM> GetAllDatas();
    }
}
