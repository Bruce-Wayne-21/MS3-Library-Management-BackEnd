using Microsoft.AspNetCore.Http.HttpResults;
using MS3_LMS.Enity.Core;
using MS3_LMS.IRepository.V1;
using MS3_LMS.IService.V1;
using MS3_LMS.LMSDbcontext;
using MS3_LMS.Models.RequestModel;

namespace MS3_LMS.Service.V1
{
    public class SubcriptionService:ISubcriptionService
    {
        private readonly ISubcriptionRepository _subcriptionRepository;
        private readonly IPaymentRepository _PaymentRepository;

        public SubcriptionService(ISubcriptionRepository subcriptionRepository,IPaymentRepository payment)

        {
            _subcriptionRepository = subcriptionRepository;
            _PaymentRepository=payment;
        }


        public async Task <Subscription> NewSubcription(SubcriptionRequest subcriptionRequest)
        {
            try
            {
                var request = new Subscription
                {
                    SubId= Guid.NewGuid(),
                    SubType = subcriptionRequest.SubType,
                    IsActive = subcriptionRequest.IsActive,
                    MemebID=subcriptionRequest.MemebID
                };
                var payment = new Payment
                {
                    PaymentId= Guid.NewGuid(),
                    Type=subcriptionRequest.PaymentType,
                    Amount=subcriptionRequest.PaymentAmount,
                    SubId=request.SubId
                };
                 await _PaymentRepository.CreateNewPayment(payment);
                return await _subcriptionRepository.NewSubcription(request);
            }
            catch (Exception ex)
            {
                throw new Exception();
            }
        }


        public async Task<bool> CheckIsActive(Guid memberId)
        {
            try
            {
                var subscription = await _subcriptionRepository.CheckSubIsActive(memberId);

               
                if (subscription == null || !subscription.IsActive)
                {
                    return false;
                }

                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("Error checking subscription activity.", ex);
            }
        }


        public async Task<List<Subscription>> GetSubCription()
        {
            try
            {
                var data = await _subcriptionRepository.GetAllSubcription();
                return data;
            }
            catch (Exception ex)
            {
                throw new Exception();
            }
        }


    }
}
