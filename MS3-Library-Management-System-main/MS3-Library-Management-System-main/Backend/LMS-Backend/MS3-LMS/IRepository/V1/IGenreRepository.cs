using MS3_LMS.Enity.Book;

namespace MS3_LMS.IRepository
{
    public interface IGenreRepository
    {
        Task<Genre> CreateGenre(Genre genre);
        Task<List<Genre>> GetAllGenre();
    }
}
