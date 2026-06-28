using MS3_LMS.Enity.User;

namespace MS3_LMS.IRepository
{
    public interface IRoleRepository
    {
        Task AssignRoleTouser(Guid uerid, Guid roleId);
        Task<Role> GetRoleByNameAsync(string Usertype);
        Task<List<Role>> GetAllAdmin();
    }
}
