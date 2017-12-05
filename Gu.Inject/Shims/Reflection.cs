namespace Gu.Inject.Shims
{
    using System;
    using System.Linq;

    internal static class Reflection
    {
        public static Type[] GenericTypeArguments(this Type type)
        {
            return type.GetGenericArguments().Where(t => !t.IsGenericParameter).ToArray();
        }

        public static TypeInfo GetTypeInfo(this Type type)
        {
            return new TypeInfo(type.GetInterfaces());
        }
    }
}
