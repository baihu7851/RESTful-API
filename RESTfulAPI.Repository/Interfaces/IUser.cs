using RESTfulAPI.Middleware.ViewModel;
using System.Collections.Generic;

namespace RESTfulAPI.Repository.Interfaces
{
    public interface IUser
    {
        IEnumerable<User> Users();

        User User(int id);

        void Add(IEnumerable<User> user);

        void Update(IEnumerable<User> user);

        void Delete(IEnumerable<int> id);
    }
}