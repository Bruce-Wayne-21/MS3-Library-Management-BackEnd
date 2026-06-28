using Microsoft.CodeAnalysis.CSharp;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.Internal;
using Microsoft.Identity.Client;
using MS3_LMS.Enity.Book;
using MS3_LMS.Enity.Core;
using MS3_LMS.IRepository;
using MS3_LMS.LMSDbcontext;
using MS3_LMS.Models.RequestModel;
using NuGet.Protocol.Plugins;
using System.Net;

namespace MS3_LMS.Repository.V1
{
    public class BookLendRepository: IBookLendRepository
    {
        private readonly LMSContext _DbContext;
        private readonly ILogger<BookLendRepository> _Logger;   

        public BookLendRepository(LMSContext dbContext, ILogger<BookLendRepository> logger)
        {
            _DbContext = dbContext;
            _Logger = logger;
        }

        public Task<bool> HAsBorrowedBook(Guid memberid, Guid bookid)
        {
            try
            {
                return _DbContext.BookLends
              .AnyAsync(bl => bl.MemebID == memberid && bl.Bookid == bookid && bl.Status == BookLend.State.Waiting);
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public Task<int> getBorrowedBooks(Guid MemberID)
        {
            try
            {
                return _DbContext.BookLends
              .CountAsync(b => b.MemebID == MemberID && b.Status == BookLend.State.Waiting);
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<BookLend>RequestBook(BookLend bookLend)
        {
            try
            {
                 await _DbContext.BookLends.AddAsync(bookLend);
                await _DbContext.SaveChangesAsync();
                return bookLend;
            }
            catch (Exception ex)
            {
                _Logger.LogError(ex.Message);
                throw new Exception(ex.Message);

            }
        }

        public async Task<List<BookLend>>GetAllRequestWithDeatails()
        {
            var data=await _DbContext.BookLends.Include(b=>b.Book).Include(b=>b.Book.Image).Include(M=>M.Member).ToListAsync();
            return data;
        }


        public async Task<List<BookLend>> GetEnumBaseRecords(BookLend.State state)
        {
            try
            {
                var data = await _DbContext.BookLends.Where(s => s.Status == state).Include(d => d.Book)
                    .Include(m => m.Member).Include(i=>i.Book.Image).ToListAsync();
                return data;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        

        public async Task<BookLend>UpdateState(BookLend bookLend)
        {
            try
            {
                 _DbContext.BookLends.Update(bookLend);
               await _DbContext.SaveChangesAsync();
                return bookLend;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


      
        public async Task<BookLend>updatedate(Guid Id,DateTime date,string datetype)
        {
            try
            {
                var data = await _DbContext.BookLends.FindAsync(Id);
                if(data == null)
                {
                    return null;
                }

                switch (datetype.ToLower())
                {
                    case "return":
                        data.ReturnDate = date;
                        break;

                    case "approve":
                        data.ApprovedDate = date;
                        break;

                    case "collect":
                        data.CollectDate= date;
                        break;

                    default:
                        throw new ArgumentException("invalid  date type ");

                };
                _DbContext.BookLends.Update(data);
                await _DbContext.SaveChangesAsync();
                return data;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<BookLend>> OverDueAsync()
        {
            try
            {
                var data = await _DbContext.BookLends.Where(O => O.CollectDate.HasValue && O.CollectDate < DateTime.Now)
                    .Include(I => I.Book)
                    .Include(b => b.Member)
                    .ToListAsync();
                return data;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<BookLend>> GetDueDateBooks()
        {
            try
            {
                var today = DateTime.Now;

                var data = await _DbContext.BookLends.Where(b => b.CollectDate.HasValue && EF.Functions.DateDiffDay(b.CollectDate, today) > 6)
                    .Include(I => I.Book)
                    .Include(b => b.Member)
                    .ToListAsync();
                return data;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        public async Task<List<BookLend>> GetDetailsByMembe(Guid memberId, BookLend.State state)
        {
            try
            {
                var data = await _DbContext.BookLends
                    .Include(m=>m.Member)
                    .Include(b => b.Book)
                    .ThenInclude(i => i.Image) 
                    
                    .Where(b => b.MemebID == memberId && b.Status == state)
                    .ToListAsync();
                return data;
            }
            catch (Exception)
            {
                throw; 
            }
        }







    }
}
