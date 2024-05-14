using Dapper;
using Microsoft.Data.SqlClient;
using System.Reflection.Metadata;
using WebAPI.Model;

namespace WebAPI.Parameter
{
    public class SoParameter
    {
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

        public int InsertDB(SoParameter parameter)
        {
            String sqlcomm = @"insert into SaleOrder (OrderNo,OrderItem,ProductName,Qty,Price,Amount) 
            values (@OrderNo,@OrderItem,@ProductName,@Qty,@Price,@Amount);select max(Id) from SaleOrder;
            ";
            using (SqlConnection conn = new SqlConnection(GetConnection()))
            {
                var result = conn.QueryFirstOrDefault<int>(sqlcomm, parameter);
                return result;
            }

        }

        public int UpdateDB(SoParameter parameter, int id)
        {
            String sqlcomm = @"update SaleOrder set 
            OrderNo=@OrderNo ,OrderItem=@OrderItem,ProductName=@ProductName,Qty=@Qty,Price=@Price,Amount=@Amount where Id=@id";
            var parameters = new DynamicParameters(parameter);
            parameters.Add("id", id, System.Data.DbType.Int32);
            using (SqlConnection conn = new SqlConnection(GetConnection()))
            {
                var result=conn.Execute(sqlcomm, parameters);
                return result;
            }
            
        }

        public int DeleteDB(int id)
        {
            String sqlcomm = @"delete from SaleOrder where id=@id";
            var parameters = new DynamicParameters(sqlcomm);
            parameters.Add("id", id,System.Data.DbType.Int32);
            using (SqlConnection conn = new SqlConnection(GetConnection()))
            {
                var result=conn.Execute(sqlcomm,parameters);
                return result;
            }
        }


    }
}
