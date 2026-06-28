using MS3_LMS.Enity.Core;

namespace MS3_LMS.IRepository.V1
{
    public interface ISubcriptionRepository
    {
        Task<Subscription> NewSubcription(Subscription subscription);
        Task<Subscription> CheckSubIsActive(Guid memberid);
         Task<List<Subscription>> GetAllSubcription();
    }
}
