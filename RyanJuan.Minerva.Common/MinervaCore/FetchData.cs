using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace RyanJuan.Minerva.Common
{
    public sealed partial class MinervaCore
    {
        private int _firstBufferSizeForFetchModeHybrid = 4096;

        /// <summary>
        /// Execute the <see cref="DbCommand.CommandText"/> against the
        /// <see cref="DbCommand.Connection"/>, and return an enumerable of type
        /// <typeparamref name="T"/> for results using one of the <see cref="CommandBehavior"/>
        /// values.
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
        /// <param name="behavior">One of the <see cref="CommandBehavior"/> values.</param>
        /// <param name="fetchMode"></param>
        /// <param name="parameters">The objects which use to construct parameter.</param>
        /// <returns>Enumerable of results.</returns>
        /// <exception cref="ArgumentNullException">
        /// Parameter <paramref name="command"/> is null.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// <see cref="DbType"/> of parameter is not valid.
        /// </exception>
        public IEnumerable<T> FetchData<T>(
            DbCommand command,
            CommandBehavior behavior,
            FetchMode fetchMode,
            params object[] parameters)
        {
            Error.ThrowIfArgumentNull(nameof(command), command);
            if (fetchMode == FetchMode.Default)
            {
                fetchMode = _defaultFetchMode;
            }
            AddWithValues(command.Parameters, parameters);
            var reader = command.ExecuteReader(behavior);
            if (reader.HasRows)
            {
                var type = typeof(T);
                var isObjectType = Type.GetTypeCode(type) == TypeCode.Object;
                LinkedList<PropertyInfo>[] properties = null;
                if (isObjectType)
                {
                    properties = reader.GetBindingPropertiesOfType(type);
                }
                switch (fetchMode)
                {
                    case FetchMode.Stream:
                        return new FetchDataStream<T>(
                           reader,
                           isObjectType,
                           properties,
                           CancellationToken.None);
                    case FetchMode.Hybrid:
                    {
                        var list = new List<T>();
                        while (reader.Read())
                        {
                            list.Add(reader.GetValueAsT<T>(isObjectType, properties));
                            if (list.Count == _firstBufferSizeForFetchModeHybrid)
                            {
                                return new FetchDataStream<T>(
                                    reader,
                                    isObjectType,
                                    properties,
                                    list,
                                    CancellationToken.None);
                            }
                        }
                        return list;
                    }
                    case FetchMode.Buffer:
                    default:
                    {
                        using (reader)
                        {
                            // FetchMode.Buffer as default
                            var list = new List<T>();
                            while (reader.Read())
                            {
                                list.Add(reader.GetValueAsT<T>(isObjectType, properties));
                            }
                            return list;
                        }
                    }
                }
            }
            return Enumerable.Empty<T>();
        }

        /// <summary>
        /// This is the asynchronous version of
        /// <see cref="FetchData{T}(DbCommand, CommandBehavior, FetchMode, object[])"/>.
        /// Execute the <see cref="DbCommand.CommandText"/> against the
        /// <see cref="DbCommand.Connection"/>, and return an enumerable of type
        /// <typeparamref name="T"/> for results using one of the <see cref="CommandBehavior"/>
        /// values.
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
        /// <param name="behavior">One of the <see cref="CommandBehavior"/> values.</param>
        /// <param name="fetchMode"></param>
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
        public async Task<IEnumerable<T>> FetchDataAsync<T>(
            DbCommand command,
            CommandBehavior behavior,
            FetchMode fetchMode,
            CancellationToken cancellationToken,
            params object[] parameters)
        {
            Error.ThrowIfArgumentNull(nameof(command), command);
            AddWithValues(command.Parameters, parameters);
            var reader = await command.ExecuteReaderAsync(behavior, cancellationToken);
            if (reader.HasRows)
            {
                var type = typeof(T);
                var isObjectType = Type.GetTypeCode(type) == TypeCode.Object;
                LinkedList<PropertyInfo>[] properties = null;
                if (isObjectType)
                {
                    properties = reader.GetBindingPropertiesOfType(type);
                }
                switch (fetchMode)
                {
                    case FetchMode.Stream:
                        return new FetchDataStream<T>(
                           reader,
                           isObjectType,
                           properties,
                           cancellationToken);
                    case FetchMode.Hybrid:
                    {
                        var list = new List<T>();
                        while (await reader.ReadAsync(cancellationToken))
                        {
                            Error.ThrowIfOperationCanceled(cancellationToken);
                            list.Add(reader.GetValueAsT<T>(isObjectType, properties));
                            if (list.Count == _firstBufferSizeForFetchModeHybrid)
                            {
                                return new FetchDataStream<T>(
                                    reader,
                                    isObjectType,
                                    properties,
                                    list,
                                    cancellationToken);
                            }
                        }
                        return list;
                    }
                    case FetchMode.Buffer:
                    default:
                    {
                        using (reader)
                        {
                            // FetchMode.Buffer as default
                            var list = new List<T>();
                            while (await reader.ReadAsync(cancellationToken))
                            {
                                Error.ThrowIfOperationCanceled(cancellationToken);
                                list.Add(reader.GetValueAsT<T>(isObjectType, properties));
                            }
                            return list;
                        }
                    }
                }
            }
            return Enumerable.Empty<T>();
        }

        internal sealed class FetchDataStream<T> : IEnumerable<T>, IEnumerable, IDisposable
        {
            /// <summary>
            /// For <see cref="FetchMode.Stream"/>.
            /// </summary>
            /// <param name="reader"></param>
            /// <param name="isObjectType"></param>
            /// <param name="properties"></param>
            /// <param name="cancellationToken"></param>
            public FetchDataStream(
                DbDataReader reader,
                bool isObjectType,
                LinkedList<PropertyInfo>[] properties,
                CancellationToken cancellationToken)
                : this(reader, isObjectType, properties, new List<T>(), cancellationToken) { }

            /// <summary>
            /// For <see cref="FetchMode.Hybrid"/>.
            /// </summary>
            /// <param name="reader"></param>
            /// <param name="isObjectType"></param>
            /// <param name="properties"></param>
            /// <param name="buffer"></param>
            /// <param name="cancellationToken"></param>
            public FetchDataStream(
                DbDataReader reader,
                bool isObjectType,
                LinkedList<PropertyInfo>[] properties,
                List<T> buffer,
                CancellationToken cancellationToken)
            {
                _reader = reader;
                _isObjectType = isObjectType;
                _properties = properties;
                _buffer = buffer;
                _lock = new ReaderWriterLockSlim();
                _cancellationToken = cancellationToken;
            }

            ~FetchDataStream()
            {
                Dispose();
            }

            private volatile bool _isDisposed = false;

            private DbDataReader _reader = null;

            private bool _isObjectType = false;

            private LinkedList<PropertyInfo>[] _properties = null;

            private List<T> _buffer = null;

            private ReaderWriterLockSlim _lock = null;

            private CancellationToken _cancellationToken;

            public void Dispose()
            {
                if (_reader != null)
                {
                    if (!_reader.IsClosed)
                    {
                        _reader.Close();
                    }
                    _reader.Dispose();
                    _reader = null;
                }
                _isObjectType = false;
                _properties = null;
                _buffer = null;
                if (_lock != null)
                {
                    _lock.Dispose();
                    _lock = null;
                }
                _cancellationToken = CancellationToken.None;
                GC.SuppressFinalize(this);
            }

            public IEnumerator<T> GetEnumerator() =>
                new Enumerator(this);

            IEnumerator IEnumerable.GetEnumerator() =>
                GetEnumerator();

            private bool MoveNext(int index)
            {
                Error.ThrowIfOperationCanceled(_cancellationToken);
                int count;
                _lock.EnterReadLock();
                try
                {
                    count = _buffer.Count;
                }
                finally
                {
                    _lock.ExitReadLock();
                }
                if (index == count)
                {
                    _lock.EnterWriteLock();
                    try
                    {
                        if (_reader.Read())
                        {
                            _buffer.Add(_reader.GetValueAsT<T>(_isObjectType, _properties));
                            return true;
                        }
                    }
                    finally
                    {
                        _lock.ExitWriteLock();
                    }
                }
                else if (index < count)
                {
                    return true;
                }
                return false;
            }

            internal struct Enumerator : IEnumerator<T>, IEnumerator, IDisposable
            {
                public Enumerator(FetchDataStream<T> stream)
                {
                    _stream = stream;
                    _current = default;
                    _index = 0;
                    _isDisposed = false;
                }

                private FetchDataStream<T> _stream;

                private T _current;

                private int _index;

                private volatile bool _isDisposed;

                public T Current
                {
                    get
                    {
                        if (_isDisposed ||
                            _stream._isDisposed)
                        {
                            throw Error.ObjectDisposed(nameof(Enumerator));
                        }
                        return _current;
                    }
                }

                object IEnumerator.Current => Current;

                public void Dispose()
                {
                    if (_isDisposed)
                    {
                        return;
                    }
                    _stream = null;
                    _current = default;
                    _index = default;
                    _isDisposed = true;
                    GC.SuppressFinalize(this);
                }

                public bool MoveNext()
                {
                    if (_isDisposed ||
                        _stream._isDisposed)
                    {
                        throw Error.ObjectDisposed(nameof(Enumerator));
                    }

                    int count;
                    _stream._lock.EnterReadLock();
                    try
                    {
                        count = _stream._buffer.Count;
                    }
                    finally
                    {
                        _stream._lock.ExitReadLock();
                    }

                    if (_index < count ||
                        _stream.MoveNext(_index))
                    {
                        _stream._lock.EnterReadLock();
                        try
                        {
                            _current = _stream._buffer[_index];
                        }
                        finally
                        {
                            _stream._lock.ExitReadLock();
                        }

                        _index += 1;
                        return true;
                    }
                    return false;
                }

                public void Reset()
                {
                    if (_isDisposed ||
                        _stream._isDisposed)
                    {
                        throw Error.ObjectDisposed(nameof(Enumerator));
                    }

                    _index = 0;
                    _current = default;
                }
            }
        }
    }
}
