using MS3_LMS.Enity.Core;
using MS3_LMS.Models.RequestModel;

namespace MS3_LMS.IService.V1
{
    public interface ISubcriptionService
    {
        Task<Subscription> NewSubcription(SubcriptionRequest subcriptionRequest);
        Task<bool> CheckIsActive(Guid memberId);
        Task<List<Subscription>> GetSubCription();
    }
}
