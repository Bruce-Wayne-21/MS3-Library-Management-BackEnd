using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer.Query.Internal;
using Microsoft.Identity.Client;
using MS3_LMS.Enity.User;
using MS3_LMS.IRepository;
using MS3_LMS.LMSDbcontext;

namespace MS3_LMS.Repository.V1
{
    public class RoleRepository:IRoleRepository
    {
        private readonly LMSContext _lMSContext;

        public RoleRepository(LMSContext lMSContext)
        {
            _lMSContext = lMSContext;
        }

        public async Task<Role>GetRoleByNameAsync(string Usertype)
        {
            var role=await _lMSContext.Roles.FirstAsync(r=>r.UserAType==Usertype);
            return role;

        }

        public async Task AssignRoleTouser(Guid uerid,Guid roleId)
        {
            var userrole = new UserRole
            {
                UserId = uerid,
                RoleID = roleId
            };

            _lMSContext.UserRoles.AddAsync(userrole);
            await _lMSContext.SaveChangesAsync();

        }

        public async Task<List<Role>> GetAllMembers()
        {
            try
            {
                var role = await _lMSContext.Roles.Where(r => r.UserAType == "Member").ToListAsync();
                return role;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<Role>> GetAllAdmin()
        {
            try
            {
                var details = await _lMSContext.Roles.Where(a => a.UserAType == "Admin").ToListAsync();
                return details;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }


}

    

