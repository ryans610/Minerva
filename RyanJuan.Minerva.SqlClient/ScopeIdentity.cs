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
        private static readonly string s_sqlScopeIdentity = "select Scope_Identity()";

        /// <summary>
        /// 
        /// </summary>
        /// <param name="connection"></param>
        /// <returns></returns>
        public static decimal ScopeIdentityDecimal(
            this SqlConnection connection)
        {
            using (var command = connection.CreateCommand())
            {
                command.CommandText = s_sqlScopeIdentity;
                return command.FetchScalar<decimal>();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="connection"></param>
        /// <returns></returns>
        public static async Task<decimal> ScopeIdentityDecimalAsync(
            this SqlConnection connection)
        {
            return await connection.ScopeIdentityDecimalAsync(CancellationToken.None);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public static async Task<decimal> ScopeIdentityDecimalAsync(
            this SqlConnection connection,
            CancellationToken cancellationToken)
        {
            using (var command = connection.CreateCommand())
            {
                command.CommandText = s_sqlScopeIdentity;
                return await command.FetchScalarAsync<decimal>(cancellationToken);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="connection"></param>
        /// <returns></returns>
        public static int ScopeIdentityInt(
            this SqlConnection connection)
        {
            return (int)connection.ScopeIdentityDecimal();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="connection"></param>
        /// <returns></returns>
        public static async Task<int> ScopeIdentityIntAsync(
            this SqlConnection connection)
        {
            return await connection.ScopeIdentityIntAsync(CancellationToken.None);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public static async Task<int> ScopeIdentityIntAsync(
            this SqlConnection connection,
            CancellationToken cancellationToken)
        {
            return (int)await connection.ScopeIdentityDecimalAsync(cancellationToken);
        }
    }
}
