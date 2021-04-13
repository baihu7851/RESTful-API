using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using RESTfulAPI.Model.Models;
using RESTfulAPI.Repository.Interfaces;

namespace RESTfulAPI.Repository.Repositories
{
    public class UserMySqlRepository : IUserInterface
    {
        public UserMySqlRepository(IDbInterface db)
        {
            Connection = db.GetDb();
        }

        public IDbConnection Connection { get; }

        public void Add<T>(T user)
        {
            const string strSql = "INSERT INTO `Users` (UserName, Birthday, Email, Phone) VALUES (@UserName, @Birthday, @Email, @Phone)";
            Connection.ExecuteScalar<User>(strSql, user);
        }

        public void Delete<T>(T id)
        {
            const string strSql = "DELETE FROM `Users` WHERE (Id = @Id)";
            Connection.ExecuteScalar<User>(strSql, new { Id = id });
        }

        public void Update<T>(T user)
        {
            const string strSql = "UPDATE `Users` SET UserName = @UserName, Birthday = @Birthday, Email = @Email, Phone = @Phone WHERE (Id = @Id)";
            Connection.ExecuteScalar<User>(strSql, user);
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