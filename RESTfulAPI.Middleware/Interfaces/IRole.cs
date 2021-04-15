using System.Collections.Generic;
using RESTfulAPI.ViewModel;

namespace RESTfulAPI.Middleware.Interfaces
{
    public interface IRole
    {
        public List<ViewRole> GetRoles();

        public ViewRole GetRole(int id);

        public List<ViewRole> AddRole(List<ViewRole> roles);

        public List<ViewRole> UpdateRole(List<ViewRole> roles);

        public ViewRole DeleteRole(int id);
    }
}