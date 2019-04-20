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
        /// 執行查詢，並傳回查詢所傳回的結果集序列。
        /// <para>
        /// 查詢結果的資料型別若為一般值型別，會直接以值回傳序列。
        /// 型別若為物件，則會以查詢結果集的資料欄位名稱，對映物件中可寫入的屬性。
        /// 物件屬性若有設定 <see cref="DbColumnNameAttribute"/> 時，會以此欄位名稱做為對映，
        /// 若有多個屬性都有設定相同名稱的 <see cref="DbColumnNameAttribute"/>，
        /// 則單一欄位會對映到複數物件屬性。
        /// 如果沒有符合的 <see cref="DbColumnNameAttribute"/>，則會使用屬性名稱對映。
        /// </para>
        /// <para>
        /// 會以 <see cref="SqlCommand.Parameters"/> 呼叫
        /// <see cref="AddWithValues(SqlParameterCollection, object[])"/> 以存入參數。
        /// </para>
        /// </summary>
        /// <typeparam name="T">查詢結果的資料型別。</typeparam>
        /// <param name="command"><see cref="SqlCommand"/> 物件。</param>
        /// <param name="parameters">用於查詢的參數物件。</param>
        /// <returns>查詢結果序列。</returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="MissingMethodException"></exception>
        /// <exception cref="InvalidCastException"></exception>
        /// <exception cref="SqlException"></exception>
        /// <exception cref="System.IO.IOException"></exception>
        /// <exception cref="InvalidOperationException"></exception>
        /// <exception cref="ObjectDisposedException"></exception>
        public static IEnumerable<T> FetchData<T>(
            this SqlCommand command,
            params object[] parameters)
        {
            return command.FetchData<T>(
                CommandBehavior.Default,
                FetchMode.Default,
                parameters);
        }

        /// <summary>
        /// 執行查詢，並傳回查詢所傳回的結果集序列。
        /// <para>
        /// 查詢結果的資料型別若為一般值型別，會直接以值回傳序列。
        /// 型別若為物件，則會以查詢結果集的資料欄位名稱，對映物件中可寫入的屬性。
        /// 物件屬性若有設定 <see cref="DbColumnNameAttribute"/> 時，會以此欄位名稱做為對映，
        /// 若有多個屬性都有設定相同名稱的 <see cref="DbColumnNameAttribute"/>，
        /// 則單一欄位會對映到複數物件屬性。
        /// 如果沒有符合的 <see cref="DbColumnNameAttribute"/>，則會使用屬性名稱對映。
        /// </para>
        /// <para>
        /// 會以 <see cref="SqlCommand.Parameters"/> 呼叫
        /// <see cref="AddWithValues(SqlParameterCollection, object[])"/> 以存入參數。
        /// </para>
        /// </summary>
        /// <typeparam name="T">查詢結果的資料型別。</typeparam>
        /// <param name="command"><see cref="SqlCommand"/> 物件。</param>
        /// <param name="behavior"></param>
        /// <param name="parameters">用於查詢的參數物件。</param>
        /// <returns>查詢結果序列。</returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="MissingMethodException"></exception>
        /// <exception cref="InvalidCastException"></exception>
        /// <exception cref="SqlException"></exception>
        /// <exception cref="System.IO.IOException"></exception>
        /// <exception cref="InvalidOperationException"></exception>
        /// <exception cref="ObjectDisposedException"></exception>
        public static IEnumerable<T> FetchData<T>(
            this SqlCommand command,
            CommandBehavior behavior,
            params object[] parameters)
        {
            return command.FetchData<T>(
                behavior,
                FetchMode.Default,
                parameters);
        }

        /// <summary>
        /// 執行查詢，並傳回查詢所傳回的結果集序列。
        /// <para>
        /// 查詢結果的資料型別若為一般值型別，會直接以值回傳序列。
        /// 型別若為物件，則會以查詢結果集的資料欄位名稱，對映物件中可寫入的屬性。
        /// 物件屬性若有設定 <see cref="DbColumnNameAttribute"/> 時，會以此欄位名稱做為對映，
        /// 若有多個屬性都有設定相同名稱的 <see cref="DbColumnNameAttribute"/>，
        /// 則單一欄位會對映到複數物件屬性。
        /// 如果沒有符合的 <see cref="DbColumnNameAttribute"/>，則會使用屬性名稱對映。
        /// </para>
        /// <para>
        /// 會以 <see cref="SqlCommand.Parameters"/> 呼叫
        /// <see cref="AddWithValues(SqlParameterCollection, object[])"/> 以存入參數。
        /// </para>
        /// </summary>
        /// <typeparam name="T">查詢結果的資料型別。</typeparam>
        /// <param name="command"><see cref="SqlCommand"/> 物件。</param>
        /// <param name="fetchMode"></param>
        /// <param name="parameters">用於查詢的參數物件。</param>
        /// <returns>查詢結果序列。</returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="MissingMethodException"></exception>
        /// <exception cref="InvalidCastException"></exception>
        /// <exception cref="SqlException"></exception>
        /// <exception cref="System.IO.IOException"></exception>
        /// <exception cref="InvalidOperationException"></exception>
        /// <exception cref="ObjectDisposedException"></exception>
        public static IEnumerable<T> FetchData<T>(
            this SqlCommand command,
            FetchMode fetchMode,
            params object[] parameters)
        {
            return command.FetchData<T>(
                CommandBehavior.Default,
                fetchMode,
                parameters);
        }

        /// <summary>
        /// 執行查詢，並傳回查詢所傳回的結果集序列。
        /// <para>
        /// 查詢結果的資料型別若為一般值型別，會直接以值回傳序列。
        /// 型別若為物件，則會以查詢結果集的資料欄位名稱，對映物件中可寫入的屬性。
        /// 物件屬性若有設定 <see cref="DbColumnNameAttribute"/> 時，會以此欄位名稱做為對映，
        /// 若有多個屬性都有設定相同名稱的 <see cref="DbColumnNameAttribute"/>，
        /// 則單一欄位會對映到複數物件屬性。
        /// 如果沒有符合的 <see cref="DbColumnNameAttribute"/>，則會使用屬性名稱對映。
        /// </para>
        /// <para>
        /// 會以 <see cref="SqlCommand.Parameters"/> 呼叫
        /// <see cref="AddWithValues(SqlParameterCollection, object[])"/> 以存入參數。
        /// </para>
        /// </summary>
        /// <typeparam name="T">查詢結果的資料型別。</typeparam>
        /// <param name="command"><see cref="SqlCommand"/> 物件。</param>
        /// <param name="behavior">其中一個 <see cref="CommandBehavior"/> 值。</param>
        /// <param name="fetchMode"></param>
        /// <param name="parameters">用於查詢的參數物件。</param>
        /// <returns>查詢結果序列。</returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="MissingMethodException"></exception>
        /// <exception cref="InvalidCastException"></exception>
        /// <exception cref="SqlException"></exception>
        /// <exception cref="System.IO.IOException"></exception>
        /// <exception cref="InvalidOperationException"></exception>
        /// <exception cref="ObjectDisposedException"></exception>
        public static IEnumerable<T> FetchData<T>(
            this SqlCommand command,
            CommandBehavior behavior,
            FetchMode fetchMode,
            params object[] parameters)
        {
            return s_core.FetchData<T>(
                command,
                behavior,
                fetchMode,
                parameters);
        }

        /// <summary>
        /// 非同步版本的 <see cref="FetchData{T}(SqlCommand, object[])"/>。
        /// 執行查詢，並傳回查詢所傳回的結果集序列。
        /// <para>
        /// 查詢結果的資料型別若為一般值型別，會直接以值回傳序列。
        /// 型別若為物件，則會以查詢結果集的資料欄位名稱，對映物件中可寫入的屬性。
        /// 物件屬性若有設定 <see cref="DbColumnNameAttribute"/> 時，會以此欄位名稱做為對映，
        /// 若有多個屬性都有設定相同名稱的 <see cref="DbColumnNameAttribute"/>，
        /// 則單一欄位會對映到複數物件屬性。
        /// 如果沒有符合的 <see cref="DbColumnNameAttribute"/>，則會使用屬性名稱對映。
        /// <para>
        /// 會以 <see cref="SqlCommand.Parameters"/> 呼叫
        /// <see cref="AddWithValues(SqlParameterCollection, object[])"/> 以存入參數。
        /// </para>
        /// </para>
        /// </summary>
        /// <typeparam name="T">查詢結果的資料型別。</typeparam>
        /// <param name="command"><see cref="SqlCommand"/> 物件。</param>
        /// <param name="parameters">用於查詢的參數物件。</param>
        /// <returns>查詢結果序列。</returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="MissingMethodException"></exception>
        /// <exception cref="InvalidCastException"></exception>
        /// <exception cref="SqlException"></exception>
        /// <exception cref="System.IO.IOException"></exception>
        /// <exception cref="InvalidOperationException"></exception>
        /// <exception cref="ObjectDisposedException"></exception>
        public static async Task<IEnumerable<T>> FetchDataAsync<T>(
            this SqlCommand command,
            params object[] parameters)
        {
            return await command.FetchDataAsync<T>(
                CommandBehavior.Default,
                FetchMode.Default,
                CancellationToken.None,
                parameters);
        }

        /// <summary>
        /// 非同步版本的 <see cref="FetchData{T}(SqlCommand, object[])"/>。
        /// 執行查詢，並傳回查詢所傳回的結果集序列。
        /// <para>
        /// 查詢結果的資料型別若為一般值型別，會直接以值回傳序列。
        /// 型別若為物件，則會以查詢結果集的資料欄位名稱，對映物件中可寫入的屬性。
        /// 物件屬性若有設定 <see cref="DbColumnNameAttribute"/> 時，會以此欄位名稱做為對映，
        /// 若有多個屬性都有設定相同名稱的 <see cref="DbColumnNameAttribute"/>，
        /// 則單一欄位會對映到複數物件屬性。
        /// 如果沒有符合的 <see cref="DbColumnNameAttribute"/>，則會使用屬性名稱對映。
        /// <para>
        /// 會以 <see cref="SqlCommand.Parameters"/> 呼叫
        /// <see cref="AddWithValues(SqlParameterCollection, object[])"/> 以存入參數。
        /// </para>
        /// </para>
        /// </summary>
        /// <typeparam name="T">查詢結果的資料型別。</typeparam>
        /// <param name="command"><see cref="SqlCommand"/> 物件。</param>
        /// <param name="behavior">其中一個 <see cref="CommandBehavior"/> 值。</param>
        /// <param name="parameters">用於查詢的參數物件。</param>
        /// <returns>查詢結果序列。</returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="MissingMethodException"></exception>
        /// <exception cref="InvalidCastException"></exception>
        /// <exception cref="SqlException"></exception>
        /// <exception cref="System.IO.IOException"></exception>
        /// <exception cref="InvalidOperationException"></exception>
        /// <exception cref="ObjectDisposedException"></exception>
        public static async Task<IEnumerable<T>> FetchDataAsync<T>(
            this SqlCommand command,
            CommandBehavior behavior,
            params object[] parameters)
        {
            return await command.FetchDataAsync<T>(
                behavior,
                FetchMode.Default,
                CancellationToken.None,
                parameters);
        }

        /// <summary>
        /// 非同步版本的 <see cref="FetchData{T}(SqlCommand, object[])"/>。
        /// 執行查詢，並傳回查詢所傳回的結果集序列。
        /// <para>
        /// 查詢結果的資料型別若為一般值型別，會直接以值回傳序列。
        /// 型別若為物件，則會以查詢結果集的資料欄位名稱，對映物件中可寫入的屬性。
        /// 物件屬性若有設定 <see cref="DbColumnNameAttribute"/> 時，會以此欄位名稱做為對映，
        /// 若有多個屬性都有設定相同名稱的 <see cref="DbColumnNameAttribute"/>，
        /// 則單一欄位會對映到複數物件屬性。
        /// 如果沒有符合的 <see cref="DbColumnNameAttribute"/>，則會使用屬性名稱對映。
        /// <para>
        /// 會以 <see cref="SqlCommand.Parameters"/> 呼叫
        /// <see cref="AddWithValues(SqlParameterCollection, object[])"/> 以存入參數。
        /// </para>
        /// </para>
        /// </summary>
        /// <typeparam name="T">查詢結果的資料型別。</typeparam>
        /// <param name="command"><see cref="SqlCommand"/> 物件。</param>
        /// <param name="fetchMode"></param>
        /// <param name="parameters">用於查詢的參數物件。</param>
        /// <returns>查詢結果序列。</returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="MissingMethodException"></exception>
        /// <exception cref="InvalidCastException"></exception>
        /// <exception cref="SqlException"></exception>
        /// <exception cref="System.IO.IOException"></exception>
        /// <exception cref="InvalidOperationException"></exception>
        /// <exception cref="ObjectDisposedException"></exception>
        public static async Task<IEnumerable<T>> FetchDataAsync<T>(
            this SqlCommand command,
            FetchMode fetchMode,
            params object[] parameters)
        {
            return await command.FetchDataAsync<T>(
                CommandBehavior.Default,
                fetchMode,
                CancellationToken.None,
                parameters);
        }

        /// <summary>
        /// 非同步版本的 <see cref="FetchData{T}(SqlCommand, object[])"/>。
        /// 執行查詢，並傳回查詢所傳回的結果集序列。
        /// <para>
        /// 查詢結果的資料型別若為一般值型別，會直接以值回傳序列。
        /// 型別若為物件，則會以查詢結果集的資料欄位名稱，對映物件中可寫入的屬性。
        /// 物件屬性若有設定 <see cref="DbColumnNameAttribute"/> 時，會以此欄位名稱做為對映，
        /// 若有多個屬性都有設定相同名稱的 <see cref="DbColumnNameAttribute"/>，
        /// 則單一欄位會對映到複數物件屬性。
        /// 如果沒有符合的 <see cref="DbColumnNameAttribute"/>，則會使用屬性名稱對映。
        /// <para>
        /// 會以 <see cref="SqlCommand.Parameters"/> 呼叫
        /// <see cref="AddWithValues(SqlParameterCollection, object[])"/> 以存入參數。
        /// </para>
        /// </para>
        /// </summary>
        /// <typeparam name="T">查詢結果的資料型別。</typeparam>
        /// <param name="command"><see cref="SqlCommand"/> 物件。</param>
        /// <param name="behavior">其中一個 <see cref="CommandBehavior"/> 值。</param>
        /// <param name="fetchMode"></param>
        /// <param name="parameters">用於查詢的參數物件。</param>
        /// <returns>查詢結果序列。</returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="MissingMethodException"></exception>
        /// <exception cref="InvalidCastException"></exception>
        /// <exception cref="SqlException"></exception>
        /// <exception cref="System.IO.IOException"></exception>
        /// <exception cref="InvalidOperationException"></exception>
        /// <exception cref="ObjectDisposedException"></exception>
        public static async Task<IEnumerable<T>> FetchDataAsync<T>(
            this SqlCommand command,
            CommandBehavior behavior,
            FetchMode fetchMode,
            params object[] parameters)
        {
            return await command.FetchDataAsync<T>(
                behavior,
                fetchMode,
                CancellationToken.None,
                parameters);
        }

        /// <summary>
        /// 非同步版本的 <see cref="FetchData{T}(SqlCommand, object[])"/>。
        /// 執行查詢，並傳回查詢所傳回的結果集序列。
        /// 取消語彙基元可用於要求在命令逾時之前取消作業。
        /// <para>
        /// 查詢結果的資料型別若為一般值型別，會直接以值回傳序列。
        /// 型別若為物件，則會以查詢結果集的資料欄位名稱，對映物件中可寫入的屬性。
        /// 物件屬性若有設定 <see cref="DbColumnNameAttribute"/> 時，會以此欄位名稱做為對映，
        /// 若有多個屬性都有設定相同名稱的 <see cref="DbColumnNameAttribute"/>，
        /// 則單一欄位會對映到複數物件屬性。
        /// 如果沒有符合的 <see cref="DbColumnNameAttribute"/>，則會使用屬性名稱對映。
        /// <para>
        /// 會以 <see cref="SqlCommand.Parameters"/> 呼叫
        /// <see cref="AddWithValues(SqlParameterCollection, object[])"/> 以存入參數。
        /// </para>
        /// </para>
        /// </summary>
        /// <typeparam name="T">查詢結果的資料型別。</typeparam>
        /// <param name="command"><see cref="SqlCommand"/> 物件。</param>
        /// <param name="cancellationToken">取消指令。</param>
        /// <param name="parameters">用於查詢的參數物件。</param>
        /// <returns>查詢結果序列。</returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="MissingMethodException"></exception>
        /// <exception cref="InvalidCastException"></exception>
        /// <exception cref="SqlException"></exception>
        /// <exception cref="System.IO.IOException"></exception>
        /// <exception cref="InvalidOperationException"></exception>
        /// <exception cref="ObjectDisposedException"></exception>
        public static async Task<IEnumerable<T>> FetchDataAsync<T>(
            this SqlCommand command,
            CancellationToken cancellationToken,
            params object[] parameters)
        {
            return await command.FetchDataAsync<T>(
                CommandBehavior.Default,
                FetchMode.Default,
                cancellationToken,
                parameters);
        }

        /// <summary>
        /// 非同步版本的 <see cref="FetchData{T}(SqlCommand, object[])"/>。
        /// 執行查詢，並傳回查詢所傳回的結果集序列。
        /// 取消語彙基元可用於要求在命令逾時之前取消作業。
        /// <para>
        /// 查詢結果的資料型別若為一般值型別，會直接以值回傳序列。
        /// 型別若為物件，則會以查詢結果集的資料欄位名稱，對映物件中可寫入的屬性。
        /// 物件屬性若有設定 <see cref="DbColumnNameAttribute"/> 時，會以此欄位名稱做為對映，
        /// 若有多個屬性都有設定相同名稱的 <see cref="DbColumnNameAttribute"/>，
        /// 則單一欄位會對映到複數物件屬性。
        /// 如果沒有符合的 <see cref="DbColumnNameAttribute"/>，則會使用屬性名稱對映。
        /// <para>
        /// 會以 <see cref="SqlCommand.Parameters"/> 呼叫
        /// <see cref="AddWithValues(SqlParameterCollection, object[])"/> 以存入參數。
        /// </para>
        /// </para>
        /// </summary>
        /// <typeparam name="T">查詢結果的資料型別。</typeparam>
        /// <param name="command"><see cref="SqlCommand"/> 物件。</param>
        /// <param name="behavior"></param>
        /// <param name="cancellationToken">取消指令。</param>
        /// <param name="parameters">用於查詢的參數物件。</param>
        /// <returns>查詢結果序列。</returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="MissingMethodException"></exception>
        /// <exception cref="InvalidCastException"></exception>
        /// <exception cref="SqlException"></exception>
        /// <exception cref="System.IO.IOException"></exception>
        /// <exception cref="InvalidOperationException"></exception>
        /// <exception cref="ObjectDisposedException"></exception>
        public static async Task<IEnumerable<T>> FetchDataAsync<T>(
            this SqlCommand command,
            CommandBehavior behavior,
            CancellationToken cancellationToken,
            params object[] parameters)
        {
            return await command.FetchDataAsync<T>(
                behavior,
                FetchMode.Default,
                cancellationToken,
                parameters);
        }

        /// <summary>
        /// 非同步版本的 <see cref="FetchData{T}(SqlCommand, object[])"/>。
        /// 執行查詢，並傳回查詢所傳回的結果集序列。
        /// 取消語彙基元可用於要求在命令逾時之前取消作業。
        /// <para>
        /// 查詢結果的資料型別若為一般值型別，會直接以值回傳序列。
        /// 型別若為物件，則會以查詢結果集的資料欄位名稱，對映物件中可寫入的屬性。
        /// 物件屬性若有設定 <see cref="DbColumnNameAttribute"/> 時，會以此欄位名稱做為對映，
        /// 若有多個屬性都有設定相同名稱的 <see cref="DbColumnNameAttribute"/>，
        /// 則單一欄位會對映到複數物件屬性。
        /// 如果沒有符合的 <see cref="DbColumnNameAttribute"/>，則會使用屬性名稱對映。
        /// <para>
        /// 會以 <see cref="SqlCommand.Parameters"/> 呼叫
        /// <see cref="AddWithValues(SqlParameterCollection, object[])"/> 以存入參數。
        /// </para>
        /// </para>
        /// </summary>
        /// <typeparam name="T">查詢結果的資料型別。</typeparam>
        /// <param name="command"><see cref="SqlCommand"/> 物件。</param>
        /// <param name="fetchMode"></param>
        /// <param name="cancellationToken">取消指令。</param>
        /// <param name="parameters">用於查詢的參數物件。</param>
        /// <returns>查詢結果序列。</returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="MissingMethodException"></exception>
        /// <exception cref="InvalidCastException"></exception>
        /// <exception cref="SqlException"></exception>
        /// <exception cref="System.IO.IOException"></exception>
        /// <exception cref="InvalidOperationException"></exception>
        /// <exception cref="ObjectDisposedException"></exception>
        public static async Task<IEnumerable<T>> FetchDataAsync<T>(
            this SqlCommand command,
            FetchMode fetchMode,
            CancellationToken cancellationToken,
            params object[] parameters)
        {
            return await command.FetchDataAsync<T>(
                CommandBehavior.Default,
                fetchMode,
                cancellationToken,
                parameters);
        }

        /// <summary>
        /// 非同步版本的 <see cref="FetchData{T}(SqlCommand, object[])"/>。
        /// 執行查詢，並傳回查詢所傳回的結果集序列。
        /// 取消語彙基元可用於要求在命令逾時之前取消作業。
        /// <para>
        /// 查詢結果的資料型別若為一般值型別，會直接以值回傳序列。
        /// 型別若為物件，則會以查詢結果集的資料欄位名稱，對映物件中可寫入的屬性。
        /// 物件屬性若有設定 <see cref="DbColumnNameAttribute"/> 時，會以此欄位名稱做為對映，
        /// 若有多個屬性都有設定相同名稱的 <see cref="DbColumnNameAttribute"/>，
        /// 則單一欄位會對映到複數物件屬性。
        /// 如果沒有符合的 <see cref="DbColumnNameAttribute"/>，則會使用屬性名稱對映。
        /// <para>
        /// 會以 <see cref="SqlCommand.Parameters"/> 呼叫
        /// <see cref="AddWithValues(SqlParameterCollection, object[])"/> 以存入參數。
        /// </para>
        /// </para>
        /// </summary>
        /// <typeparam name="T">查詢結果的資料型別。</typeparam>
        /// <param name="command"><see cref="SqlCommand"/> 物件。</param>
        /// <param name="behavior">其中一個 <see cref="CommandBehavior"/> 值。</param>
        /// <param name="fetchMode"></param>
        /// <param name="cancellationToken">取消指令。</param>
        /// <param name="parameters">用於查詢的參數物件。</param>
        /// <returns>查詢結果序列。</returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="MissingMethodException"></exception>
        /// <exception cref="InvalidCastException"></exception>
        /// <exception cref="SqlException"></exception>
        /// <exception cref="System.IO.IOException"></exception>
        /// <exception cref="InvalidOperationException"></exception>
        /// <exception cref="ObjectDisposedException"></exception>
        public static async Task<IEnumerable<T>> FetchDataAsync<T>(
            this SqlCommand command,
            CommandBehavior behavior,
            FetchMode fetchMode,
            CancellationToken cancellationToken,
            params object[] parameters)
        {
            return await s_core.FetchDataAsync<T>(
                command,
                behavior,
                fetchMode,
                cancellationToken,
                parameters);
        }
    }
}
