using Greeny.Data;
using Greeny.Services.Interface;

namespace Greeny.Services
{
    public class SettingService : ISettingService
    {
        private readonly AppDbContext _context;

        public SettingService(AppDbContext context)
        {
            _context = context;
        }

        public Dictionary<string, string> GetAll()
        {
            return _context.Settings.AsEnumerable().ToDictionary(m => m.Key, m => m.Value);
        }
    }
}
