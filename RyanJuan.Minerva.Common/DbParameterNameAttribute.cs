using System;

namespace RyanJuan.Minerva
{
    /// <summary>
    /// Specifies the name of the corresponding parameter.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, Inherited = false, AllowMultiple = true)]
    public sealed class DbParameterNameAttribute : Attribute
    {
        /// <summary>
        /// Specifies the name of the corresponding parameter.
        /// </summary>
        /// <param name="name">Parameter's name.</param>
        public DbParameterNameAttribute(string name)
        {
            Name = name;
        }

        /// <summary>
        /// Parameter's name.
        /// </summary>
        public string Name { get; }
    }
}
