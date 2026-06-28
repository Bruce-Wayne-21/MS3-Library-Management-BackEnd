using MS3_LMS.Models.RequestModel;

namespace MS3_LMS.IService.V1
{
    public interface IImageService
    {
        Task AddNewBook(ImageRequestModel imageRequestModel);
    }
}
