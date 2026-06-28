using MS3_LMS.Enity.Book;
using MS3_LMS.IRepository;
using MS3_LMS.IService.V1;
using MS3_LMS.Models.Request;
using MS3_LMS.Models.RequestModel;
using MS3_LMS.Models.ResponeModel;
using MS3_LMS.Repository;

namespace MS3_LMS.Service.V1
{
    public class BookService : IBookService
    {
        private readonly IBookRepository _bookRepository;
        private readonly ILogger<BookService> _logger;

        public BookService(IBookRepository bookRepository ,ILogger<BookService> logger)
        {
            _bookRepository = bookRepository;
            _logger = logger;

        }

        //public async Task CreateBookAsyn(BookRequestModel bookRequestModel)
        //{
        //    try
        //    {
        //        _logger.LogInformation("Creating book with the following details: {@BookRequestModel}", bookRequestModel);

        //        var book = new Book
        //        {
        //            Name = bookRequestModel.Name,
        //            Description = bookRequestModel.Description,
        //            ISBN = bookRequestModel.ISBN,
        //            PageCount = bookRequestModel.PageCount,
        //            AuthorId = bookRequestModel.AuthorId,
        //            GenreId = bookRequestModel.GenreId,
        //            LanguageId = bookRequestModel.LanguageId,
        //            PublisherId = bookRequestModel.PublisherId,
        //            BookType = bookRequestModel.BookType,
        //            Quantity = bookRequestModel.Quantity
        //        };

        //        _logger.LogInformation("Book entity created: {@Book}", book);
        //        await _bookRepository.CreateBookAsync(book);
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError("Error creating book: {Message}. StackTrace: {StackTrace}", ex.Message, ex.StackTrace);
        //        throw new Exception("An error occurred while creating the book.", ex);
        //    }
        //}


        public async Task<IReadOnlyList<Book>> GetBooksAsync()
        {
            return await _bookRepository.GetAllBooksAsync();
        }

        public async Task<bool> DeleteBook(Guid Id)
        {
            return await _bookRepository.DeleteBookByid(Id);
        }

        public async Task<Book> GetBookId(Guid id)
        {
            return await _bookRepository.GetBookByid(id);
        }

        public async Task<IReadOnlyList<Book>>FilterByGenre(string Genre)
        {
            return await _bookRepository.FilterByGenre(Genre);

        }
         public async Task<IReadOnlyList<Book>> FilterByLanguage(string Language)
        {
            return await _bookRepository.FilterByLanguage(Language);
        }
        
        //public async Task<IReadOnlyList<Book>> BasedOnBookType(Book.type booktype)
        //{
        //    return await _bookRepository.BasedOnBookType(booktype);
        //}

        public async Task<Book>CreateBook(Book book)
        {
            return await _bookRepository.PostNewBOok(book);
        }

        public async Task<IReadOnlyList<Book>>FilterByAuthor(Guid author)
        {
            return await _bookRepository.FilterByAuthor(author);
        }

        public async Task<List<Book>>GetEnumBAsedBooks(Book.type type)
        {
            try
            {
                var data = await _bookRepository.GetEnumBasedBooks(type);
                return data;
            }
            catch (Exception ex)
            {
               throw new Exception(ex .Message);
            }
        }

        public async Task<BookResponse> UpdateBook(Guid id, BookResponse bookResponse)
        {
            try
            {
                var book = await _bookRepository.GetBookByid(id);
                if (book == null)
                {
                    throw new Exception("Book not found");
                }
                book.Name = bookResponse.Name;
                book.PageCount = bookResponse.PageCount;
                book.AuthorId = bookResponse.AuthorId;
                book.GenreId = bookResponse.GenreId;
                book.PublisherId = bookResponse.PublisherId;
                book.LanguageId = bookResponse.LanguageId;

                book.Image.Image2Path= bookResponse.Image2Path;
                book.IsAvailable = bookResponse.IsAvailable;
                book.Quantity = bookResponse.Quantity;

                var data=await _bookRepository.UpdateBooks(book);

                var response = new BookResponse
                {
                    Name=data.Name,
                    PageCount=data.PageCount,
                    AuthorId=data.AuthorId, 
                    GenreId=data.GenreId,
                    PublisherId=data.PublisherId,
                    LanguageId=data.LanguageId,
                    Quantity=data.Quantity,

                    Image2Path=data.Image.Image2Path,
                    IsAvailable=data.IsAvailable
                };
                return response;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

    }
}
