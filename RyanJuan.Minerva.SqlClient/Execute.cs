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
        /// 針對連接執行 Transact-SQL 陳述式，並傳回受影響的資料列數目。
        /// <para>
        /// 會以 <see cref="SqlCommand.Parameters"/> 呼叫
        /// <see cref="AddWithValues(SqlParameterCollection, object[])"/> 以存入參數。
        /// </para>
        /// </summary>
        /// <param name="command"><see cref="SqlCommand"/> 物件。</param>
        /// <param name="parameters">用於 Transact-SQL 陳述式的參數物件。</param>
        /// <returns>受影響的資料列數目。</returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="InvalidCastException"></exception>
        /// <exception cref="SqlException"></exception>
        /// <exception cref="System.IO.IOException"></exception>
        /// <exception cref="InvalidOperationException"></exception>
        /// <exception cref="ObjectDisposedException"></exception>
        public static int Execute(
            this SqlCommand command,
            params object[] parameters)
        {
            return s_core.Execute(
                command,
                parameters);
        }

        /// <summary>
        /// 非同步版本的 <see cref="Execute(SqlCommand, object[])"/>。
        /// 針對連接執行 Transact-SQL 陳述式，並傳回受影響的資料列數目。
        /// <para>
        /// 會以 <see cref="SqlCommand.Parameters"/> 呼叫
        /// <see cref="AddWithValues(SqlParameterCollection, object[])"/> 以存入參數。
        /// </para>
        /// </summary>
        /// <param name="command"><see cref="SqlCommand"/> 物件。</param>
        /// <param name="parameters">用於 Transact-SQL 陳述式的參數物件。</param>
        /// <returns>受影響的資料列數目。</returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="InvalidCastException"></exception>
        /// <exception cref="SqlException"></exception>
        /// <exception cref="System.IO.IOException"></exception>
        /// <exception cref="InvalidOperationException"></exception>
        /// <exception cref="ObjectDisposedException"></exception>
        public static Task<int> ExecuteAsync(
            this SqlCommand command,
            params object[] parameters)
        {
            return command.ExecuteAsync(
                CancellationToken.None,
                parameters);
        }

        /// <summary>
        /// 非同步版本的 <see cref="Execute(SqlCommand, object[])"/>。
        /// 針對連接執行 Transact-SQL 陳述式，並傳回受影響的資料列數目。
        /// 取消語彙基元可用於要求在命令逾時之前取消作業。
        /// <para>
        /// 會以 <see cref="SqlCommand.Parameters"/> 呼叫
        /// <see cref="AddWithValues(SqlParameterCollection, object[])"/> 以存入參數。
        /// </para>
        /// </summary>
        /// <param name="command"><see cref="SqlCommand"/> 物件。</param>
        /// <param name="cancellationToken">取消指令。</param>
        /// <param name="parameters">用於 Transact-SQL 陳述式的參數物件。</param>
        /// <returns>受影響的資料列數目。</returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="InvalidCastException"></exception>
        /// <exception cref="SqlException"></exception>
        /// <exception cref="System.IO.IOException"></exception>
        /// <exception cref="InvalidOperationException"></exception>
        /// <exception cref="ObjectDisposedException"></exception>
        public static Task<int> ExecuteAsync(
            this SqlCommand command,
            CancellationToken cancellationToken,
            params object[] parameters)
        {
            return s_core.ExecuteAsync(
                command,
                cancellationToken,
                parameters);
        }
    }
}
