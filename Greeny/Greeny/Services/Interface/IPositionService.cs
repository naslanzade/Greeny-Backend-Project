using Greeny.Areas.Admin.ViewModels.Author;
using Greeny.Areas.Admin.ViewModels.Position;
using Greeny.Models;

namespace Greeny.Services.Interface
{
    public interface IPositionService
    {
        Task<List<PositionVM>> GetAllMappedDatas();

        Task<List<Position>> GetAllAsync();

        Task<Position> GetByIdAsync(int id);

        PositionDetailVM GetMappedData(Position info);

        Task CreateAsync(PositionCreateVM position);

        Task DeleteAsync(int id);

        Task EditAsync(PositionEditVM position);
    }
}
