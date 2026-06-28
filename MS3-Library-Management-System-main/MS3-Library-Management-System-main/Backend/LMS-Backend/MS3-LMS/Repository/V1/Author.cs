using Microsoft.EntityFrameworkCore;
using MS3_LMS.Enity.Book;
using MS3_LMS.Enity.Core;
using MS3_LMS.IRepository;
using MS3_LMS.LMSDbcontext;

namespace MS3_LMS.Repository.V1
{
    public class Author: IAothorRepository
    {
        private readonly LMSContext _context;

        public Author(LMSContext context)
        {
            _context = context;
        }


        public async Task<Enity.Book.Author>PostNewAuthor(Enity.Book.Author author)
        {
            try
            {
                var data = await _context.Authors.AddAsync(author);
                await _context.SaveChangesAsync();
                return data.Entity;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<List<Enity.Book.Author>> GetAllAuthor()
        {
            try
            {
                
                var data = await _context.Authors.ToListAsync();

                
                return data;
            }
            catch (Exception ex)
            {
               
                throw new Exception($"Error fetching authors: {ex.Message}", ex);
            }
        }

        public async Task<List<Book>>GetAuthorBooksByID(Guid id)
        {
            try
            {
                var data = await _context.Books.
                    Include(i => i.Genre).
                    Include(i => i.Image).
                    Include(p => p.Publisher).
                    Where(a=>a.AuthorId==id).
                    ToListAsync();
                if(data == null)
                {
                    throw new Exception();
                }
                return data;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }





    }
}
