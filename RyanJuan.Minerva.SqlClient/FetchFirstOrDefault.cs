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
        /// 執行查詢，並傳回查詢所傳回的結果集中唯一一筆資料，如果沒有資料，則為預設值。
        /// 如果結果集中有多個項目，則這個方法會擲回例外狀況。
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
        public static T FetchFirstOrDefault<T>(
            this SqlCommand command,
            params object[] parameters)
        {
            return s_core.FetchFirstOrDefault<T>(
                command,
                parameters);
        }

        /// <summary>
        /// 非同步版本的 <see cref="FetchFirstOrDefault{T}(SqlCommand, object[])"/>。
        /// 執行查詢，並傳回查詢所傳回的結果集中唯一一筆資料，如果沒有資料，則為預設值。
        /// 如果結果集中有多個項目，則這個方法會擲回例外狀況。
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
        public static async Task<T> FetchFirstOrDefaultAsync<T>(
            this SqlCommand command,
            params object[] parameters)
        {
            return await command.FetchFirstOrDefaultAsync<T>(
                CancellationToken.None,
                parameters);
        }

        /// <summary>
        /// 非同步版本的 <see cref="FetchFirstOrDefault{T}(SqlCommand, object[])"/>。
        /// 執行查詢，並傳回查詢所傳回的結果集中唯一一筆資料，如果沒有資料，則為預設值。
        /// 如果結果集中有多個項目，則這個方法會擲回例外狀況。
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
        public static async Task<T> FetchFirstOrDefaultAsync<T>(
            this SqlCommand command,
            CancellationToken cancellationToken,
            params object[] parameters)
        {
            return await s_core.FetchFirstOrDefaultAsync<T>(
                command,
                cancellationToken,
                parameters);
        }
    }
}
