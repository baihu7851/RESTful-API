using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RESTfulAPI.Model.Models;

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