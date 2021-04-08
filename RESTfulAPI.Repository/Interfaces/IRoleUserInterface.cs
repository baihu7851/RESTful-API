using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RESTfulAPI.Repository.Interfaces
{
    public interface IRoleUserInterface
    {
        List<T> View<T>();

        T View<T>(int roleId);

        void Add<T>(int roleId, List<T> userId);

        void Update<T>(List<T> data);

        void Delete<T>(List<T> roleId);
    }
}