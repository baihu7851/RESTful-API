using Autofac;
using RESTfulAPI.Middleware.Interfaces;
using RESTfulAPI.Repository.Interfaces;
using RESTfulAPI.Repository.Repositories;

namespace RESTfulAPI.ApiController
{
    public class AutodacModule :Autofac.Module
    {
        protected override void Load(ContainerBuilder containerBuilder)
        {
            containerBuilder.RegisterType<Middleware.User>().As<IUser>();
            containerBuilder.RegisterType<UserSqlServerRepository>().As<IUserInterface>();
            containerBuilder.RegisterType<RoleSqlServerRepository>().As<IRoleInterface>();
            containerBuilder.RegisterType<RoleUserSqlServerRepository>().As<IRoleUserInterface>();
        }
    }
}