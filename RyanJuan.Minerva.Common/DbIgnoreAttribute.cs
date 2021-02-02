using System;
using System.Collections.Generic;
using System.Text;

namespace RyanJuan.Minerva.Common
{
    /// <summary>
    /// 
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, Inherited = true, AllowMultiple = false)]
    public sealed class DbIgnoreAttribute : Attribute
    {
        /// <summary>
        /// 
        /// </summary>
        public DbIgnoreAttribute() : this(DbIgnoreOption.IgnoreBoth) { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ignoreOption"></param>
        public DbIgnoreAttribute(DbIgnoreOption ignoreOption)
        {
            IgnoreOption = ignoreOption;
        }

        /// <summary>
        /// 
        /// </summary>
        public DbIgnoreOption IgnoreOption { get; }
    }

    /// <summary>
    /// 
    /// </summary>
    [Flags]
    public enum DbIgnoreOption : byte
    {
        /// <summary>
        /// 
        /// </summary>
        IgnoreColumn = 0b_0001,
        /// <summary>
        /// 
        /// </summary>
        IgnoreParameter = 0b_0010,
        /// <summary>
        /// 
        /// </summary>
        IgnoreBoth = IgnoreColumn | IgnoreParameter,
    }
}
