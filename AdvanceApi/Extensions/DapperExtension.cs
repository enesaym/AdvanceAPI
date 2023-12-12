using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Data;
using System.Data.SqlClient;

namespace AdvanceApi.Extensions
{
    public static class DapperExtension
    {
        public static void AddDapper(this IServiceCollection services,IConfiguration conf)
        {
            string connectionString = conf.GetConnectionString("myconn");
            services.AddScoped<IDbConnection>(conn => new SqlConnection(connectionString));

        }
    }
}
