using System.Collections.Generic;

namespace RESTfulAPI.Middleware.Interfaces
{
    public interface IUser
    {
        public List<ViewModel.User> GetUsers();
        public ViewModel.User GetUser(int id);
        //public List<ViewModel.User> PostUser(List<ViewModel.User> user);
    }
}
