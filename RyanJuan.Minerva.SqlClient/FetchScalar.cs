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
        /// 執行查詢，並傳回查詢所傳回的結果集第一個資料列的第一個資料行。
        /// 會忽略其他的資料行或資料列。
        /// <para>
        /// 會以 <see cref="SqlCommand.Parameters"/> 呼叫
        /// <see cref="AddWithValues(SqlParameterCollection, object[])"/> 以存入參數。
        /// </para>
        /// </summary>
        /// <typeparam name="T">查詢結果的資料型別。</typeparam>
        /// <param name="command"><see cref="SqlCommand"/> 物件。</param>
        /// <param name="parameters">用於查詢的參數物件。</param>
        /// <returns>查詢結果。</returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="InvalidCastException"></exception>
        /// <exception cref="SqlException"></exception>
        /// <exception cref="System.IO.IOException"></exception>
        /// <exception cref="InvalidOperationException"></exception>
        /// <exception cref="ObjectDisposedException"></exception>
        public static T FetchScalar<T>(
            this SqlCommand command,
            params object[] parameters)
        {
            return s_core.FetchScalar<T>(
                command,
                parameters);
        }

        /// <summary>
        /// 非同步版本的 <see cref="FetchScalar{T}(SqlCommand, object[])"/>。
        /// 執行查詢，並傳回查詢所傳回的結果集第一個資料列的第一個資料行。
        /// 會忽略其他的資料行或資料列。
        /// <para>
        /// 會以 <see cref="SqlCommand.Parameters"/> 呼叫
        /// <see cref="AddWithValues(SqlParameterCollection, object[])"/> 以存入參數。
        /// </para>
        /// </summary>
        /// <typeparam name="T">查詢結果的資料型別。</typeparam>
        /// <param name="command"><see cref="SqlCommand"/> 物件。</param>
        /// <param name="parameters">用於查詢的參數物件。</param>
        /// <returns>查詢結果。</returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="InvalidCastException"></exception>
        /// <exception cref="SqlException"></exception>
        /// <exception cref="System.IO.IOException"></exception>
        /// <exception cref="InvalidOperationException"></exception>
        /// <exception cref="ObjectDisposedException"></exception>
        public static async Task<T> FetchScalarAsync<T>(
            this SqlCommand command,
            params object[] parameters)
        {
            return await command.FetchScalarAsync<T>(
                CancellationToken.None,
                parameters);
        }

        /// <summary>
        /// 非同步版本的 <see cref="FetchScalar{T}(SqlCommand, object[])"/>。
        /// 執行查詢，並傳回查詢所傳回的結果集第一個資料列的第一個資料行。
        /// 會忽略其他的資料行或資料列。
        /// 取消語彙基元可用於要求在命令逾時之前取消作業。
        /// <para>
        /// 會以 <see cref="SqlCommand.Parameters"/> 呼叫
        /// <see cref="AddWithValues(SqlParameterCollection, object[])"/> 以存入參數。
        /// </para>
        /// </summary>
        /// <typeparam name="T">查詢結果的資料型別。</typeparam>
        /// <param name="command"><see cref="SqlCommand"/> 物件。</param>
        /// <param name="cancellationToken">取消指令。</param>
        /// <param name="parameters">用於查詢的參數物件。</param>
        /// <returns>查詢結果。</returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="InvalidCastException"></exception>
        /// <exception cref="SqlException"></exception>
        /// <exception cref="System.IO.IOException"></exception>
        /// <exception cref="InvalidOperationException"></exception>
        /// <exception cref="ObjectDisposedException"></exception>
        public static async Task<T> FetchScalarAsync<T>(
            this SqlCommand command,
            CancellationToken cancellationToken,
            params object[] parameters)
        {
            return await s_core.FetchScalarAsync<T>(
                command,
                cancellationToken,
                parameters);
        }
    }
}
