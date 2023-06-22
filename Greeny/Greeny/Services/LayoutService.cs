using Greeny.Data;
using Greeny.Services.Interface;
using Greeny.ViewModels;
using Org.BouncyCastle.Bcpg.OpenPgp;

namespace Greeny.Services
{
    public class LayoutService : ILayoutService
    {

        private readonly AppDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IBasketService _basketService;
        private readonly IWishlistService _wishlistService;


        public LayoutService(AppDbContext context,
                            IBasketService basketService,
                            IWishlistService wishlistService)
        {
            _context = context;
            _basketService = basketService;
            _wishlistService = wishlistService;
            
        }
        public async Task<LayoutVM> GetAllDatas()
        {
            var datas=_context.Settings.AsEnumerable().ToDictionary(m=>m.Key,m=>m.Value);
            int count = _basketService.GetCount();
            int wishlistCount=_wishlistService.GetCount();
            return new LayoutVM { SettingDatas = datas, BasketCount = count,WishList=wishlistCount };
        }
    }
}
