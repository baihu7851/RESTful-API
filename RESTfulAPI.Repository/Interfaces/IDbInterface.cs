using System.Data;

namespace RESTfulAPI.Repository.Interfaces
{
    public interface IDbInterface
    {
        public IDbConnection GetDb();
    }
}