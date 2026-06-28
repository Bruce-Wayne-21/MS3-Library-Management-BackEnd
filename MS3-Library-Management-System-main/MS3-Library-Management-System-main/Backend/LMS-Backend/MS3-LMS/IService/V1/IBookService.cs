using MS3_LMS.Enity.Book;
using MS3_LMS.Models.Request;
using MS3_LMS.Models.RequestModel;
using MS3_LMS.Models.ResponeModel;

namespace MS3_LMS.IService.V1
{
    public interface IBookService
    {
      //  Task CreateBookAsyn(BookRequestModel bookRequestModel);
        Task<IReadOnlyList<Book>> GetBooksAsync();
        Task<bool> DeleteBook(Guid Id);
        Task<Book> GetBookId(Guid id);
        Task<IReadOnlyList<Book>> FilterByLanguage(string Language);
        Task<IReadOnlyList<Book>> FilterByGenre(string Genre);
        //Task<IReadOnlyList<Book>> BasedOnBookType(Book.type booktype);
        Task<Book> CreateBook(Book book);
        Task<IReadOnlyList<Book>> FilterByAuthor(Guid author);

        Task<List<Book>> GetEnumBAsedBooks(Book.type type);
        Task<BookResponse> UpdateBook(Guid id, BookResponse bookResponse);

    }
}
