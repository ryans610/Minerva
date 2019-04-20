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
        /// 所有 <see cref="SqlParameter"/> 允許的 <see cref="DbType"/>。
        /// </summary>
        public static DbType[] ValidDbTypes
        {
            get
            {
                if (s_validDbTypes is null)
                {
                    s_validDbTypes = s_core.GetValidDbTypes().ToArray();
                }
                return s_validDbTypes;
            }
        }

        /// <summary>
        /// <see cref="ValidDbTypes"/> 底層的儲存陣列。
        /// </summary>
        private static DbType[] s_validDbTypes = null;
    }
}
