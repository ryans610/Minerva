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
        /// 使用指定的 <see cref="SqlCommand"/> 開始資料庫交易。
        /// </summary>
        /// <param name="connection">資料庫連線。</param>
        /// <param name="command">指定的 <see cref="SqlCommand"/> 物件。</param>
        /// <returns>資料庫交易。</returns>
        /// <exception cref="SqlException"></exception>
        /// <exception cref="InvalidOperationException"></exception>
        /// <exception cref="ArgumentNullException"></exception>
        public static SqlTransaction BeginTransaction(
            this SqlConnection connection,
            SqlCommand command)
        {
            return connection.BeginTransaction(
                command,
                IsolationLevel.Unspecified);
        }

        /// <summary>
        /// 使用指定的 <see cref="SqlCommand"/> 與交易層級開始資料庫交易。
        /// </summary>
        /// <param name="connection">資料庫連線。</param>
        /// <param name="command">指定的 <see cref="SqlCommand"/> 物件。</param>
        /// <param name="iso">應該在其下執行交易的隔離等級。</param>
        /// <returns>資料庫交易。</returns>
        /// <exception cref="SqlException"></exception>
        /// <exception cref="InvalidOperationException"></exception>
        /// <exception cref="ArgumentNullException"></exception>
        public static SqlTransaction BeginTransaction(
            this SqlConnection connection,
            SqlCommand command,
            IsolationLevel iso)
        {
            return s_core.BeginTransaction(
                connection,
                command,
                iso) as SqlTransaction;
        }

        /// <summary>
        /// 使用指定的 <see cref="SqlCommand"/> 與交易名稱開始資料庫交易。
        /// </summary>
        /// <param name="connection">資料庫連線。</param>
        /// <param name="command">指定的 <see cref="SqlCommand"/> 物件。</param>
        /// <param name="transactionName">異動名稱。</param>
        /// <returns>資料庫交易。</returns>
        /// <exception cref="SqlException"></exception>
        /// <exception cref="InvalidOperationException"></exception>
        /// <exception cref="ArgumentNullException"></exception>
        public static SqlTransaction BeginTransaction(
            this SqlConnection connection,
            SqlCommand command,
            string transactionName)
        {
            return connection.BeginTransaction(
                command,
                IsolationLevel.Unspecified,
                transactionName);
        }

        /// <summary>
        /// 使用指定的 <see cref="SqlCommand"/>、交易層級與交易名稱開始資料庫交易。
        /// </summary>
        /// <param name="connection">資料庫連線。</param>
        /// <param name="command">指定的 <see cref="SqlCommand"/> 物件。</param>
        /// <param name="iso">應該在其下執行交易的隔離等級。</param>
        /// <param name="transactionName">異動名稱。</param>
        /// <returns>資料庫交易。</returns>
        /// <exception cref="SqlException"></exception>
        /// <exception cref="InvalidOperationException"></exception>
        /// <exception cref="ArgumentNullException"></exception>
        public static SqlTransaction BeginTransaction(
            this SqlConnection connection,
            SqlCommand command,
            IsolationLevel iso,
            string transactionName)
        {
            if (connection is null)
            {
                throw new ArgumentNullException(nameof(connection));
            }
            if (command is null)
            {
                throw new ArgumentNullException(nameof(command));
            }
            var trans = connection.BeginTransaction(
                iso,
                transactionName);
            command.Transaction = trans;
            return trans;
        }
    }
}
