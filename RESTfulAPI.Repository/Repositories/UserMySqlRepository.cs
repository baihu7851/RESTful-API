using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using Microsoft.Extensions.Configuration;
using RESTfulAPI.Middleware.ViewModel;
using RESTfulAPI.Repository.Interfaces;

namespace RESTfulAPI.Repository.Repositories
{
    public class UserMySqlRepository : IUserInterface
    {
        public UserMySqlRepository(IConfiguration configuration, IDbInterface db)
        {
            Configuration = configuration;
            Connection = db.GetDb();
        }

        public IConfiguration Configuration { get; }
        public IDbConnection Connection { get; }

        public void Add<T>(List<T> users)
        {
            const string strSql = "INSERT INTO `Users` (UserName, Birthday, Email, Phone) VALUES (@UserName, @Birthday, @Email, @Phone)";
            foreach (var user in users)
            {
                Connection.ExecuteScalar<User>(strSql, user);
            }
        }

        public void Delete<T>(List<T> id)
        {
            const string strSql = "DELETE FROM `Users` WHERE (Id = @Id)";
            foreach (var i in id)
            {
                Connection.ExecuteScalar<User>(strSql, new { Id = i });
            }
        }

        public void Update<T>(List<T> users)
        {
            const string strSql = "UPDATE `Users` SET UserName = @UserName, Birthday = @Birthday, Email = @Email, Phone = @Phone WHERE (Id = @Id)";
            foreach (var user in users)
            {
                Connection.ExecuteScalar<User>(strSql, user);
            }
        }

        public T View<T>(int id)
        {
            const string strSql = "SELECT * FROM `Users` WHERE (Users.Id = @Id)";
            return Connection.QueryFirstOrDefault<T>(strSql, new
            {
                Id = id
            });
        }

        public List<T> View<T>()
        {
            const string strSql = "SELECT * FROM `Users`";
            return Connection.Query<T>(strSql).ToList();
        }
    }
}