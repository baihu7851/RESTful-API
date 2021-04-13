using System.Collections.Generic;
using RESTfulAPI.Model.Models;

namespace RESTfulAPI.Repository.Interfaces
{
    public interface IRoleUserInterface
    {
        public List<RoleUser> GetUsers(int roleId);

        public List<RoleUser> GetRoles(int userId);

        public void Add(RoleUser roleUser);

        public void Update(RoleUser roleUser);

        public void Delete(RoleUser roleUser);
    }
}