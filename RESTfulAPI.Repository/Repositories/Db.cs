using System;
using System.Data;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using MySqlConnector;
using RESTfulAPI.Repository.Interfaces;

namespace RESTfulAPI.Repository.Repositories
{
    public class Db : IDb
    {
        public Db(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public IDbConnection GetDb()
        {
            return Environment.GetEnvironmentVariable("Database") switch
            {
                "SqlServer" => new SqlConnection(Configuration.GetConnectionString("SqlServer")),
                "MySql" => new MySqlConnection(Configuration.GetConnectionString("MySql")),
                _ => null
            };
        }
    }
}