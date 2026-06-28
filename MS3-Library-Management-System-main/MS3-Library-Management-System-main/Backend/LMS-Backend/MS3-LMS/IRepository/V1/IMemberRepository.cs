using MS3_LMS.Enity.Core;
using MS3_LMS.Enity.User;

namespace MS3_LMS.IRepository.V1
{
    public interface IMemberRepository
    {
        Task CreateNewUser(Member member);
        Task<Member> GetMemberById(Guid Nic);
        Task<List<Member>> GetAllMembers();
        Task<List<BookLend>> GetMemberBorrowedBook(Guid id, BookLend.State state);
        Task<Member> UpdateMemberDetails(Member member);
        Task DeleteMemerByid(Member member);
        Task<bool> updatePAsswordAsync(Guid Userid, string HashPassword);
    }
}
