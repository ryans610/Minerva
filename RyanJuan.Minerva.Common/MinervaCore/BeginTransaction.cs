using System;
using System.Data;
using System.Data.Common;

namespace RyanJuan.Minerva.Common
{
    public sealed partial class MinervaCore
    {
        /// <summary>
        /// Starts a database transaction with the specified <see cref="DbCommand"/>
        /// and isolation level.
        /// </summary>
        /// <param name="connection">Database connection.</param>
        /// <param name="command">Specified <see cref="DbCommand"/> object.</param>
        /// <param name="isolationLevel">
        /// Specifies the isolaction level for the transaction.
        /// </param>
        /// <returns>Database transaction object.</returns>
        /// <exception cref="ArgumentNullException">
        /// Parameter <paramref name="connection"/> or <paramref name="command"/> is null.
        /// </exception>
        public DbTransaction BeginTransaction(
            DbConnection connection,
            DbCommand command,
            IsolationLevel isolationLevel)
        {
            Error.ThrowIfArgumentNull(nameof(connection), connection);
            Error.ThrowIfArgumentNull(nameof(command), command);
            var trans = connection.BeginTransaction(isolationLevel);
            command.Transaction = trans;
            return trans;
        }
    }
}
