using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using Microsoft.Extensions.Configuration;
using RESTfulAPI.Middleware.ViewModel;
using RESTfulAPI.Repository.Interfaces;

namespace RESTfulAPI.Repository.Repositories
{
    public class RoleSqlServerRepository : IRoleInterface
    {
        public RoleSqlServerRepository(IConfiguration configuration, IDbInterface db)
        {
            Configuration = configuration;
            Connection = db.GetDb();
        }

        public IConfiguration Configuration { get; }
        public IDbConnection Connection { get; }

        public void Add<T>(List<T> roles)
        {
            const string strSql = "INSERT INTO [Roles] (RoleName) VALUES (@RoleName)";
            foreach (var role in roles)
            {
                Connection.ExecuteScalar<Role>(strSql, role);
            }
        }

        public void Delete<T>(List<T> id)
        {
            const string strSql = "DELETE FROM [Roles] WHERE (Id = @Id)";
            foreach (var i in id)
            {
                Connection.ExecuteScalar<Role>(strSql, new { Id = i });
            }
        }

        public void Update<T>(List<T> roles)
        {
            const string strSql = "UPDATE [Roles] SET RoleName = @RoleName WHERE (Id = @Id)";
            foreach (var role in roles)
            {
                Connection.ExecuteScalar<Role>(strSql, role);
            }
        }

        public T View<T>(int id)
        {
            const string strSql = "SELECT * FROM [Roles] WHERE (Roles.Id = @Id)";
            return Connection.QueryFirstOrDefault<T>(strSql, new
            {
                Id = id
            });
        }

        public List<T> View<T>()
        {
            const string strSql = "SELECT * FROM [Roles]";
            return Connection.Query<T>(strSql).ToList();
        }
    }
}