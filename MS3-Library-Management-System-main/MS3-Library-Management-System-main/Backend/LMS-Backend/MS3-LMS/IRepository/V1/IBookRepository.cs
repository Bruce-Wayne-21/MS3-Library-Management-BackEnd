using MS3_LMS.Enity.Book;

namespace MS3_LMS.IRepository
{
    public interface IBookRepository
    {
       
        Task<IReadOnlyList<Book>> GetAllBooksAsync();
        Task<bool> DeleteBookByid(Guid Id);
        Task<Book> GetBookByid(Guid id);
        Task<IReadOnlyList<Book>> FilterByLanguage(string Language);
        Task<IReadOnlyList<Book>> FilterByGenre(string Genre);
        //Task<IReadOnlyList<Book>> BasedOnBookType(Book.type booktype);
        Task<Book> PostNewBOok(Book book);
        Task<IReadOnlyList<Book>> FilterByAuthor(Guid Author);

        Task<List<Book>> GetEnumBasedBooks(Book.type type);
         Task<Book> UpdateBooks(Book book);
    }
}
