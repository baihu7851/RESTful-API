using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;
using RESTfulAPI.Model.Models;
using RESTfulAPI.Repository.Interfaces;
using RESTfulAPI.Repository.Repositories;
using RiskFirst.Hateoas;

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
            #region Migrations

            switch (Environment.GetEnvironmentVariable("Database"))
            {
                case "SqlServer":
                    services.AddDbContext<ModelContext>(options => options.UseSqlServer(Configuration.GetConnectionString("SqlServer")));
                    break;

                case "MySql":
                    services.AddDbContext<ModelContext>(options => options.UseMySql(Configuration.GetConnectionString("MySql"),
                        new MySqlServerVersion(new Version(8, 0, 23)),
                        mySqlOptions => mySqlOptions.CharSetBehavior(CharSetBehavior.NeverAppend)));
                    break;
            }

            #endregion Migrations

            // 注入 DB
            services.AddSingleton<IDbInterface, Db>();
            // 注入資料介面
            services.AddSingleton<IDataInterface, UserSqlServerRepository>();
            // 注入 Links
            services.AddLinks(config =>
            {
                config.AddPolicy<User>(policy =>
                {
                    policy.RequireSelfLink()
                        .RequireRoutedLink("user", "GetUserRoute", x => new { id = x.Id })
                        .RequireRoutedLink("delete", "DeleteUserRoute", x => new { id = x.Id });
                });
            });

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
    }
}