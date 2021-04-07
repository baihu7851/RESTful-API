using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using RESTfulAPI.Model.Models;
using RESTfulAPI.Repository.Interfaces;

namespace RESTfulAPI.Repository.Repositories
{
    public class UserSqlServerRepository : IUser
    {
        public UserSqlServerRepository(IConfiguration configuration)
        {
            Configuration = configuration;

            Connection = new SqlConnection(configuration.GetConnectionString("SqlServer"));
        }

        public IConfiguration Configuration { get; }

        public IDbConnection Connection { get; }

        public void Add(IEnumerable<User> user)
        {
            const string strSql = "INSERT INTO [Users] (UserName, Birthday, Email, Phone) VALUES (@UserName, @Birthday, @Email, @Phone)";
            foreach (var i in user)
            {
                Connection.ExecuteScalar<User>(strSql, i);
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

        public void Update(IEnumerable<User> user)
        {
            const string strSql = "UPDATE  [Users] SET UserName = @UserName, Birthday = @Birthday, Email = @Email, Phone = @Phone WHERE (Id = @Id)";
            foreach (var i in user)
            {
                Connection.ExecuteScalar<User>(strSql, i);
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