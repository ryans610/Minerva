using System;
using System.Data;

namespace RyanJuan.Minerva
{
    /// <summary>
    /// Specifies the database type of the corresponding parameter or column.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, Inherited = true, AllowMultiple = false)]
    public class DbTypeAttribute : Attribute
    {
        /// <summary>
        /// Specifies the database type of the corresponding parameter or column.
        /// </summary>
        /// <param name="dbType">One of the <see cref="DbType"/> value.</param>
        public DbTypeAttribute(DbType dbType)
        {
            DBType = dbType;
        }

        /// <summary>
        /// Specifies the database type of the corresponding parameter or column.
        /// </summary>
        /// <param name="dbType">One of the <see cref="DbType"/> value.</param>
        /// <param name="size">Length limit of the parameter.</param>
        /// <exception cref="ArgumentException">
        /// Value of <paramref name="size"/> is not greater than zero.
        /// </exception>
        public DbTypeAttribute(DbType dbType, int size)
            : this(dbType)
        {
            if (size <= 0)
            {
                throw new ArgumentException(
                    $"Argument '{nameof(size)}' must be greater than 0.",
                    nameof(size));
            }
            Size = size;
        }

#if ZH_HANT
        /// <summary>
        /// 資料庫欄位的型別。
        /// </summary>
#else
        /// <summary>
        /// Database type.
        /// </summary>
#endif
        public DbType DBType { get; }

        /// <summary>
        /// Length limit of the parameter.
        /// </summary>
        public int? Size { get; } = null;

        /// <summary>
        /// (Optional) Whether this parameter allow <see langword="null"/> or not.
        /// The default value is <see langword="false"/>.
        /// </summary>
        public bool AllowNull { get; set; } = false;
    }
}
