namespace Gu.Inject.Shims
{
    using System;

    internal class TypeInfo
    {
        internal TypeInfo(Type[] interfaces)
        {
            this.ImplementedInterfaces = interfaces;
        }

        public Type[] ImplementedInterfaces { get; }
    }
}
