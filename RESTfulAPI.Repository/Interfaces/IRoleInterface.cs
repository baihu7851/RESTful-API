using System.Collections.Generic;

namespace RESTfulAPI.Repository.Interfaces
{
    public interface IRoleInterface
    {
        public List<T> View<T>();

        public T View<T>(int id);

        public int Add<T>(T data);

        public void Update<T>(T data);

        public void Delete<T>(T id);
    }
}