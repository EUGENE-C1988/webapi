using Dapper;
using Microsoft.Data.SqlClient;


namespace WebAPI.Model
{
    public class So
    {
        public int Id { get; set; }
        public string OrderNo { get; set; }
        public int OrderItem { get; set; }
        public string ProductName { get; set; }
        public int Qty { get; set; }
        public int Price { get; set; }
        public int Amount { get; set; }

        private String GetConnection()
        {
            var builder = new ConfigurationBuilder()
                     .SetBasePath(Directory.GetCurrentDirectory())
                     .AddJsonFile("appsettings.json");
            var config = builder.Build();
            return config["ConnectionStrings:DefaultConnectionString"];
        }

        /// <summary>
        /// 查詢所有訂單
        /// </summary>
        /// <returns></returns>


        public List<So> GetList()
        {
            //string sqlcomm = @"select * from SaleOrder where 1=1";

            using (SqlConnection conn = new SqlConnection(GetConnection()))
            {
                List<So> list = conn.Query<So>("select Id,OrderNo,OrderItem,ProductName,Qty,Price,Amount from SaleOrder").ToList();
                return list;
            }
        }

        public List<So> GetElement(int id)
        {
            String sqlcomm = @"select * from SaleOrder where Id=@id";
            var parameters = new DynamicParameters();
            parameters.Add("id", id);
            using(SqlConnection conn = new SqlConnection(GetConnection()))
            {
                List<So> list=conn.Query<So>(sqlcomm,parameters).ToList();
                return list;
            }
            
        }
        

    }

}
