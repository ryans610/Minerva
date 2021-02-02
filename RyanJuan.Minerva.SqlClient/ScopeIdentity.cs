﻿using System;
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
        private const string SqlScopeIdentity = @"select Scope_Identity()";

        /// <summary>
        /// 
        /// </summary>
        /// <param name="connection"></param>
        /// <returns></returns>
        public static decimal ScopeIdentityDecimal(
            this SqlConnection connection)
        {
            using var command = connection.CreateCommand();
            command.CommandText = SqlScopeIdentity;
            return command.FetchScalar<decimal>();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="connection"></param>
        /// <returns></returns>
        public static Task<decimal> ScopeIdentityDecimalAsync(
            this SqlConnection connection)
        {
            return connection.ScopeIdentityDecimalAsync(CancellationToken.None);
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
            using var command = connection.CreateCommand();
            command.CommandText = SqlScopeIdentity;
            return await command.FetchScalarAsync<decimal>(cancellationToken);
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
        public static Task<int> ScopeIdentityIntAsync(
            this SqlConnection connection)
        {
            return connection.ScopeIdentityIntAsync(CancellationToken.None);
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
