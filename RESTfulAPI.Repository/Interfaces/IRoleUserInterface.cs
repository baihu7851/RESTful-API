using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RESTfulAPI.Middleware.ViewModel;

namespace RESTfulAPI.Repository.Interfaces
{
    public interface IRoleUserInterface
    {
        List<RoleUser> View();

        RoleUser View(int roleId);

        void Add<T>(int roleId, List<T> userId);

        void Update<T>(int roleId, List<T> usersId);

        void Delete<T>(int roleId, List<T> usersId);
    }
}