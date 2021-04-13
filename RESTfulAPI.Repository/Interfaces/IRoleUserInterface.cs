using System.Collections.Generic;

namespace RESTfulAPI.Repository.Interfaces
{
    public interface IRoleUserInterface
    {
        public List<T> View<T>();

        public T View<T>(int id);

        public void Add<T>(int id, T data);

        public void Update<T>(int id, T data);

        public void Delete<T>(int id, T data);
    }
}