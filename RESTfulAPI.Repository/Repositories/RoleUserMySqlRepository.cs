using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using RESTfulAPI.Model.Models;
using RESTfulAPI.Repository.Interfaces;

namespace RESTfulAPI.Repository.Repositories
{
    public class RoleUserMySqlRepository : IRoleUserInterface
    {
        public IDbConnection Connection { get; }

        public RoleUserMySqlRepository(IDbInterface db)
        {
            Connection = db.GetDb();
        }

        public int Add(RoleUser role)
        {
            const string strSql = "INSERT INTO `RoleUser` (RolesId, UsersId) VALUES (@RolesId, @UsersId)";
            return Connection.ExecuteScalar<int>(strSql, new { role });
        }

        public void Delete(RoleUser role)
        {
            const string strSql = "DELETE FROM `RoleUser` WHERE (UsersId = @UsersId) AND (RolesId = @RolesId)";
            Connection.ExecuteScalar<RoleUser>(strSql, new { role });
        }

        public void Update(RoleUser role)
        {
            const string strSql = "UPDATE `RoleUser` SET UsersId = @UsersId WHERE (UsersId = @UsersId) AND (RolesId = @RolesId)";
            Connection.ExecuteScalar<RoleUser>(strSql, new { role });
        }

        public List<RoleUser> GetUsers(int roleId)
        {
            const string strSql = @"
                SELECT DISTINCT RolesId, UsersId
                FROM `RoleUser`
                WHERE (RolesId = @RolesId)";
            return Connection.Query<RoleUser>(strSql, new { RolesId = roleId }).ToList();
        }

        public List<RoleUser> GetRoles(int userId)
        {
            const string strSql = @"
                SELECT DISTINCT UsersId, RolesId
                FROM `RoleUser`
                WHERE (UsersId = @UsersId)";
            return Connection.Query<RoleUser>(strSql, new { UsersId = userId }).ToList();
        }
    }
}