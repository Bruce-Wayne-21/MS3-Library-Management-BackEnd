using MS3_LMS.Enity.Book;
using MS3_LMS.IRepository;
using MS3_LMS.IService.V1;
using MS3_LMS.Models.RequestModel;
using MS3_LMS.Models.ResponeModel;

namespace MS3_LMS.Service.V1
{
    public class GenreService: IGenreService
    {
        private readonly IGenreRepository _repository;

        public GenreService(IGenreRepository repository)
        {
            _repository = repository;
        }

        public async Task CreteGenre(GenreRequestModel genreRequestModel)
        {
            try
            {
                var genre = new Genre
                {
                    BookGenre = genreRequestModel.BookGenre,
                    Description = genreRequestModel.Description
                };
                await _repository.CreateGenre(genre);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<GenreResponseModel>> GetAllGenres()
        {
            try
            {
                
                var genres = await _repository.GetAllGenre();

               
                if (genres == null || !genres.Any())
                    throw new Exception("No genres available.");

                var response = genres.Select(genre => new GenreResponseModel
                {
                    GenreId = genre.GenreId,
                    Description = genre.Description,
                    BookGenre = genre.BookGenre
                }).ToList();

                return response;
            }
            catch (Exception ex)
            {
                
                Console.WriteLine($"Error in GetAllGenres: {ex.Message}");
                throw;
            }
        }




    }
}
