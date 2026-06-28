using Microsoft.EntityFrameworkCore;
using MS3_LMS.Enity.Book;
using MS3_LMS.IRepository;
using MS3_LMS.LMSDbcontext;


namespace MS3_LMS.Repository.V1
{
    public class BookRepo : IBookRepository
    {
        private readonly ILogger<BookRepo> _logger;
        private LMSContext _context;

        public BookRepo(LMSContext context,ILogger<BookRepo>logger)
        {
            _context = context;
            _logger=logger;
        }

        public async Task<Book>PostNewBOok(Book book)
        {
            var data=await _context.Books.AddAsync(book);
            await _context.SaveChangesAsync();
            return data.Entity;
        }
        



        public async Task<IReadOnlyList<Book>> GetAllBooksAsync()
        {
            try
            {
                
                var books = await _context.Books
                    .Include(b => b.Author)
                    .Include(b => b.Genre)
                    .Include(b => b.Publisher)
                    .Include(i=>i.Image)
                    .Include(l=>l.Language)
                    //.Include(b=>b.BookLends)
                   //.Include(r=>r.Ratings)
                    .AsNoTracking()
                    .Where(b=>b.BookType==Book.type.Manual)
                    .ToListAsync();

               

                return books;
            }
            catch (Exception ex)
            {
                
                throw new Exception("An error occurred while retrieving books.", ex);
            }
        }


       

        public async Task<bool>DeleteBookByid(Guid Id)
        {
            try
            {
                var book = await _context.Books.FindAsync(Id);
                if (book == null)
                {
                    throw new Exception();
                }
                _context.Books.Remove(book);
                await _context.SaveChangesAsync();

                return true;
            }
            catch(Exception ex)
            {

                throw new Exception();

            }
        }

        public async Task<Book>GetBookByid(Guid id)
        {
            try
            {
                   var book=await _context.Books.
                    Include(g=>g.Genre).
                    Include(i=>i.Image)
                    .Include(p=>p.Publisher).
                    Include(a=>a.Author).
                    Include(l=>l.Language).
                    Include(r=>r.Ratings).
                    FirstOrDefaultAsync(b=>b.Bookid==id);
                   

                if (book == null)
                {
                    _logger.LogWarning("Book with id notfound");
                    throw new KeyNotFoundException($"Book with id `{id}` not found");
                }
                return book;
            }
            catch(KeyNotFoundException Ex)
            {
                _logger.LogWarning(Ex.Message);

                throw;
            }
            catch(Exception Ex)
            {
                _logger.LogError(Ex.Message);
                throw new InvalidOperationException();
            }
        }


        public async Task<IReadOnlyList<Book>>FilterByGenre(string Genre)
        {
            try
            {
                var data = _context.Books.Include(x => x.Genre)
                    .Include(i=>i.Image)
                    .Include(l=>l.Language)
                    .Include(l=>l.Author)
                    .Include(p=>p.Publisher)
                    .AsNoTracking()
                    .AsQueryable();

                if (!string.IsNullOrWhiteSpace(Genre))
                {
                    data=data.Where(g=>g.Genre.BookGenre==Genre);

                };
                return await data.ToListAsync();
            }
            catch(Exception Ex)
            {
                throw new Exception("Can not filter by Genre");
            }
        }

        public async Task<IReadOnlyList<Book>>FilterByLanguage(string Language)
        {
            try
            {
                var data = _context.Books.Include(l => l.Language)
                    .Include(i => i.Image)
                    .Include(l => l.Genre)
                    .Include(l => l.Author)
                    .Include(p => p.Publisher)
                    .AsNoTracking().AsQueryable();
                if (!string.IsNullOrWhiteSpace(Language))
                {
                    data = data.Where(l => l.Language.TypeOfLanguage==Language);
                }
                return await data.ToListAsync();
            }
            catch(Exception Ex)
            {
                throw new Exception("can not filter Book by Language");
            }
        }

        public async Task<IReadOnlyList<Book>>FilterByAuthor(Guid authorId)
        {
            try
            {
                var author =  _context.Books.Include(l => l.Language)
                    .Include(i => i.Image)
                    .Include(g => g.Genre)
                    .Include(l => l.Author)
                    .Include(p => p.Publisher)
                    .AsNoTracking().AsQueryable();
                if (authorId != Guid.Empty)
                {
                    author = author.Where(b => b.Author.AuthorId == authorId);
                }
                return await author.ToListAsync();
            }
            catch(Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
        }

        public async Task<List<Book>>GetEnumBasedBooks(Book.type type)
        {
            try
            {
                var books = await _context.Books.Include(l => l.Language)
                    .Include(i => i.Image)
                    .Include(g => g.Genre)
                    .Include(l => l.Author)
                    .Include(p => p.Publisher)
                    .Where(a => a.BookType == type)
                    .ToListAsync();

                return books;
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
        }


        public async Task<Book> UpdateBooks(Book book)
        {
            try
            {
                _context.Books.Update(book);
                await _context.SaveChangesAsync();
                return book; 
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while updating the book: {ex.Message}", ex);
            }
        }







    }
}
