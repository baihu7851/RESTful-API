using System.Data;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using RESTfulAPI.Repository.Interfaces;

namespace RESTfulAPI.Repository.Repositories
{
    public class DbMsSql : IDbInterface
    {
        public DbMsSql(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public IDbConnection GetDb()
        {
            return new SqlConnection(Configuration.GetConnectionString("MsSql"));
        }
    }
}