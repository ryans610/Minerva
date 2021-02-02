using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace RyanJuan.Minerva.Common
{
    internal static partial class ReflectionCenter
    {
        public static T CreateInstance<T>()
        {
            return ExpressionConstructor<T>.Default();
        }

        private static class ExpressionConstructor<T>
        {
            private static Func<T> s_default = null;

            public static Func<T> Default =>
                s_default ??= Expression.Lambda<Func<T>>(Expression.New(typeof(T))).Compile();
        }
    }
}
