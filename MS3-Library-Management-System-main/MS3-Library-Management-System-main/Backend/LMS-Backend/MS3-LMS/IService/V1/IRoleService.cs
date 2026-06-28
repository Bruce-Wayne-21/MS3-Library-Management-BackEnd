namespace MS3_LMS.IService.V1
{
    public interface IRoleService
    {
        Task AssignDefaultRole(Guid userId);
        Task AssignAdmin(Guid AdminID);
    }
}
