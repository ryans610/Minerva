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
        /// 以傳入物件中的屬性建立對映的資料庫參數 <see cref="SqlParameter"/> 物件，
        /// 並存入 <see cref="SqlParameterCollection"/> 之中。
        /// <para>
        /// 在建立 <see cref="SqlParameter"/> 物件時，參數的名稱會自動從物件的屬性取得。
        /// 物件屬性若有設定 <see cref="DbParameterNameAttribute"/> 時，會以此參數名稱做為對映，
        /// 若物件屬性有設定多個 <see cref="DbParameterNameAttribute"/> 時，
        /// 會產生對映的複數個 <see cref="SqlParameter"/> 物件；
        /// 其次若是物件屬性有設定 <see cref="DbColumnNameAttribute"/> 且
        /// <see cref="DbColumnNameAttribute.UseAsParameter"/> 值設為 <see langword="true"/> 時，
        /// 會以欄位名稱 <see cref="DbColumnNameAttribute.Name"/> 作為參數名稱建立額外的
        /// <see cref="SqlParameter"/> 物件；
        /// 若是上述屬性都沒有設定，則會使用屬性名稱做為參數名稱。
        /// </para>
        /// <para>
        /// 如果參數名稱已經存在於 <see cref="SqlParameterCollection"/> 物件之中，
        /// 則該屬性會被忽略，保留已經存在 <see cref="SqlParameterCollection"/> 中的參數。
        /// </para>
        /// <para>
        /// 建立 <see cref="SqlParameter"/> 物件後，在存入值之前會先設定參數型別。
        /// 物件屬性若有設定 <see cref="DbTypeAttribute"/> 時，會以此設定的型別做為參數型別，
        /// 否則會以該屬性的宣告型別所對應的資料庫型別做為參數型別。
        /// </para>
        /// <para>
        /// 如果設定的參數型別不合法，則會拋出 <see cref="ArgumentException"/>。
        /// </para>
        /// </summary>
        /// <param name="collection"><see cref="SqlParameterCollection"/> 物件。</param>
        /// <param name="parameters">用於 Transact-SQL 陳述式的參數物件。</param>
        /// <returns><see cref="SqlParameterCollection"/> 物件。</returns>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="MissingMethodException"></exception>
        public static SqlParameterCollection AddWithValues(
            this SqlParameterCollection collection,
            params object[] parameters)
        {
            return s_core.AddWithValues(
                collection,
                parameters) as SqlParameterCollection;
        }
    }
}
