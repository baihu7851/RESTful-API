using System.Collections.Generic;
using System.Linq;
using RESTfulAPI.Middleware.Interfaces;
using RESTfulAPI.Repository.Interfaces;
using RESTfulAPI.ViewModel;

namespace RESTfulAPI.Middleware
{
    public class RoleUserMw : IRoleUser
    {
        private readonly IRoleUserInterface _roleUser;

        public RoleUserMw(IRoleUserInterface roleUser)
        {
            _roleUser = roleUser;
        }

        public List<int> AddRoleUser(List<ViewRoleUser> viewRoleUsers)
        {
            foreach (var viewRole in viewRoleUsers)
            {
                Model.Models.RoleUser result = new Model.Models.RoleUser
                {
                    RolesId = viewRole.RoleId,
                    UsersId = viewRole.UserId
                };
                _roleUser.Add(result);
            }
            return viewRoleUsers.Select(i => i.UserId).ToList();
        }

        public List<int> DeleteRoleUser(List<ViewRoleUser> viewRoleUsers)
        {
            foreach (var viewRoleUser in viewRoleUsers)
            {
                Model.Models.RoleUser result = new Model.Models.RoleUser
                {
                    RolesId = viewRoleUser.RoleId,
                    UsersId = viewRoleUser.UserId
                };
                _roleUser.Delete(result);
            }
            return viewRoleUsers.Select(i => i.UserId).ToList();
        }
    }
}