using System.Collections.Generic;
using RESTfulAPI.ViewModel;

namespace RESTfulAPI.Middleware.Interfaces
{
    public interface IUser
    {
        public List<ViewUser> GetUsers();

        public ViewUser GetUser(int id);

        public List<ViewUser> AddUser(List<ViewUser> users);

        public List<ViewUser> UpdateUser(List<ViewUser> users);

        public ViewUser DeleteUser(int id);
    }
}