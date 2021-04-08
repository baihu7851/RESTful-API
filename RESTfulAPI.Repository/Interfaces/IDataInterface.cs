using System.Collections.Generic;

namespace RESTfulAPI.Repository.Interfaces
{
    public interface IDataInterface
    {
        List<T> View<T>();

        T View<T>(int id);

        void Add<T>(List<T> data);

        void Update<T>(List<T> data);

        void Delete<T>(List<T> id);
    }
}