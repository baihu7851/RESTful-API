using System.Collections.Generic;
using RESTfulAPI.ViewModel;

namespace RESTfulAPI.Middleware.Interfaces
{
    public interface IRole
    {
        public List<ViewRole> GetRoles();

        public ViewRole GetRole(int id);

        public void AddRole(List<ViewRole> roles);

        public void UpdateRole(List<ViewRole> roles);

        public void DeleteRole(List<int> id);
    }
}