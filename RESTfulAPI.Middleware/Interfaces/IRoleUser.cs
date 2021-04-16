using System.Collections.Generic;
using RESTfulAPI.ViewModel;

namespace RESTfulAPI.Middleware.Interfaces
{
    public interface IRoleUser
    {
        public List<int> AddRoleUser(List<ViewRoleUser> viewRoleUsers);

        public List<int> DeleteRoleUser(List<ViewRoleUser> viewRoleUsers);
    }
}