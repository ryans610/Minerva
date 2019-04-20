using System;

namespace RyanJuan.Minerva
{
    /// <summary>
    /// Specifies the name of the corresponding database column.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, Inherited = true, AllowMultiple = false)]
    public class DbColumnNameAttribute : Attribute
    {
        /// <summary>
        /// Specifies the name of the corresponding database column.
        /// </summary>
        /// <param name="name">Column's name.</param>
        public DbColumnNameAttribute(string name)
        {
            Name = name;
        }

        /// <summary>
        /// Column's name.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Whether this name will be used as parameter's name or not.
        /// </summary>
        public bool UseAsParameter { get; set; } = false;
    }
}
