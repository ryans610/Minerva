using System;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Reflection;

namespace RyanJuan.Minerva.Common
{
    public sealed partial class MinervaCore
    {
        /// <summary>
        /// Adds the new instance of <see cref="DbParameter"/> to
        /// <see cref="DbParameterCollection"/> which the <see cref="DbParameter"/> is
        /// constructed using properties of the input objects.
        /// <para>
        /// When constructing <see cref="DbParameter"/> instances, the value of
        /// <see cref="DbParameter.ParameterName"/> will be set from properties of input
        /// <paramref name="parameters"/> object.
        /// If the property has <see cref="DbParameterNameAttribute"/>,
        /// <see cref="DbParameterNameAttribute.Name"/> will be used as parameter name.
        /// If the property has multiple <see cref="DbParameterNameAttribute"/>, multiple
        /// <see cref="DbParameter"/> instance will be create.
        /// Aslo if the property has <see cref="DbColumnNameAttribute"/> and
        /// <see cref="DbColumnNameAttribute.UseAsParameter"/> set to <see langword="true"/>,
        /// <see cref="DbColumnNameAttribute.Name"/> will be used as parameter name to create
        /// an additional <see cref="DbParameter"/> instance.
        /// At last, if none of the above condition are met, the property name will be used as
        /// parameter name.
        /// </para>
        /// <para>
        /// If parameter with the same name has already exists in
        /// <see cref="DbParameterCollection"/>, that property will be ignore.
        /// </para>
        /// <para>
        /// After <see cref="DbParameter"/> instance is created, parameter type will be set
        /// before the value is set.
        /// If the property has <see cref="DbTypeAttribute"/>,
        /// <see cref="DbTypeAttribute.DBType"/> will be used as parameter type, otherwise
        /// this method will take the property's type and convert to corresponding
        /// <see cref="DbType"/>.
        /// </para>
        /// <para>
        /// The <see cref="ArgumentException"/> will be thrown if the <see cref="DbType"/>
        /// of parameter is not valid.
        /// </para>
        /// </summary>
        /// <param name="collection">Instance of <see cref="DbParameterCollection"/>.</param>
        /// <param name="parameters">The objects which use to construct parameter.</param>
        /// <returns>Instance of <see cref="DbParameterCollection"/>.</returns>
        /// <exception cref="ArgumentNullException">
        /// Parameter <paramref name="collection"/> is null.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// <see cref="DbType"/> of parameter is not valid.
        /// </exception>
        public DbParameterCollection AddWithValues(
            DbParameterCollection collection,
            params object[] parameters)
        {
            if (collection is null)
            {
                throw Error.ArgumentNull(nameof(collection));
            }
            foreach (var obj in parameters)
            {
                foreach (var property in obj
                    .GetType()
                    .GetInstanceProperties()
                    .Where(p => p.CanRead))
                {
                    try
                    {
                        var dbtype = property.GetCustomAttribute<DbTypeAttribute>();
                        var type = dbtype is null ? GetDBType(property.PropertyType) : dbtype.DBType;
                        var column = property.GetCustomAttribute<DbColumnNameAttribute>();
                        var value = property.GetValue(obj);
                        var added = false;
                        foreach (var parameter in property
                            .GetCustomAttributes<DbParameterNameAttribute>()
                            .Where(x => !string.IsNullOrEmpty(x.Name)))
                        {
                            AddParameterValue(
                                collection,
                                parameter.Name,
                                value,
                                type,
                                dbtype?.Size);
                            added = true;
                        }
                        if (column?.UseAsParameter ?? false &&
                            !string.IsNullOrEmpty(column.Name))
                        {
                            AddParameterValue(
                                collection,
                                column.Name,
                                value,
                                type,
                                dbtype?.Size);
                            added = true;
                        }
                        if (!added)
                        {
                            AddParameterValue(
                                collection,
                                property.Name,
                                value,
                                type,
                                dbtype?.Size);
                        }
                    }
                    catch (ArgumentException ex)
                    {
                        throw Error.ArgumentException(nameof(parameters), ex);
                    }
                }
            }
            return collection;
        }

        /// <summary>
        /// Adds a <see cref="DbParameter"/> into <see cref="DbParameterCollection"/>.
        /// </summary>
        /// <param name="collection">Instance of <see cref="DbParameterCollection"/>.</param>
        /// <param name="name">Name of parameter.</param>
        /// <param name="value">Value of parameter.</param>
        /// <param name="type">Database type of parameter.</param>
        /// <param name="size">(Optional) Lenght limit for column in database.</param>
        private void AddParameterValue(
            DbParameterCollection collection,
            string name,
            object value,
            DbType type,
            int? size = null)
        {
            if (collection.Contains(name))
            {
                return;
            }
            if (value is null)
            {
                value = DBNull.Value;
            }
            var parameter = _factory.CreateParameter();
            parameter.ParameterName = name;
            parameter.DbType = type;
            if (size.HasValue)
            {
                parameter.Size = size.Value;
            }
            parameter.Value = value;
            collection.Add(parameter);
        }
    }
}
