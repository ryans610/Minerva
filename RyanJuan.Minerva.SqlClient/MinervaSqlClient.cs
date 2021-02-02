using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using RyanJuan.Minerva.Common;
using System.Collections.ObjectModel;

namespace RyanJuan.Minerva.SqlClientHelper
{
    /// <summary>
    /// 
    /// </summary>
    public static partial class MinervaSqlClient
    {
        static MinervaSqlClient()
        {
            s_factory = SqlClientFactory.Instance;
            s_core = new MinervaCore(s_factory);
        }

        private static readonly SqlClientFactory s_factory;

        private static readonly MinervaCore s_core;

        /// <summary>
        /// <see cref="ValidDbTypes"/> 底層的儲存陣列。
        /// </summary>
        private static ReadOnlyCollection<DbType> s_validDbTypes = null;

        /// <summary>
        /// 所有 <see cref="SqlParameter"/> 允許的 <see cref="DbType"/>。
        /// </summary>
        public static IReadOnlyCollection<DbType> ValidDbTypes =>
            s_validDbTypes ??= s_core.GetValidDbTypes().ToList().AsReadOnly();
    }
}
