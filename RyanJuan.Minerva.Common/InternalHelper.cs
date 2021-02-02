using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace RyanJuan.Minerva.Common
{
    internal static class InternalHelper
    {
        /// <summary>
        /// Mapping table for <see cref="Type"/> and <see cref="DbType"/>.
        /// </summary>
        internal static Dictionary<Type, DbType> DBTypeMap { get; } = new Dictionary<Type, DbType>
        {
            [typeof(byte)] = DbType.Byte,
            [typeof(sbyte)] = DbType.SByte,
            [typeof(short)] = DbType.Int16,
            [typeof(ushort)] = DbType.UInt16,
            [typeof(int)] = DbType.Int32,
            [typeof(uint)] = DbType.UInt32,
            [typeof(long)] = DbType.Int64,
            [typeof(ulong)] = DbType.UInt64,
            [typeof(float)] = DbType.Single,
            [typeof(double)] = DbType.Double,
            [typeof(decimal)] = DbType.Decimal,
            [typeof(bool)] = DbType.Boolean,
            [typeof(string)] = DbType.String,
            [typeof(char)] = DbType.StringFixedLength,
            [typeof(Guid)] = DbType.Guid,
            [typeof(DateTime)] = DbType.DateTime,
            [typeof(DateTimeOffset)] = DbType.DateTimeOffset,
            [typeof(byte[])] = DbType.Binary,
            [typeof(byte?)] = DbType.Byte,
            [typeof(sbyte?)] = DbType.SByte,
            [typeof(short?)] = DbType.Int16,
            [typeof(ushort?)] = DbType.UInt16,
            [typeof(int?)] = DbType.Int32,
            [typeof(uint?)] = DbType.UInt32,
            [typeof(long?)] = DbType.Int64,
            [typeof(ulong?)] = DbType.UInt64,
            [typeof(float?)] = DbType.Single,
            [typeof(double?)] = DbType.Double,
            [typeof(decimal?)] = DbType.Decimal,
            [typeof(bool?)] = DbType.Boolean,
            [typeof(char?)] = DbType.StringFixedLength,
            [typeof(Guid?)] = DbType.Guid,
            [typeof(DateTime?)] = DbType.DateTime,
            [typeof(DateTimeOffset?)] = DbType.DateTimeOffset,
        };

        internal static T GetValueOrDefault<T>(
            object value)
        {
            return value == DBNull.Value ? default : (T)value;
        }

        internal static LinkedList<PropertyInfo>[] GetBindingPropertiesOfType(
            this DbDataReader reader,
            Type type)
        {
            var allProperties = ReflectionCenter.GetPropertiesForColumn(type);
            var properties = new LinkedList<PropertyInfo>[reader.FieldCount];
            for (int i = 0; i < reader.FieldCount; i += 1)
            {
                properties[i] = new LinkedList<PropertyInfo>();
                string fieldName = reader.GetName(i);
                var set = false;
                foreach (var property in allProperties.Where(p =>
                     p.GetCustomAttribute<DbColumnNameAttribute>()?.Name == fieldName))
                {
                    properties[i].AddLast(property);
                    set = true;
                }
                if (!set)
                {
                    var property = allProperties.FirstOrDefault(p => p.Name == fieldName);
                    if (property is { })
                    {
                        properties[i].AddLast(property);
                    }
                }
            }
            return properties;
        }

        internal static T GetValueAsT<T>(
            this DbDataReader reader,
            bool isObjectType,
            LinkedList<PropertyInfo>[] properties = null)
        {
            if (isObjectType)
            {
                Error.ThrowIfArgumentNull(nameof(properties), properties);
                var instance = ReflectionCenter.CreateInstance<T>();
                for (int i = 0; i < reader.FieldCount; i += 1)
                {
                    var propertiesForField = properties[i];
                    if (propertiesForField.Count == 0)
                    {
                        continue;
                    }
                    var value = reader.GetValue(i);
                    if (value != DBNull.Value)
                    {
                        if (propertiesForField.Count == 1)
                        {
                            propertiesForField.First.Value.SetValue(instance, value);
                        }
                        else
                        {
                            foreach (var property in propertiesForField)
                            {
                                property.SetValue(instance, value);
                            }
                        }
                    }
                }
                return instance;
            }
            else
            {
                return GetValueOrDefault<T>(reader.GetValue(0));
            }
        }
    }
}
