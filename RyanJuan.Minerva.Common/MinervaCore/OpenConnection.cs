using System.Data.Common;
using System.Threading;
using System.Threading.Tasks;

namespace RyanJuan.Minerva.Common
{
    public sealed partial class MinervaCore
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="connectionString"></param>
        /// <returns></returns>
        public DbConnection OpenConnection(
            string connectionString)
        {
            var conn = _factory.CreateConnection();
            conn.ConnectionString = connectionString;
            conn.Open();
            return conn;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="connectionString"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<DbConnection> OpenConnectionAsync(
            string connectionString,
            CancellationToken cancellationToken)
        {
            var conn = _factory.CreateConnection();
            conn.ConnectionString = connectionString;
            await conn.OpenAsync(cancellationToken);
            return conn;
        }
    }
}
