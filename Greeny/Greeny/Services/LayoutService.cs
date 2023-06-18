using Greeny.Data;
using Greeny.Services.Interface;
using Greeny.ViewModels;

namespace Greeny.Services
{
    public class LayoutService : ILayoutService
    {

        private readonly AppDbContext _context;

        public LayoutService(AppDbContext context)
        {
            _context = context;
        }
        public async Task<LayoutVM> GetAllDatas()
        {
            var datas=_context.Settings.AsEnumerable().ToDictionary(m=>m.Key,m=>m.Value);

            return new LayoutVM { SettingDatas = datas };
        }
    }
}
