using MS3_LMS.Enity.User;
using MS3_LMS.IRepository;
using MS3_LMS.IService.V1;
using System.Drawing.Printing;

namespace MS3_LMS.Service.V1
{
    public class RoleService:IRoleService
    {
        private readonly IRoleRepository _roleRepository;

        public RoleService(IRoleRepository roleRepository)
        {
            _roleRepository = roleRepository;
        }

        public async Task AssignDefaultRole(Guid userId)
        {
            var role = await _roleRepository.GetRoleByNameAsync("Member");
            if (role != null)
            {
                await _roleRepository.AssignRoleTouser(userId,role.RoleID);
            }
        }

        public async Task AssignAdmin(Guid AdminID)
        {
            var role = await _roleRepository.GetRoleByNameAsync("Admin");
            if (role != null)
            {
                await _roleRepository.AssignRoleTouser(AdminID,role.RoleID);
            }
        }

        public async Task<List<Role>> AdminDetails()
        {
            return await _roleRepository.GetAllAdmin();
        }


    }
}
