using MS3_LMS.Enity.User;

namespace MS3_LMS.IRepository.V1
{
    public interface IUserRepository
    {
        Task createMemeber(Member member);
        Task<User> createUser(User user);
        Task CreteAdmin(User user);
        Task<User> LoginAsync(string email, string password);
        Task<Member> GetMemberByUSerId(Guid UserId);
    }
}
