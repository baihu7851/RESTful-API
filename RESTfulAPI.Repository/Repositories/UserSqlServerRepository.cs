using System.Collections.Generic;
using System.Data;
using System.Security.Policy;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.Extensions.Configuration;
using RESTfulAPI.Model.ViewModel;
using RESTfulAPI.Repository.Interfaces;

namespace RESTfulAPI.Repository.Repositories
{
    public class UserSqlServerRepository : IUser
    {
        public UserSqlServerRepository(IConfiguration configuration, IDb db)
        {
            Configuration = configuration;
            Connection = db.GetDb();
        }

        public IConfiguration Configuration { get; }
        public IDbConnection Connection { get; }

        public void Add(IEnumerable<User> users)
        {
            const string strSql = "INSERT INTO [Users] (UserName, Birthday, Email, Phone) VALUES (@UserName, @Birthday, @Email, @Phone)";
            foreach (var user in users)
            {
                Connection.ExecuteScalar<User>(strSql, user);
            }
        }

        public void Delete(IEnumerable<int> id)
        {
            const string strSql = "DELETE FROM [Users] WHERE (Id = @Id)";
            foreach (var i in id)
            {
                Connection.ExecuteScalar<User>(strSql, new { Id = i });
            }
        }

        public void Update(IEnumerable<User> users)
        {
            const string strSql = "UPDATE  [Users] SET UserName = @UserName, Birthday = @Birthday, Email = @Email, Phone = @Phone WHERE (Id = @Id)";
            foreach (var user in users)
            {
                Connection.ExecuteScalar<User>(strSql, user);
            }
        }

        public User User(int id)
        {
            const string strSql = "SELECT * FROM [Users] WHERE (Users.Id = @Id)";
            return Connection.QueryFirst<User>(strSql, new
            {
                Id = id
            });
        }

        public IEnumerable<User> Users()
        {
            const string strSql = "SELECT * FROM [Users]";
            return Connection.Query<User>(strSql);
        }
    }
}