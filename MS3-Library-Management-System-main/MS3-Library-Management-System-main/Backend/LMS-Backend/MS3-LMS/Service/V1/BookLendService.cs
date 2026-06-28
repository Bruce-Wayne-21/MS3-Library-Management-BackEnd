using Microsoft.Build.Framework;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer.Query.Internal;
using MS3_LMS.Enity.Core;
using MS3_LMS.Enity.Notification;
using MS3_LMS.IRepository;
using MS3_LMS.IRepository.V1;
using MS3_LMS.IService.V1;
using MS3_LMS.LMSDbcontext;
using MS3_LMS.Models.RequestModel;
using MS3_LMS.Models.ResponeModel;

namespace MS3_LMS.Service.V1
{
    public class BookLendService : IBookLendService
    {
        private readonly IBookLendRepository _bookLendRepository;
        private readonly LMSContext _lMSContext;
        private readonly INotificationRepository _notificationRepository;
        private readonly IRoleRepository _roleRepository;

        public BookLendService(IBookLendRepository bookLendRepository ,LMSContext lMSContext,INotificationRepository notificationRepository,IRoleRepository roleRepository)
        {
            _bookLendRepository = bookLendRepository;
            _lMSContext = lMSContext;
            _notificationRepository = notificationRepository;
            _roleRepository = roleRepository;

        }

        public BookLendService()
        {
        }

        public async Task<BookLend> BookRequest(BookLendRequest bookLendRequest)
        {
            try
            {

                var borrowedBooks = await _bookLendRepository.getBorrowedBooks(bookLendRequest.MemebID);
                if (borrowedBooks >=2)
                {
                    throw new Exception("You can only borrow up to two books at a time.");
                }

                var hasBooks = await _bookLendRepository.HAsBorrowedBook(bookLendRequest.MemebID, bookLendRequest.Bookid);
                if (hasBooks)
                {
                    throw new Exception("You cannot borrow the same book  more than once.");
                }

                var request = new BookLend
                {
                    Status=bookLendRequest.State,
                    //LendDays = bookLendRequest.LendDays,
                    //ReturnDate = bookLendRequest.ReturnDate,
                    //ApprovedDate = bookLendRequest.ApprovedDate,
                    //CollectDate = bookLendRequest.CollectDate,
                    Bookid = bookLendRequest.Bookid,
                    MemebID = bookLendRequest.MemebID

                };
                var daa = await _bookLendRepository.RequestBook(request);
                return daa;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        

        public async Task<List<BookLendResponse>> GetBookLendResponsesAsync()
        {
            try
            {
                var data = await _bookLendRepository.GetAllRequestWithDeatails();
                var response =  data.Select(s => new BookLendResponse {
                    Status = s.Status,
                    LendDays = s.LendDays,
                    ReturnDate = s.ReturnDate,
                    ApprovedDate = s.ApprovedDate,
                    CollectDate = s.CollectDate,
                    Title = s.Book.Name,
                    MemberName = s.Member.FirstName,
                    Lastname = s.Member.FirstName,
                    Nic = s.Member.Nic,
                    Image1Path=s.Book.Image?.Image1Path,
                    Image2Path=s.Book.Image?.Image2Path


                }).ToList();
                return response;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<BookLendResponse>> EnumBasedResponse(BookLend.State state)
        {
            try
            {
                var data = await _bookLendRepository.GetEnumBaseRecords(state);
                var response = data.Select(data => new BookLendResponse
                {

                    LendId= data.LendId,
                    Status = data.Status,
                    LendDays = data.LendDays,
                    ReturnDate = data.ReturnDate,
                    ApprovedDate = data.ApprovedDate,
                    CollectDate = data.CollectDate,
                    Title = data.Book.Name,
                    MemberName = data.Member.FirstName,
                    MemberId=data.MemebID,
                    Lastname = data.Member.FirstName,
                    Nic = data.Member.Nic,
                    Image2Path=data.Book.Image?.Image2Path,
                    IsVerify=data.Member.IsVerify,
                    BookId=data.Bookid
                }).ToList();
                return response;
            }
            catch (Exception ex)
            {
                throw new Exception($"{ex.Message}");
            }
        }

        public async Task<BookLend>UpdateRecord(Guid id,BookLend.State state)
        {
            try
            {
                var member = await _lMSContext.BookLends.FindAsync(id) ?? throw new KeyNotFoundException("Member not found");
                member.Status = state;

                var res = await _bookLendRepository.UpdateState(member);
                return res;
            }
            catch(Exception ex) 
            {
                throw new Exception(ex.Message);
            }

        }

        //public async Task<bool> UpdatereturnDate(Guid memberID, DateTime returnDate)
        //{
        //    try
        //    {
        //        var data = await _bookLendRepository.UpdateReturnDate(memberID, returnDate);
        //        return data != null;
        //    }
        //    catch(Exception ex)
        //    {
        //        throw new Exception(ex.Message);
        //    }
        //}


       

        public async Task<bool>UpdateDates(Guid Memberid,DateTime date,string? dateType)
        {
            try
            {
                var data = await _bookLendRepository.updatedate(Memberid, date, dateType);
                return data !=null;

            }
            catch(Exception ex)
            {
                throw new Exception();
            }
        }


        public async Task SendNotification()
        {
            try
            {
                var message = await _bookLendRepository.GetDueDateBooks();
                
                

                foreach(var booklend in message)
                {
                    var memberNotification = new Notification
                    {
                        NotificationId = Guid.NewGuid(),
                        Type = "over Due reminder",
                        Title = "Book Return Reminder",
                        Body = $"your book `{booklend.Book.Name}` Is due  for return tommorrow.",
                        ViewStatus = false,
                        ReceiveId = booklend.MemebID
                    };
                    await _notificationRepository.AddNotification(memberNotification);



                    //var AdminNotification=new Notification
                    //{
                    //    NotificationId=Guid.NewGuid(),
                    //    Type="OverDue Reminder",
                    //    Title="Over Due Alert",
                    //    Body=$"Book `{booklend.Book.Name}` Borrowed By `{booklend.Member.FirstName} is  6 Days OverDue`",
                    //    ViewStatus = false,
                    //    ReceiveId=
                        
                    //}

                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        public async Task<List<NotificationResponseModel>> sentNotification(Guid memberId)
        {
            try
            {
                var data = await _notificationRepository.GetNotificationAsync(memberId);
                var response = data.Select(data => new NotificationResponseModel
                {
                    Type = data.Type,
                    Title = data.Title,
                    Body = data.Body,

                }).ToList();
                return response;



            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        public async Task<List<BookLendResponse>> GetDetailsByMemberId(Guid memberId, BookLend.State state)
        {
            try
            {
                var data = await _bookLendRepository.GetDetailsByMembe(memberId, state);

                var response = data
                    .Select(s => new BookLendResponse
                    {
                        LendId = s.LendId,
                        MemberId=s.MemebID,
                        ApprovedDate = s.ApprovedDate,
                        CollectDate = s.CollectDate,
                        ReturnDate = s.ReturnDate,
                        Title = s.Book?.Name ?? "N/A", 
                        BookId=s.Bookid,
                        Image2Path = s.Book?.Image?.Image2Path ?? "default-image-path.jpg" 
                    })
                    .ToList();

                return response;
            }
            catch (Exception ex)
            {
                
                Console.WriteLine($"Error occurred: {ex.Message}");
                throw;
            }
        }



    }
}
