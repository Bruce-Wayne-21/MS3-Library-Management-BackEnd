using MS3_LMS.Enity.Book;

namespace MS3_LMS.IService.V1
{
    public interface IRatingService
    {
        Task<Rating> PostRatingAsync(Models.RequestModel.RatingRequestModel ratingRequestModel);
    }
}
