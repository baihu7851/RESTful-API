using System.Collections.Generic;
using RESTfulAPI.Middleware.Interfaces;
using RESTfulAPI.Repository.Interfaces;

namespace RESTfulAPI.Middleware
{
    public class RoleUserMw : IRoleUser
    {
        private readonly IRoleUserInterface _roleUser;

        public RoleUserMw(IRoleUserInterface roleUser)
        {
            _roleUser = roleUser;
        }

        public void AddRoleUser(int roleId, List<int> usersId)
        {
            foreach (var userId in usersId)
            {
                Model.Models.RoleUser result = new Model.Models.RoleUser
                {
                    RolesId = roleId,
                    UsersId = userId
                };
                _roleUser.Add(result);
            }
        }

        public void DeleteRoleUser(int roleId, List<int> usersId)
        {
            foreach (var userId in usersId)
            {
                Model.Models.RoleUser result = new Model.Models.RoleUser
                {
                    RolesId = roleId,
                    UsersId = userId
                };
                _roleUser.Delete(result);
            }
        }

        public List<int> GetRoleUser(int roleId)
        {
            var roleUsers = _roleUser.GetUsers(roleId);
            List<int> userId = new();
            foreach (var roleUser in roleUsers)
            {
                userId.Add(roleUser.UsersId);
            }
            return userId;
        }

        public void AddUserRole(int userId, List<int> rolesId)
        {
            foreach (var roleId in rolesId)
            {
                Model.Models.RoleUser result = new Model.Models.RoleUser
                {
                    RolesId = roleId,
                    UsersId = userId
                };
                _roleUser.Add(result);
            }
        }

        public void DeleteUserRole(int userId, List<int> rolesId)
        {
            foreach (var roleId in rolesId)
            {
                Model.Models.RoleUser result = new Model.Models.RoleUser
                {
                    RolesId = roleId,
                    UsersId = userId
                };
                _roleUser.Delete(result);
            }
        }

        public List<int> GetUserRole(int userId)
        {
            var userRoles = _roleUser.GetRoles(userId);
            List<int> roleId = new();
            foreach (var userRole in userRoles)
            {
                roleId.Add(userRole.RolesId);
            }
            return roleId;
        }
    }
}