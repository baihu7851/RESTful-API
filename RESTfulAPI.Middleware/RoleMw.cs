using System.Collections.Generic;
using System.Linq;
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

        public List<ViewRole> GetRoles()
        {
            const string key = "Roles";
            if (Cache.GetCache(key) == null)
            {
                List<ViewRole> roles = _role.View<Role>().Select(
                    role => new ViewRole
                    {
                        Id = role.Id,
                        RoleName = role.RoleName,
                    }).ToList();
                Cache.SetCache(key, roles);
            }
            List<ViewRole> result = (List<ViewRole>)Cache.GetCache(key);
            return result;
        }

        public ViewRole GetRole(int id)
        {
            string key = $"Role{id}";
            if (Cache.GetCache(key) == null)
            {
                ViewRole viewRole = new ViewRole();
                Role role = _role.View<Role>(id);
                if (role == null)
                {
                    return null;
                }
                viewRole.Id = role.Id;
                viewRole.RoleName = role.RoleName;
                Cache.SetCache(key, viewRole);
            }
            ViewRole result = (ViewRole)Cache.GetCache(key);
            return result;
        }

        public List<ViewRole> AddRole(List<ViewRole> roles)
        {
            List<int> listId = new();
            foreach (var role in roles)
            {
                if (VerifyRole(role) != null)
                {
                    var id = _role.Add(role);
                    listId.Add(id);
                    string key = $"Role{id}";
                    Cache.RemoveCache(key);
                }
            }
            List<ViewRole> result = listId.Select(GetRole).ToList();
            return result;
        }

        public List<ViewRole> UpdateRole(List<ViewRole> roles)
        {
            List<int> listId = new();
            foreach (var role in roles)
            {
                if (VerifyRole(role) != null)
                {
                    _role.Update(role);
                    listId.Add(role.Id);
                }
                string key = $"Role{role.Id}";
                Cache.SetCache(key, role);
            }
            List<ViewRole> result = listId.Select(GetRole).ToList();
            return result;
        }

        public ViewRole DeleteRole(int id)
        {
            var role = GetRole(id);
            if (role == null)
            {
                return null;
            }
            _role.Delete(id);
            string key = $"Role{id}";
            Cache.RemoveCache(key);
            return role;
        }

        private object VerifyRole(ViewRole role)
        {
            return string.IsNullOrWhiteSpace(role.RoleName) ? null : role;
        }
    }
}