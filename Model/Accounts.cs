using Dapper;
using Microsoft.Data.SqlClient;

namespace WebAPI.Model
{
    public class Accounts
    {
        public object GUID { get; set; }
        public string ID { get; set; }
        public string UserName { get; set; }
        public string Language { get; set; }
        public string Email { get; set; }

        private String GetConnection()
        {
            var builder = new ConfigurationBuilder()
                     .SetBasePath(Directory.GetCurrentDirectory())
                     .AddJsonFile("appsettings.json");
            var config = builder.Build();
            return config["ConnectionStrings:DefaultConnectionString"];
        }

        public List<Accounts> GetAllList()
        {
            using (SqlConnection conn = new SqlConnection(GetConnection()))
            {
                List<Accounts> list = conn.Query<Accounts>("SELECT GUID,ID,UserName,Language,Email FROM UserInfo").ToList();
                return list;
            }
        }

        public List<Accounts> GetOneRoleList()
        {
            using (SqlConnection conn = new SqlConnection(GetConnection()))
            {
                List<Accounts> list = conn.Query<Accounts>("SSELECT UserInfo.GUID,UserInfo.ID,UserInfo.Language,UserInfo.UserName \r\nFROM RoleUser \r\ninner join UserInfo on RoleUser.UserUID=UserInfo.GUID\r\nwhere RoleUser.RoleUID=''").ToList();
                return list;
            }
        }
    }
}
