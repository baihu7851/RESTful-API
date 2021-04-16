using System;
using System.Reflection;
using Autofac;
using Microsoft.Data.SqlClient;
using MySqlConnector;
using Module = Autofac.Module;

namespace RESTfulAPI.ApiController
{
    public class AutofacModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            Assembly repository = Assembly.Load("RESTfulAPI.Repository");
            Assembly middleware = Assembly.Load("RESTfulAPI.Middleware");
            switch (Environment.GetEnvironmentVariable("Database"))
            {
                case "MsSql":
                    builder.RegisterAssemblyTypes(repository)
                        .Where(t => t.Name.EndsWith("MsSql"))
                        .AsImplementedInterfaces();
                    builder.RegisterAssemblyTypes(repository)
                        .Where(t => t.Name.EndsWith("SqlServerRepository"))
                        .AsImplementedInterfaces();
                    break;

                case "MySql":
                    builder.RegisterAssemblyTypes(repository)
                        .Where(t => t.Name.EndsWith("MySql"))
                        .AsImplementedInterfaces();
                    builder.RegisterAssemblyTypes(repository)
                        .Where(t => t.Name.EndsWith("MySqlRepository"))
                        .AsImplementedInterfaces();
                    break;
            }
            builder.RegisterAssemblyTypes(middleware)
                .Where(t => t.Name.EndsWith("Mw"))
                .AsImplementedInterfaces();
        }
    }
}