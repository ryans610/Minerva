using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace RyanJuan.Minerva.Common
{
    public sealed partial class MinervaCore
    {
        /// <summary>
        /// Execute the <see cref="DbCommand.CommandText"/> against the
        /// <see cref="DbCommand.Connection"/>, and return the single data of result.
        /// If the result contains more than one data, an
        /// <see cref="InvalidOperationException"/> will be throw.
        /// <para>
        /// If the type <typeparamref name="T"/> is class, properties will be mapping to columns
        /// by <see cref="DbColumnNameAttribute.Name"/>.
        /// If multiple properties have the same <see cref="DbColumnNameAttribute.Name"/>, then
        /// that column will be mapping into multiple properties.
        /// For those properties which does not have <see cref="DbColumnNameAttribute"/>,
        /// property's name will be used.
        /// </para>
        /// <para>
        /// This method will call
        /// <see cref="AddWithValues(DbParameterCollection, object[])"/>
        /// to add parameters into <see cref="DbCommand.Parameters"/>.
        /// </para>
        /// </summary>
        /// <typeparam name="T">The type of the result.</typeparam>
        /// <param name="command">Instance of <see cref="DbCommand"/>.</param>
        /// <param name="parameters">The objects which use to construct parameter.</param>
        /// <returns>Enumerable of results.</returns>
        /// <exception cref="ArgumentNullException">
        /// Parameter <paramref name="command"/> is null.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// <see cref="DbType"/> of parameter is not valid.
        /// </exception>
        /// <exception cref="InvalidOperationException">
        /// The data contains more than one row.
        /// </exception>
        public T FetchSingle<T>(
            DbCommand command,
            params object[] parameters)
        {
            if (command is null)
            {
                throw Error.ArgumentNull(nameof(command));
            }
            var list = FetchData<T>(
                command,
                CommandBehavior.Default,
                FetchMode.Stream,
                parameters);
            return FetchSinglePrivate(list);
        }

        /// <summary>
        /// This is the asynchronous version of
        /// <see cref="FetchSingle{T}(DbCommand, object[])"/>.
        /// Execute the <see cref="DbCommand.CommandText"/> against the
        /// <see cref="DbCommand.Connection"/>, and return the single data of result.
        /// If the result contains more than one data, an
        /// <see cref="InvalidOperationException"/> will be throw.
        /// The cancellation token can be used to request that the operation be abandoned
        /// before the command timeout elapses.
        /// <para>
        /// If the type <typeparamref name="T"/> is class, properties will be mapping to columns
        /// by <see cref="DbColumnNameAttribute.Name"/>.
        /// If multiple properties have the same <see cref="DbColumnNameAttribute.Name"/>, then
        /// that column will be mapping into multiple properties.
        /// For those properties which does not have <see cref="DbColumnNameAttribute"/>,
        /// property's name will be used.
        /// </para>
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
        /// <returns>Enumerable of results.</returns>
        /// <exception cref="ArgumentNullException">
        /// Parameter <paramref name="command"/> is null.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// <see cref="DbType"/> of parameter is not valid.
        /// </exception>
        /// <exception cref="InvalidOperationException">
        /// The data contains more than one row.
        /// </exception>
        public async Task<T> FetchSingleAsync<T>(
            DbCommand command,
            CancellationToken cancellationToken,
            params object[] parameters)
        {
            if (command is null)
            {
                throw Error.ArgumentNull(nameof(command));
            }
            var list = await FetchDataAsync<T>(
                command,
                CommandBehavior.Default,
                FetchMode.Stream,
                cancellationToken,
                parameters);
            return FetchSinglePrivate(list);
        }

        private T FetchSinglePrivate<T>(IEnumerable<T> list)
        {
            using (list as IDisposable)
            {
                using (var iterator = list.GetEnumerator())
                {
                    if (!iterator.MoveNext())
                    {
                        throw Error.NoElements();
                    }
                    var result = iterator.Current;
                    if (iterator.MoveNext())
                    {
                        throw Error.MoreThanOneElement();
                    }
                    return result;
                }
            }
        }
    }
}
