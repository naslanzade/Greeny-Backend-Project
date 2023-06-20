using Greeny.Models;

namespace Greeny.Services.Interface
{
    public interface ITextService
    {
        Task<Text> GetAllAsync();
    }
}
