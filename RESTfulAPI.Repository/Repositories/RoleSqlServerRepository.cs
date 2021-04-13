using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using RESTfulAPI.Model.Models;
using RESTfulAPI.Repository.Interfaces;

namespace RESTfulAPI.Repository.Repositories
{
    public class RoleSqlServerRepository : IRoleInterface
    {
        public RoleSqlServerRepository(IDbInterface db)
        {
            Connection = db.GetDb();
        }

        public IDbConnection Connection { get; }

        public void Add<T>(T role)
        {
            const string strSql = "INSERT INTO [Roles] (RoleName) VALUES (@RoleName)";
            Connection.ExecuteScalar<Role>(strSql, role);
        }

        public void Delete<T>(T id)
        {
            const string strSql = "DELETE FROM [Roles] WHERE (Id = @Id)";
            Connection.ExecuteScalar<Role>(strSql, new { Id = id });
        }

        public void Update<T>(T role)
        {
            const string strSql = "UPDATE [Roles] SET RoleName = @RoleName WHERE (Id = @Id)";
            Connection.ExecuteScalar<Role>(strSql, role);
        }

        public T View<T>(int id)
        {
            const string strSql = "SELECT * FROM [Roles] WHERE (Roles.Id = @Id)";
            return Connection.QueryFirstOrDefault<T>(strSql, new { Id = id });
        }

        public List<T> View<T>()
        {
            const string strSql = "SELECT * FROM [Roles]";
            return Connection.Query<T>(strSql).ToList();
        }
    }
}