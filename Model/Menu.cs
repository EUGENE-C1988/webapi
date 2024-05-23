using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace WebAPI.Model
{
    public class Menu
    {
        //建構子於起始時注入config檔
        //private readonly IConfiguration _configuration;
        //public Menu(IConfiguration configuration)
        //{
        //    _configuration = configuration;
        //}

        public string DisplayName { get; set; }
        public Guid ParentUID { get; set; }
        public Guid PageUID { get; set; }
        public string Description { get; set; }
        public int Hierarchy { get; set; }
        public int SortBy { get; set; }
        //public string IconLeft { get; set; }
        public string URL { get; set; }
        public Guid GUID { get; set; }

        private String GetConnection()
        {
            var builder = new ConfigurationBuilder()
                     .SetBasePath(Directory.GetCurrentDirectory())
                     .AddJsonFile("appsettings.json");
            var config = builder.Build();
            return config["ConnectionStrings:DefaultConnectionString"];
        }

        public List<Menu> GetMenusAdmin()
        {
            //string connectionString = _configuration.GetConnectionString("DefaultConnectionString");
            //string connectionString = _configuration["ConnectionStrings:DefaultConnectionString"];
            string sql = @"SELECT [GUID]
      ,[DisplayName]
      ,[ParentUID]
      ,[PageUID]
      ,[Hierarchy]
      ,[Sortby]
      ,[Description]
      ,[URL]
      ,[IconLeft]
        FROM [sideproject].[dbo].[Menu]";
            using (SqlConnection conn = new SqlConnection(GetConnection()))
            {
                List<Menu> list = conn.Query<Menu>(sql).ToList();
                return list;
            }

        }
    }
}
