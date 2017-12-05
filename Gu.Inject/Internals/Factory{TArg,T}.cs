namespace Gu.Inject
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using Gu.Inject.Shims;

    internal class Factory<TArg, T> : IFactory
    {
        private readonly Func<TArg, T> creator;

        public Factory(Func<TArg, T> creator)
        {
            this.creator = creator;
        }

        public ReadOnlyList<Type> ParameterTypes { get; } = new ReadOnlyList<Type>(new[] { typeof(TArg) });

        public object Create(object[] args)
        {
            Debug.Assert(args.Length == 1, "args.Length ==1");
            return this.creator((TArg)args[0]);
        }
    }
}