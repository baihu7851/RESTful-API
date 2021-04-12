using System.Collections.Generic;

namespace RESTfulAPI.Repository.Interfaces
{
    public interface IRoleUserInterface
    {
        List<T> View<T>();

        T View<T>(int id);

        void Add<T>(int id, List<T> data);

        void Update<T>(int id, List<T> data);

        void Delete<T>(int id, List<T> data);
    }
}