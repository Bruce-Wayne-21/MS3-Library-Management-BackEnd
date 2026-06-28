using MS3_LMS.Models.RequestModel;
using MS3_LMS.Models.ResponeModel;

namespace MS3_LMS.IService.V1
{
    public interface IPublisherService
    {
        Task CretePublisher(PublisherRequestModel publisherRequestModel);
        Task<List<PublisherResponseMOdel>> GetAllPublishers();
    }
}
