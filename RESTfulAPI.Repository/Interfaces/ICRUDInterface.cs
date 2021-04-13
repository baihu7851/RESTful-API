using System.Collections.Generic;

namespace RESTfulAPI.Repository.Interfaces
{
    public interface ICRUDInterface
    {
        public List<T> View<T>();

        public T View<T>(int id);

        public void Add<T>(T data);

        public void Update<T>(T data);

        public void Delete<T>(T id);
    }
}