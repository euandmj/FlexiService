using System.Reflection;
using OneOf;

namespace core.Extensions;

public static class Extensions
{
    public static bool TryCreateHandler<T>(
        this Type type,
        object instance,
        MethodInfo method,
        out T? handler) where T : System.Delegate
    {
            handler = Delegate.CreateDelegate(
                typeof(T),
                instance,
                method,
                throwOnBindFailure: false) as T;

            return handler is not null;
    }

    public static OneOf<TA, TB> ToOneOf<TA, TB>(this (TA, TB) tuple)
    {
        return tuple.Item1 is not null
            ? tuple.Item1
            : tuple.Item2;
    }
}