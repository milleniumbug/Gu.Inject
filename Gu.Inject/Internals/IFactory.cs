namespace Gu.Inject
{
    using System;
    using System.Collections.Generic;
    using Gu.Inject.Shims;

    interface IFactory
    {
        object Create(object[] args);

        ReadOnlyList<Type> ParameterTypes { get; }
    }
}