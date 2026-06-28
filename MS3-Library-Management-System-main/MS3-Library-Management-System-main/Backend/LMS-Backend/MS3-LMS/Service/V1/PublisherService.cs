using MS3_LMS.Enity.Book;
using MS3_LMS.IRepository.V1;
using MS3_LMS.IService.V1;
using MS3_LMS.Models.RequestModel;
using MS3_LMS.Models.ResponeModel;

namespace MS3_LMS.Service.V1
{
    public class PublisherService : IPublisherService
    {
        private readonly IPublisherRepository _publisherRepository;

        public PublisherService(IPublisherRepository publisherRepository)
        {
            _publisherRepository = publisherRepository;
        }

        public async Task CretePublisher(PublisherRequestModel publisherRequestModel)
        {
            try
            {
                var pub = new Publisher
                {
                    Name = publisherRequestModel.PublisherName,
                    Description = publisherRequestModel.Description,
                    Details = publisherRequestModel.Details,
                   
                };
                await _publisherRepository.PostPublisher(pub);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<PublisherResponseMOdel>> GetAllPublishers()
        {
            try
            {
                var data = await _publisherRepository.GetAllpublisger();

                var response = data.Select(s => new PublisherResponseMOdel
                {
                    PublisherId = s.PublisherId,
                    PublisherName = s.Name,
                    Description = s.Description,
                    Details = s.Details,
                }).ToList();
                return response;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        
    }
}
