using MS3_LMS.Enity.Book;

namespace MS3_LMS.IRepository
{
    public interface IRatingRepository
    {
        Task<Rating> PostRating(Rating rating);
    }
}
