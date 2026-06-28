using MS3_LMS.Enity.Book;
using MS3_LMS.Models.RequestModel;
using MS3_LMS.Models.ResponeModel;

namespace MS3_LMS.IService.V1
{
    public interface IAuthorService
    {
        Task PostNewAuthor(AuthorRequestModel authorRequestModel);

        Task<List<AuthorResponseModel>> GelAuthors();
        Task<List<AuthorBooksResponse>> GetAuthorsBooksById(Guid id);
    }
}
