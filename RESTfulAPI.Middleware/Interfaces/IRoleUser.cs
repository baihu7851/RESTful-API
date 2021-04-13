using System.Collections.Generic;

namespace RESTfulAPI.Middleware.Interfaces
{
    public interface IRoleUser
    {
        public List<int> GetRoleUser(int roleId);

        public void AddRoleUser(int roleId, List<int> usersId);

        public void DeleteRoleUser(int roleId, List<int> usersId);

        public List<int> GetUserRole(int userId);

        public void AddUserRole(int userId, List<int> rolesId);

        public void DeleteUserRole(int userId, List<int> rolesId);
    }
}