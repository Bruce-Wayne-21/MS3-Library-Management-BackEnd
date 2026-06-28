using MS3_LMS.Enity.Book;
using MS3_LMS.IRepository;
using MS3_LMS.LMSDbcontext;

namespace MS3_LMS.Repository.V1
{
    public class RatingRepository: IRatingRepository
    {
        private readonly LMSContext _context;

        public RatingRepository(LMSContext context)
        {
            _context = context;
        }

        public async Task<Rating>PostRating(Rating rating)
        {
            try
            {
                if (rating.StarCount < 1 || rating.StarCount > 5)
                {
                    throw new Exception("star count must be between 1 and 5 ");
                }
                   
                var data = await _context.Rating.AddAsync(rating);
                 await _context.SaveChangesAsync();

                return data.Entity;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
