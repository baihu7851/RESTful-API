using System.Collections.Generic;
using RESTfulAPI.Middleware.Interfaces;
using RESTfulAPI.Model.Models;
using RESTfulAPI.Repository.Interfaces;
using RESTfulAPI.ViewModel;

namespace RESTfulAPI.Middleware
{
    public class RoleMw : IRole
    {
        private readonly IRoleInterface _role;

        public RoleMw(IRoleInterface role)
        {
            _role = role;
        }

        public void AddRole(List<ViewRole> roles)
        {
            foreach (var role in roles)
            {
                _role.Add(role);
            }
        }

        public void DeleteRole(List<int> id)
        {
            foreach (var i in id)
            {
                _role.Delete(i);
            }
        }

        public ViewRole GetRole(int id)
        {
            Role roleData = _role.View<Role>(id);
            ViewRole role = new ViewRole()
            {
                Id = roleData.Id,
                RoleName = roleData.RoleName,
            };
            return role;
        }

        public List<ViewRole> GetRoles()
        {
            List<ViewRole> roles = new();
            foreach (var role in _role.View<Role>())
            {
                var viewRole = new ViewRole
                {
                    Id = role.Id,
                    RoleName = role.RoleName,
                };
                roles.Add(viewRole);
            }
            return roles;
        }

        public void UpdateRole(List<ViewRole> roles)
        {
            foreach (var role in roles)
            {
                _role.Update(role);
            }
        }
    }
}