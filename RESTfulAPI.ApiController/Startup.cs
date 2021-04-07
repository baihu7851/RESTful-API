using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;
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
            switch (Environment.GetEnvironmentVariable("Database"))
            {
                case "SqlServer":
                    services.AddDbContext<ModelContext>(options => options.UseSqlServer(Configuration.GetConnectionString("SqlServer")));
                    services.AddSingleton<IUser, UserSqlServerRepository>();
                    break;

                case "MySql":
                    services.AddDbContext<ModelContext>(options => options.UseMySql(Configuration.GetConnectionString("MySql"),
                        new MySqlServerVersion(new Version(8, 0, 23)),
                        mySqlOptions => mySqlOptions.CharSetBehavior(CharSetBehavior.NeverAppend)));
                    break;
            }
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