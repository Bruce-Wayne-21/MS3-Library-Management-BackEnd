using MS3_LMS.Enity.Core;

namespace MS3_LMS.IRepository.V1
{
    public interface IPaymentRepository
    {
        Task<Payment> CreateNewPayment(Payment payment);
    }
}
