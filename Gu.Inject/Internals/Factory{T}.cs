namespace Gu.Inject
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using Gu.Inject.Shims;

    internal class Factory<T> : IFactory
    {
        private static readonly ReadOnlyList<Type> Empty = new ReadOnlyList<Type>();

        private readonly Func<T> creator;

        public Factory(Func<T> creator)
        {
            this.creator = creator;
        }

        public ReadOnlyList<Type> ParameterTypes => Empty;

        public object Create(object[] args)
        {
            Debug.Assert((args?.Length ?? 0) == 0, "args?.Length ??0 ==0");
            return this.creator();
        }
    }
}