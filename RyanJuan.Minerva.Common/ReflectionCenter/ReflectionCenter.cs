using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace RyanJuan.Minerva.Common
{
    internal static partial class ReflectionCenter
    {
        internal const BindingFlags DefaultInstanceBindingAttr =
            BindingFlags.DeclaredOnly |
            BindingFlags.Instance |
            BindingFlags.Public |
            BindingFlags.NonPublic;

        private const BindingFlags SystemDefaultBindingAttr =
            BindingFlags.Instance |
            BindingFlags.Static |
            BindingFlags.Public;

        private const BindingFlags GetAllBindingAttr =
            BindingFlags.Instance |
            BindingFlags.Static |
            BindingFlags.Public |
            BindingFlags.NonPublic;

        private readonly struct TypeStringTuple : IEquatable<TypeStringTuple>
        {
            public TypeStringTuple(
                Type type,
                string name)
            {
                Type = type;
                Name = name;
            }

            public Type Type { get; }

            public string Name { get; }

            public readonly bool Equals(TypeStringTuple other) =>
                Type == other.Type && Name == other.Name;

            public readonly override bool Equals(object obj) =>
                obj is TypeStringTuple tuple && this.Equals(tuple);

            public readonly override int GetHashCode() =>
                HashCode.Combine(Type, Name);
        }

        private readonly struct TypeBindingFlagsTuple : IEquatable<TypeBindingFlagsTuple>
        {
            public TypeBindingFlagsTuple(
                Type type,
                BindingFlags bindingAttr)
            {
                Type = type;
                BindingAttr = bindingAttr;
            }

            public Type Type { get; }

            public BindingFlags BindingAttr { get; }

            public readonly bool Equals(TypeBindingFlagsTuple other) =>
                Type == other.Type && BindingAttr == other.BindingAttr;

            public readonly override bool Equals(object obj) =>
                obj is TypeBindingFlagsTuple tuple && this.Equals(tuple);

            public readonly override int GetHashCode() =>
                HashCode.Combine(Type, BindingAttr);
        }
    }
}
