using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RyanJuan.Minerva.Common
{
    internal static class Error
    {
        public static ArgumentNullException ArgumentNull(string name)
        {
            return new ArgumentNullException(name);
        }

        public static void ThrowIfArgumentNull<TValue>(string name, TValue value)
        {
            if (value is null)
            {
                throw ArgumentNull(name);
            }
        }

        public static ArgumentException ArgumentException(string name, ArgumentException ex)
        {
            return new ArgumentException(ex.Message, name, ex);
        }

        public static InvalidOperationException MoreThanOneElement()
        {
            return new InvalidOperationException("Sequence contains more than one element");
        }

        public static InvalidOperationException NoElements()
        {
            return new InvalidOperationException("Sequence contains no elements");
        }

        public static OperationCanceledException OperationCanceled(
            CancellationToken cancellationToken)
        {
            return new OperationCanceledException(
                "The CancellationToken has been canceled.",
                cancellationToken);
        }

        public static void ThrowIfOperationCanceled(
            CancellationToken cancellationToken)
        {
            if (cancellationToken.IsCancellationRequested)
            {
                throw OperationCanceled(cancellationToken);
            }
        }

        public static ObjectDisposedException ObjectDisposed(string objectName)
        {
            return new ObjectDisposedException(objectName);
        }
    }
}
