using System.Collections.Generic;
using RESTfulAPI.ViewModel;

namespace RESTfulAPI.Middleware.Interfaces
{
    public interface IUser
    {
        public List<ViewUser> GetUsers();

        public ViewUser GetUser(int id);

        public void AddUser(List<ViewUser> users);

        public void UpdateUser(List<ViewUser> users);

        public void DeleteUser(List<int> id);
    }
}