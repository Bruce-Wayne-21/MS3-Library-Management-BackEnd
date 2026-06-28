using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MS3_LMS.Enity.Book;
using MS3_LMS.IService.V1;
using MS3_LMS.LMSDbcontext;
using MS3_LMS.Models.Request;
using MS3_LMS.Models.RequestModel;
using MS3_LMS.Models.ResponeModel;
using NuGet.Packaging.Signing;
using UglyToad.PdfPig;

namespace MS3_LMS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly LMSContext _context;
        private readonly IBookService _bookService;
        private readonly ILogger<BooksController> _logger;
       

        public BooksController(LMSContext context, IBookService bookService, ILogger<BooksController> logger)
        {
            _context = context;
            _bookService = bookService;
            _logger = logger;
        }

        // GET: api/Books
        [HttpGet]
        public async Task<IActionResult> GetallBooks()
        {
            var data = await _bookService.GetBooksAsync();

            if (data == null)
            {
                return NotFound();
            }
            return Ok(data);
        }

        // GET: api/Books/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Book>> GetBookByid(Guid id)
        {
            var book = await _bookService.GetBookId(id);


            if (book == null)
            {
                return NotFound();
            }

            return book;
        }

        // PUT: api/Books/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("UpdateBook{id}")]
        
        public async Task<IActionResult> PutBook(Guid id,BookResponse bookResponse)
        {
            try
            {
                var data=await _bookService.UpdateBook(id, bookResponse);
                return Ok(data);
            }
            catch (Exception ex)
            {
                throw new Exception();
            }
        }

        // POST: api/Books
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPost]
        //public async Task<ActionResult> CreateBookAsync(BookRequestModel bookRequestModel)
        //{
        //    try
        //    {
        //        if (bookRequestModel == null)
        //        {
        //            _logger.LogError("Received null book request model.");
        //            return BadRequest(new { Message = "The book request model cannot be null." });
        //        }

   
        //        if (string.IsNullOrWhiteSpace(bookRequestModel.Name))
        //        {
        //            _logger.LogError("Validation failed: Book name is required.");
        //            return BadRequest(new { Message = "Validation failed: Book name is required." });
        //        }

        //        if (string.IsNullOrWhiteSpace(bookRequestModel.ISBN))
        //        {
        //            _logger.LogError("Validation failed: ISBN is required.");
        //            return BadRequest(new { Message = "Validation failed: ISBN is required." });
        //        }

        //        if (bookRequestModel.AuthorId == Guid.Empty)
        //        {
        //            _logger.LogError("Validation failed: AuthorId is required.");
        //            return BadRequest(new { Message = "Validation failed: AuthorId is required." });
        //        }

        //        if (bookRequestModel.PublisherId == Guid.Empty)
        //        {
        //            _logger.LogError("Validation failed: PublisherId is required.");
        //            return BadRequest(new { Message = "Validation failed: PublisherId is required." });
        //        }

        //        if (bookRequestModel.LanguageId == Guid.Empty)
        //        {
        //            _logger.LogError("Validation failed: LanguageId is required.");
        //            return BadRequest(new { Message = "Validation failed: LanguageId is required." });
        //        }

        //        if (bookRequestModel.GenreId == Guid.Empty)
        //        {
        //            _logger.LogError("Validation failed: GenreId is required.");
        //            return BadRequest(new { Message = "Validation failed: GenreId is required." });
        //        }

        //        _logger.LogInformation("Validation passed for book request model.");
        //        await _bookService.CreateBookAsyn(bookRequestModel);

        //        return Ok(bookRequestModel);
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError("Error in CreateBookAsync: {Message}", ex.Message);
        //        return StatusCode(500, new { Message = "Error creating the book.", Details = ex.Message });
        //    }
        //}




        // DELETE: api/Books/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBook(Guid id)
        {
            var book = await _bookService.DeleteBook(id);
            return Ok(book);


        }

        private bool BookExists(Guid id)
        {
            return _context.Books.Any(e => e.Bookid == id);
        }
        [HttpGet("Genre")]
        public async Task<IActionResult> FilterBookByGenre(string Genre)
        {
            var book = await _bookService.FilterByGenre(Genre);
            return Ok(book);
        }

        [HttpGet("GetAuthorBooks")]
        public async Task<IActionResult>FilterByauthor(Guid Author)
        {
            var data=await _bookService.FilterByAuthor(Author);
            return Ok(data);
        }

        [HttpGet("Languageatype")]
        public async Task<IActionResult> SortByLanguage(string Language)
        {
            var book=await _bookService.FilterByLanguage(Language);
            return Ok(book);
        }


        //[HttpGet("BookType")]
        //public async Task<IActionResult>FilterByBookType(Book.type bookType)
        //{
        //    var data=await _bookService.BasedOnBookType(bookType);
        //    return Ok(data);
        //}

        [HttpPost("create-with-extract")]
        public async Task<IActionResult> CreateBookWithExtract([FromBody] CreateBookRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var publisher = await _context.Publishers.FindAsync(request.PublisherId);
            if (publisher == null)
            {
                return NotFound(new { message = "Publisher not found" });
            }

            if (publisher.PublishDate != request.PublishDate)
            {
                publisher.PublishDate = request.PublishDate;
            }

            //string extractedText = string.Empty;
            //if (request.BookType == Book.type.EBook || request.BookType == Book.type.Both)
            //{
            //    if (string.IsNullOrEmpty(request.FilePath))
            //    {
            //        return BadRequest("File path is required for eBooks.");
            //    }

            //    try
            //    {
            //        if (!System.IO.File.Exists(request.FilePath))
            //        {
            //            return NotFound(new { message = "The specified file does not exist." });
            //        }

            //        using var stream = System.IO.File.OpenRead(request.FilePath);
            //        using var pdfDocument = PdfDocument.Open(stream);

            //        var textContent = new List<string>();
            //        foreach (var page in pdfDocument.GetPages())
            //        {
            //            textContent.Add(page.Text);
            //        }

            //        extractedText = string.Join("\n", textContent);
            //    }
            //    catch (Exception ex)
            //    {
            //        return StatusCode(500, new { message = "Error extracting text from PDF.", details = ex.Message });
            //    }
            //}

            var newBook = new Book
            {
                Bookid = Guid.NewGuid(),
                Name = request.Name,
                Description = request.Description,
                ISBN = request.ISBN,
                PageCount = request.PageCount,
                IsAvailable = request.IsAvailable,
                Quantity = request.Quantity,
                BookType = request.BookType,
                AuthorId = request.AuthorId,
                PublisherId = request.PublisherId,
                LanguageId = request.LanguageId,
                GenreId = request.GenreId,
                FilePath = request.FilePath,
               
            };

            var images = new List<Image>();
            if (!string.IsNullOrEmpty(request.Image2Path))
            {
                images.Add(new Image
                {
                    ID = Guid.NewGuid(),
                    Image2Path = request.Image2Path,

                    Bookid = newBook.Bookid
                });
            }

            try
            {
                await _context.Books.AddAsync(newBook);

                if (images.Any())
                {
                    await _context.Images.AddRangeAsync(images);
                }

                await _context.SaveChangesAsync();

                return Ok(new { message = "Book created successfully", book = newBook });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error creating the book", details = ex.Message });
            }
        }


        [HttpGet("Get-multipletypeBooks")]
        public async Task<IActionResult>GetByBookType(Book.type type)
        {
            try
            {
                var response = await _bookService.GetEnumBAsedBooks(type);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Can't get Books ", details = ex.Message });
            }
        }


        [HttpPut("UpdateBookCopies/{BookId}")]
        public async Task<IActionResult> UpdateBookCopies(Guid BookId, [FromBody] int decrementBy)
        {
            try
            {
                var data = await _context.Books.FindAsync(BookId);
                if(data == null)
                {
                    return BadRequest("Book Not Found");
                }
                data.Quantity=decrementBy;
                _context.Books.Update(data);
                _context.SaveChanges();
                return Ok(data);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

    }
}
