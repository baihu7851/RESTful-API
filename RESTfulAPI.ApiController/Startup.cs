using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System;
using Autofac;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;
using RESTfulAPI.Middleware;
using RESTfulAPI.Middleware.Interfaces;
using RESTfulAPI.Model.Models;
using RESTfulAPI.Repository.Interfaces;
using RESTfulAPI.Repository.Repositories;

namespace RESTfulAPI.ApiController
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // �`�J�@�q���
            services.AddSingleton<IDbInterface, Db>();
            services.AddMemoryCache();
            services.AddTransient<IActionContextAccessor, ActionContextAccessor>();
            services.AddTransient<IUser, UserMw>();
            services.AddTransient<IRole, RoleMw>();

            #region Migrations

            switch (Environment.GetEnvironmentVariable("Database"))
            {
                case "SqlServer":
                    services.AddDbContext<ModelContext>(options => options.UseSqlServer(Configuration.GetConnectionString("SqlServer")));
                    // �`�J SqlServer ��Ƥ���
                    services.AddTransient<IUserInterface, UserSqlServerRepository>();
                    services.AddTransient<IRoleInterface, RoleSqlServerRepository>();
                    services.AddTransient<IRoleUserInterface, RoleUserSqlServerRepository>();

                    break;

                case "MySql":
                    services.AddDbContext<ModelContext>(options => options.UseMySql(Configuration.GetConnectionString("MySql"),

                        new MySqlServerVersion(new Version(8, 0, 23)),
                        mySqlOptions => mySqlOptions.CharSetBehavior(CharSetBehavior.NeverAppend)));
                    // �`�J MySql ��Ƥ���
                    services.AddSingleton<IUserInterface, UserMySqlRepository>();
                    services.AddSingleton<IRoleInterface, RoleMySqlRepository>();
                    services.AddTransient<IRoleUserInterface, RoleUserMySqlRepository>();
                    break;
            }

            #endregion Migrations

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "RESTfulAPI.ApiController", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "RESTfulAPI.ApiController v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        public void ConfigureContainer(ContainerBuilder builder)
        {
            //builder.RegisterModule<AutofacModule>();
            //Assembly service = Assembly.Load("NetCoreDemo.Service");
            //Assembly repository = Assembly.Load("NetCoreDemo.Repository");
            //builder.RegisterAssemblyTypes(service, repository)
            //    .Where(t => t.Name.EndsWith("Service"))
            //    .AsImplementedInterfaces();
            //builder.RegisterGeneric(typeof(ViewUser))
            //    .As(typeof(IUser)).InstancePerDependency();
        }
    }
}