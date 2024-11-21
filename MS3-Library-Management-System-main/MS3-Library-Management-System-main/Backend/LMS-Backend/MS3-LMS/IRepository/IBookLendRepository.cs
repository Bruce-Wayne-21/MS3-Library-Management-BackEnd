﻿using MS3_LMS.Enity.Core;

namespace MS3_LMS.IRepository
{
    public interface IBookLendRepository
    {
        Task<BookLend> RequestBook(BookLend bookLend);
        Task<List<BookLend>> GetAllRequestWithDeatails();
        Task<List<BookLend>> GetEnumBaseRecords(BookLend.State state);
        Task<BookLend> UpdateState(BookLend bookLend);
        
        Task<BookLend> updatedate(Guid Id, DateTime date, string datetype);
        Task<List<BookLend>> GetDueDateBooks();


    }
}
