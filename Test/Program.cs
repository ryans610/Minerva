using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RyanJuan.Minerva.Common;
using RyanJuan.Minerva.SqlClientHelper;

namespace Test
{
    internal class Program
    {
        private static async Task Main(string[] args)
        {
            var core = new MinervaCore(SqlClientFactory.Instance);
            //var l=core.GetValidDbTypes();
            try
            {
                //using (var conn = await OpenConnectionAsync())
                using (var conn = core.OpenConnection(s_connStr) as SqlConnection)
                using (var command = conn.CreateCommand())
                {
                    command.CommandText = @"insert into SomeData (Name) values ('test')";
                    await command.ExecuteNonQueryAsync();
                    using (var command2 = conn.CreateCommand())
                    {
                        command2.CommandText = @"select SCOPE_IDENTITY()";
                        int i=(int)(decimal)await command2.ExecuteScalarAsync();
                    }

                    //command.CommandText = @"select ThisData from SomeData where Id=3";
                    //var result = command.ExecuteScalar();
                    //var b = result is DBNull;
                    //var r = command.ExecuteReader().GetName(0);

                    //command.CommandText = @"select * from SomeData";
                    //var list = await command.FetchDataAsync<SomeDataModel>();
                    //using(var reader=await command.ExecuteReaderAsync())
                    //{
                    //    reader.Read();
                    //    var l = reader.FieldCount;
                    //    var values = new object[3];
                    //    reader.GetValues(values);
                    //    var r = values.Select(x => x is DBNull);
                    //}
                }
            }
            catch(Exception ex)
            {

            }
        }

        //public static async Task<SqlConnection> OpenConnectionAsync()
        //{
        //    return await MinervaSqlClient.OpenConnectionAsync(@"Data Source=DESKTOP-P3E5GPF\RYANDB;Initial Catalog=SGLPDB;Persist Security Info=True;User ID=SGLPConn;Password=3edcBGT%7ujm");
        //}

        private static string s_connStr = @"Data Source=DESKTOP-P3E5GPF\RYANDB;Initial Catalog=SGLPDB;Persist Security Info=True;User ID=SGLPConn;Password=3edcBGT%7ujm";
    }

    public class SomeDataModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string ThisData { get; set; }

        public string ThatData { get; set; }

        public DateTime TimeStamp { get; set; }
    }
}
