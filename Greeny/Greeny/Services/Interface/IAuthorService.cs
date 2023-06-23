using Greeny.Areas.Admin.ViewModels.Advert;
using Greeny.Areas.Admin.ViewModels.Author;
using Greeny.Models;

namespace Greeny.Services.Interface
{
    public interface IAuthorService
    {
       
        Task<List<AuthorVM>> GetAllMappedDatas();

        Task<List<Author>> GetAllAsync();

        Task<Author> GetByIdAsync(int id);

        AuthorDetailVM GetMappedData(Author info);

        Task CreateAsync(AuthorCreateVM author);

        Task DeleteAsync(int id);

        Task EditAsync(AuthorEditVM author);
    }
}
