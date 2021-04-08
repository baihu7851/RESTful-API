using RESTfulAPI.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RESTfulAPI.Repository.Interfaces
{
    public interface IRole
    {
        IEnumerable<Role> Roles();

        Role Role(int id);

        void Add(Role role);

        void Update(Role role);

        void Delete(int id);
    }
}