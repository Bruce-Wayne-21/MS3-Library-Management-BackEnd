using MS3_LMS.Enity.Core;
using MS3_LMS.Models.RequestModel;
using MS3_LMS.Models.ResponeModel;

namespace MS3_LMS.IService.V1
{
    public interface IBookLendService
    {
        Task<BookLend>BookRequest(BookLendRequest bookLendRequest);

        Task<List<BookLendResponse>> GetBookLendResponsesAsync();
        Task<List<BookLendResponse>> EnumBasedResponse(BookLend.State state);
        Task<BookLend> UpdateRecord(Guid id, BookLend.State state);
       

        Task<bool> UpdateDates(Guid Memberid, DateTime date, string? dateType);
        Task SendNotification();
        Task<List<NotificationResponseModel>> sentNotification(Guid memberId);

        Task<List<BookLendResponse>> GetDetailsByMemberId(Guid memberId, BookLend.State state);
    }
}
