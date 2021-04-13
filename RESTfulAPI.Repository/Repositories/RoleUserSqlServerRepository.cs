using System.Collections.Generic;
using System.Data;
using Dapper;
using RESTfulAPI.Model.Models;
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

        public void Add<T>(int roleId, T userId)
        {
            const string strSql = "INSERT INTO [RoleUser] (RolesId, UsersId) VALUES  (@RolesId, @UsersId)";
            Connection.ExecuteScalar<RoleUser>(strSql, new { RolesId = roleId, UsersId = userId });
        }

        public void Delete<T>(int roleId, T userId)
        {
            const string strSql = "DELETE FROM [RoleUser] WHERE (UsersId = @UsersId) AND (RolesId = @RolesId)";
            Connection.ExecuteScalar<RoleUser>(strSql, new { RolesId = roleId, UsersId = userId });
        }

        public void Update<T>(int roleId, T userId)
        {
            const string strSql = "UPDATE [RoleUser] SET UsersId = @UsersId WHERE (RolesId = @RolesId)";
            Connection.ExecuteScalar<RoleUser>(strSql, new { RolesId = roleId, UsersId = userId });
        }

        public List<T> View<T>()
        {
            const string strSql = @"
                SELECT DISTINCT RolesId, UsersId
                FROM [RoleUser]";
            return (List<T>)Connection.Query<T>(strSql);
        }

        public T View<T>(int id)
        {
            const string strSql = @"
                SELECT DISTINCT RolesId, UsersId
                FROM [RoleUser]
                WHERE (RolesId = @RolesId)";
            return (T)Connection.Query<RoleUser>(strSql, new { RolesId = id });
        }
    }
}