﻿namespace MS3_LMS.IService
{
    public interface IRoleService
    {
        Task AssignDefaultRole(Guid userId);
        Task AssignAdmin(Guid AdminID);
    }
}
