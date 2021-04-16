using System.Data;
using Microsoft.Extensions.Configuration;
using MySqlConnector;
using RESTfulAPI.Repository.Interfaces;

namespace RESTfulAPI.Repository.Repositories
{
    public class DbMySql : IDbInterface
    {
        public DbMySql(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public IDbConnection GetDb()
        {
            return new MySqlConnection(Configuration.GetConnectionString("MySql"));
        }
    }
}