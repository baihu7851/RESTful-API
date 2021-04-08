using System;
using System.Collections.Generic;
using System.Data;
using Dapper;
using RESTfulAPI.Middleware.ViewModel;
using RESTfulAPI.Repository.Interfaces;

namespace RESTfulAPI.Repository.Repositories
{
    public class RoleUserSqlServerRepository : IRoleUserInterface
    {
        public RoleUserSqlServerRepository(IDbInterface db)
        {
            Connection = db.GetDb();
        }

        public IDbConnection Connection { get; }

        public void Add<T>(int roleId, List<T> usersId)
        {
            const string strSql = "INSERT INTO [RoleUser] (RolesId, UsersId) VALUES  (@RolesId, @UsersId)";
            foreach (var userId in usersId)
            {
                Connection.ExecuteScalar<User>(strSql, new { RolesId = roleId, UsersId = userId });
            }
        }

        public void Delete<T>(List<T> roleId)
        {
            throw new NotImplementedException();
        }

        public void Update<T>(List<T> data)
        {
            throw new NotImplementedException();
        }

        public List<T> View<T>()
        {
            throw new NotImplementedException();
        }

        public T View<T>(int roleId)
        {
            throw new NotImplementedException();
        }
    }
}