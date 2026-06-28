using MS3_LMS.Models.RequestModel;
using MS3_LMS.Models.ResponeModel;

namespace MS3_LMS.IService.V1
{
    public interface IGenreService
    {
        Task CreteGenre(GenreRequestModel genreRequestModel);
        Task<List<GenreResponseModel>> GetAllGenres();
    }
}
