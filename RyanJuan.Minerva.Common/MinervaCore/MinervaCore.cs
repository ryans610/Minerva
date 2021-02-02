using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;

using static RyanJuan.Minerva.Common.InternalHelper;

namespace RyanJuan.Minerva.Common
{
    /// <summary>
    /// Core project for RyanJuan.Minerva.
    /// <para>
    /// Should not be used directly, use other subset of RyanJuan.Minerva which implement for
    /// various database instead, such as RyanJuan.Minerva.SqlClient,
    /// RyanJuan.Minerva.Odbc etc.
    /// </para>
    /// </summary>
    public sealed partial class MinervaCore
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="factory"></param>
        public MinervaCore(DbProviderFactory factory)
        {
            Error.ThrowIfArgumentNull(nameof(factory), factory);
            _factory = factory;
        }

        private readonly DbProviderFactory _factory = null;

        private FetchMode _defaultFetchMode = FetchMode.Buffer;

        /// <summary>
        /// Gets all <see cref="DbType"/> which is valid for implement of
        /// <see cref="DbParameter"/>..
        /// </summary>
        /// <returns>Enumerable of all valid <see cref="DbType"/>.</returns>
        public IEnumerable<DbType> GetValidDbTypes()
        {
            var parameter = _factory.CreateParameter();
            foreach (DbType type in Enum.GetValues(typeof(DbType)))
            {
                try
                {
                    parameter.DbType = type;
                }
                catch(Exception)
                {
                    continue;
                }
                yield return type;
            }
        }

        /// <summary>
        /// Gets the corresponding <see cref="DbType"/> from <see cref="Type"/>.
        /// If none of the <see cref="DbType"/> is match, <paramref name="defaultDbType"/>
        /// will be returned (default value is <see cref="DbType.String"/>).
        /// </summary>
        /// <param name="type">Specified <see cref="Type"/>.</param>
        /// <param name="defaultDbType">(Optional) Default <see cref="DbType"/>.</param>
        /// <returns>The corresponding <see cref="DbType"/>.</returns>
        public DbType GetDBType(Type type, DbType defaultDbType = DbType.String)
        {
            return DBTypeMap.TryGetValue(type, out var dbtype) ? dbtype : defaultDbType;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fetchMode"></param>
        public void SetDefaultFetchMode(FetchMode fetchMode)
        {
            _defaultFetchMode = fetchMode == FetchMode.Default ? FetchMode.Buffer : fetchMode;
        }
    }
}
