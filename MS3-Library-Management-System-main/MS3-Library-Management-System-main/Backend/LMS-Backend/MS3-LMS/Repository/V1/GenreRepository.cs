using Microsoft.EntityFrameworkCore;
using MS3_LMS.Enity.Book;
using MS3_LMS.IRepository;
using MS3_LMS.LMSDbcontext;

namespace MS3_LMS.Repository.V1
{
    public class GenreRepository: IGenreRepository
    {
        private readonly LMSContext _context;

        public GenreRepository(LMSContext context)
        {
            _context = context;
        }

        public async Task<Genre>CreateGenre(Genre genre)
        {
            try
            {
                var data = await _context.AddAsync(genre);
                await _context.SaveChangesAsync();
                return data.Entity;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<Genre>> GetAllGenre()
        {
            try
            {
                
                if (_context == null)
                    throw new Exception("Database context is not initialized.");

                var data = await _context.Genres.ToListAsync();

               
                if (data == null || !data.Any())
                    throw new Exception("No genres found in the database.");

                return data;
            }
            catch (Exception ex)
            {
              
                Console.WriteLine($"Error in GetAllGenre: {ex.Message}");
                throw; 
            }
        }

    }
}
