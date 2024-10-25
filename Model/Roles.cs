using Dapper;
using Microsoft.Data.SqlClient;

namespace WebAPI.Model
{
    public class Roles
    {
        public string RoleName { get; set; }
        public string RoleDesc { get; set; }
        public object GUID { get; set; }
        private String GetConnection()
        {
            var builder = new ConfigurationBuilder()
                     .SetBasePath(Directory.GetCurrentDirectory())
                     .AddJsonFile("appsettings.json");
            var config = builder.Build();
            return config["ConnectionStrings:DefaultConnectionString"];
        }

        public List<Roles> GetList()
        {
            //string sqlcomm = @"select * from SaleOrder where 1=1";

            using (SqlConnection conn = new SqlConnection(GetConnection()))
            {
                List<Roles> list = conn.Query<Roles>("SELECT GUID,RoleName,RoleDesc from Roles").ToList();
                return list;
            }
        }

    }
}
