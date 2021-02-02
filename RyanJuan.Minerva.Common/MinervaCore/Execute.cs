using System;
using System.Data;
using System.Data.Common;
using System.Threading;
using System.Threading.Tasks;

namespace RyanJuan.Minerva.Common
{
    public sealed partial class MinervaCore
    {
        /// <summary>
        /// Executes a SQL statement against a connection object.
        /// <para>
        /// This method will call
        /// <see cref="AddWithValues(DbParameterCollection, object[])"/>
        /// to add parameters into <see cref="DbCommand.Parameters"/>.
        /// </para>
        /// </summary>
        /// <param name="command">Instance of <see cref="DbCommand"/>.</param>
        /// <param name="parameters">The objects which use to construct parameter.</param>
        /// <returns>The number of rows affected.</returns>
        /// <exception cref="ArgumentNullException">
        /// Parameter <paramref name="command"/> is null.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// <see cref="DbType"/> of parameter is not valid.
        /// </exception>
        public int Execute(
            DbCommand command,
            params object[] parameters)
        {
            Error.ThrowIfArgumentNull(nameof(command), command);
            AddWithValues(command.Parameters, parameters);
            return command.ExecuteNonQuery();
        }

        /// <summary>
        /// This is the asynchronous version of
        /// <see cref="Execute(DbCommand, object[])"/>.
        /// Executes a SQL statement against a connection object.
        /// The cancellation token can be used to request that the operation be abandoned
        /// before the command timeout elapses.
        /// <para>
        /// This method will call
        /// <see cref="AddWithValues(DbParameterCollection, object[])"/>
        /// to add parameters into <see cref="DbCommand.Parameters"/>.
        /// </para>
        /// </summary>
        /// <param name="command">Instance of <see cref="DbCommand"/>.</param>
        /// <param name="cancellationToken">
        /// The token to monitor for cancellation requests.
        /// </param>
        /// <param name="parameters">The objects which use to construct parameter.</param>
        /// <returns>The number of rows affected.</returns>
        /// <exception cref="ArgumentNullException">
        /// Parameter <paramref name="command"/> is null.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// <see cref="DbType"/> of parameter is not valid.
        /// </exception>
        public Task<int> ExecuteAsync(
            DbCommand command,
            CancellationToken cancellationToken,
            params object[] parameters)
        {
            Error.ThrowIfArgumentNull(nameof(command), command);
            AddWithValues(command.Parameters, parameters);
            return command.ExecuteNonQueryAsync(cancellationToken);
        }
    }
}
