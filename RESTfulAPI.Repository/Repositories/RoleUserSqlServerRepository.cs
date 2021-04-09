using System.Collections.Generic;
using System.Data;
using System.Linq;
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
                Connection.ExecuteScalar<RoleUser>(strSql, new { RolesId = roleId, UsersId = userId });
            }
        }

        public void Delete<T>(int roleId, List<T> usersId)
        {
            const string strSql = "DELETE FROM [RoleUser] WHERE (UsersId = @UsersId) AND (RolesId = @RolesId)";
            foreach (var userId in usersId)
            {
                Connection.ExecuteScalar<RoleUser>(strSql, new { RolesId = roleId, UsersId = userId });
            }
        }

        public void Update<T>(int roleId, List<T> usersId)
        {
            const string strSql = "UPDATE [RoleUser] SET UsersId = @UsersId WHERE (RolesId = @RolesId)";
            foreach (var userId in usersId)
            {
                Connection.ExecuteScalar<RoleUser>(strSql, new { RolesId = roleId, UsersId = userId });
            }
        }

        public List<RoleUser> View()
        {
            List<RoleUser> roleUsers = new List<RoleUser>();
            foreach (var roleId in GetRoleId())
            {
                RoleUser roleUser = GetRoleData(roleId);
                foreach (var userId in GetUserId(roleId))
                {
                    roleUser.UserDates ??= new List<UserDate>();
                    roleUser.UserDates.Add(GetUseData(userId));
                }
                roleUsers.Add(roleUser);
            }

            return roleUsers;
        }

        public RoleUser View(int roleId)
        {
            RoleUser roleUser = GetRoleData(roleId);
            foreach (var userId in GetUserId(roleId))
            {
                roleUser.UserDates ??= new List<UserDate>();
                roleUser.UserDates.Add(GetUseData(userId));
            }
            return roleUser;
        }

        #region 關聯

        private IEnumerable<int> GetRoleId()
        {
            const string strSql = "SELECT DISTINCT RolesId FROM RoleUser";
            return Connection.Query<int>(strSql).ToList();
        }

        private RoleUser GetRoleData(int roleId)
        {
            const string strSql = @"
                SELECT Id AS RoleId, RoleName
                FROM [Roles]
                WHERE (Id = @Id)";
            return Connection.QueryFirstOrDefault<RoleUser>(strSql, new { Id = roleId });
        }

        public List<int> GetUserId(int roleId)
        {
            const string strSql = @"
                SELECT DISTINCT [UsersId]
                FROM [RoleUser]
                WHERE ([RoleUser].RolesId = @RolesId)";
            return Connection.Query<int>(strSql, new { RolesId = roleId }).ToList();
        }

        public UserDate GetUseData(int userId)
        {
            const string strSql = @"
                SELECT Id AS UserId ,UserName
                FROM [Users]
                WHERE (Id = @Id)";
            return Connection.QueryFirstOrDefault<UserDate>(strSql, new { Id = userId });
        }

        #endregion 關聯
    }
}