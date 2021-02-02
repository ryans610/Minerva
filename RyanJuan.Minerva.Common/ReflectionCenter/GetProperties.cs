using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Text;

namespace RyanJuan.Minerva.Common
{
    internal static partial class ReflectionCenter
    {
        private static readonly ConcurrentDictionary<Type, ReadOnlyCollection<PropertyInfo>> s_cachedPropertyInfosForParameter =
            new ConcurrentDictionary<Type, ReadOnlyCollection<PropertyInfo>>();

        private static readonly ConcurrentDictionary<Type, ReadOnlyCollection<PropertyInfo>> s_cachedPropertyInfosForColumn =
            new ConcurrentDictionary<Type, ReadOnlyCollection<PropertyInfo>>();

        public static ReadOnlyCollection<PropertyInfo> GetPropertiesForParameter(Type type)
        {
            if (s_cachedPropertyInfosForParameter.TryGetValue(type, out var value))
            {
                return value;
            }
            return InitialProperties(type, returnForParameter: true);
        }

        public static ReadOnlyCollection<PropertyInfo> GetPropertiesForColumn(Type type)
        {
            if (s_cachedPropertyInfosForColumn.TryGetValue(type, out var value))
            {
                return value;
            }
            return InitialProperties(type, returnForParameter: false);
        }

        private static ReadOnlyCollection<PropertyInfo> InitialProperties(
            Type type,
            bool returnForParameter)
        {
            var properties = type.GetProperties(DefaultInstanceBindingAttr);
            var parameterProperties = properties.Where(x => x.CanRead).NotIgnore(DbIgnoreOption.IgnoreParameter).ToList().AsReadOnly();
            s_cachedPropertyInfosForParameter.TryAdd(type, parameterProperties);
            var columnProperties = properties.Where(x => x.CanWrite).NotIgnore(DbIgnoreOption.IgnoreColumn).ToList().AsReadOnly();
            s_cachedPropertyInfosForColumn.TryAdd(type, columnProperties);
            return returnForParameter ? parameterProperties : columnProperties;
        }

        private static IEnumerable<PropertyInfo> NotIgnore(
            this IEnumerable<PropertyInfo> source,
            DbIgnoreOption ignoreOption)
        {
            foreach(var property in source)
            {
                var ignore = property.GetCustomAttribute<DbIgnoreAttribute>();
                if (ignore is { } &&
                    ignore.IgnoreOption.HasFlag(ignoreOption))
                {
                    continue;
                }
                yield return property;
            }
        }
    }
}
