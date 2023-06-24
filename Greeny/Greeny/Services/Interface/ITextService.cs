using Greeny.Areas.Admin.ViewModels.Author;
using Greeny.Areas.Admin.ViewModels.Text;
using Greeny.Models;

namespace Greeny.Services.Interface
{
    public interface ITextService
    {
        Task<Text> GetAllAsync();

        Task<List<TextVM>> GetAllMappedDatas();

        Task<List<Text>> GetAllDatasAsync();

        Task<Text> GetByIdAsync(int id);

        TextDetailVM GetMappedData(Text info);

        Task CreateAsync(TextCreateVM text);

        Task DeleteAsync(int id);

        Task EditAsync(TextEditVM text);
    }
}
