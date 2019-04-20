using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using RyanJuan.Minerva.Common;

namespace RyanJuan.Minerva.SqlClientHelper
{
    public static partial class MinervaSqlClient
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="connectionString"></param>
        /// <returns></returns>
        public static SqlConnection OpenConnection(
            string connectionString)
        {
            return s_core.OpenConnection(connectionString) as SqlConnection;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="connectionString"></param>
        /// <param name="credential"></param>
        /// <returns></returns>
        public static SqlConnection OpenConnection(
            string connectionString,
            SqlCredential credential)
        {
            var conn = new SqlConnection(
                connectionString,
                credential);
            conn.Open();
            return conn;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="connectionString"></param>
        /// <returns></returns>
        public static async Task<SqlConnection> OpenConnectionAsync(
            string connectionString)
        {
            return await OpenConnectionAsync(
                connectionString,
                CancellationToken.None);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="connectionString"></param>
        /// <param name="credential"></param>
        /// <returns></returns>
        public static async Task<SqlConnection> OpenConnectionAsync(
            string connectionString,
            SqlCredential credential)
        {
            return await OpenConnectionAsync(
                connectionString,
                credential,
                CancellationToken.None);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="connectionString"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public static async Task<SqlConnection> OpenConnectionAsync(
            string connectionString,
            CancellationToken cancellationToken)
        {
            return await s_core.OpenConnectionAsync(
                connectionString,
                cancellationToken) as SqlConnection;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="connectionString"></param>
        /// <param name="credential"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public static async Task<SqlConnection> OpenConnectionAsync(
            string connectionString,
            SqlCredential credential,
            CancellationToken cancellationToken)
        {
            var conn = new SqlConnection(
                connectionString,
                credential);
            await conn.OpenAsync(cancellationToken);
            return conn;
        }
    }
}
