namespace Gu.Inject.Shims
{
    using System;

    [Serializable]
    public delegate void EventHandler<TEventArgs>(object sender, TEventArgs e);
}
