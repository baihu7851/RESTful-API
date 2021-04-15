using System.Collections.Generic;
using System.Data;
using System.Linq;
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

        public int Add(RoleUser roleUser)
        {
            const string strSql = "INSERT INTO [RoleUser] (RolesId, UsersId) VALUES (@RolesId, @UsersId) select scope_identity()";
            return Connection.ExecuteScalar<int>(strSql, roleUser);
        }

        public void Delete(RoleUser roleUser)
        {
            const string strSql = "DELETE FROM [RoleUser] WHERE (UsersId = @UsersId) AND (RolesId = @RolesId)";
            Connection.ExecuteScalar<RoleUser>(strSql, roleUser);
        }

        public void Update(RoleUser roleUser)
        {
            const string strSql = "UPDATE [RoleUser] SET UsersId = @UsersId WHERE (UsersId = @UsersId) AND (RolesId = @RolesId)";
            Connection.ExecuteScalar<RoleUser>(strSql, new { roleUser });
        }

        public List<RoleUser> GetUsers(int roleId)
        {
            const string strSql = @"
                SELECT DISTINCT RolesId, UsersId
                FROM [RoleUser]
                WHERE (RolesId = @RolesId)";
            return Connection.Query<RoleUser>(strSql, new { RolesId = roleId }).ToList();
        }

        public List<RoleUser> GetRoles(int userId)
        {
            const string strSql = @"
                SELECT DISTINCT UsersId, RolesId
                FROM [RoleUser]
                WHERE (UsersId = @UsersId)";
            return Connection.Query<RoleUser>(strSql, new { UsersId = userId }).ToList();
        }
    }
}