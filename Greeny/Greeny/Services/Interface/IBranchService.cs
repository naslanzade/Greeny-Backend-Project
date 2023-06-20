using Greeny.Models;

namespace Greeny.Services.Interface
{
    public interface IBranchService
    {
        Task<List<Branch>> GetAllAsync();
    }
}
