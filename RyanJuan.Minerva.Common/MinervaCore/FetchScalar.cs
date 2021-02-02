using System;
using System.Data;
using System.Data.Common;
using System.Threading;
using System.Threading.Tasks;

using static RyanJuan.Minerva.Common.InternalHelper;

namespace RyanJuan.Minerva.Common
{
    public sealed partial class MinervaCore
    {
        /// <summary>
        /// Executes the query and returns the first column of the first row in the result set
        /// returned by the query. All other columns and rows are ignored.
        /// <para>
        /// This method will call
        /// <see cref="AddWithValues(DbParameterCollection, object[])"/>
        /// to add parameters into <see cref="DbCommand.Parameters"/>.
        /// </para>
        /// </summary>
        /// <typeparam name="T">The type of the result.</typeparam>
        /// <param name="command">Instance of <see cref="DbCommand"/>.</param>
        /// <param name="parameters">The objects which use to construct parameter.</param>
        /// <returns>The first column of the first row in the result set.</returns>
        /// <exception cref="ArgumentNullException">
        /// Parameter <paramref name="command"/> is null.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// <see cref="DbType"/> of parameter is not valid.
        /// </exception>
        public T FetchScalar<T>(
            DbCommand command,
            params object[] parameters)
        {
            Error.ThrowIfArgumentNull(nameof(command), command);
            AddWithValues(command.Parameters, parameters);
            return GetValueOrDefault<T>(command.ExecuteScalar());
        }

        /// <summary>
        /// This is the asynchronous version of
        /// <see cref="FetchScalar{T}(DbCommand, object[])"/>.
        /// Executes the query and returns the first column of the first row in the result set
        /// returned by the query. All other columns and rows are ignored.
        /// The cancellation token can be used to request that the operation be abandoned
        /// before the command timeout elapses.
        /// <para>
        /// This method will call
        /// <see cref="AddWithValues(DbParameterCollection, object[])"/>
        /// to add parameters into <see cref="DbCommand.Parameters"/>.
        /// </para>
        /// </summary>
        /// <typeparam name="T">The type of the result.</typeparam>
        /// <param name="command">Instance of <see cref="DbCommand"/>.</param>
        /// <param name="cancellationToken">
        /// The token to monitor for cancellation requests.
        /// </param>
        /// <param name="parameters">The objects which use to construct parameter.</param>
        /// <returns>The first column of the first row in the result set.</returns>
        /// <exception cref="ArgumentNullException">
        /// Parameter <paramref name="command"/> is null.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// <see cref="DbType"/> of parameter is not valid.
        /// </exception>
        public async Task<T> FetchScalarAsync<T>(
            DbCommand command,
            CancellationToken cancellationToken,
            params object[] parameters)
        {
            Error.ThrowIfArgumentNull(nameof(command), command);
            AddWithValues(command.Parameters, parameters);
            return GetValueOrDefault<T>(await command.ExecuteScalarAsync(cancellationToken));
        }
    }
}
