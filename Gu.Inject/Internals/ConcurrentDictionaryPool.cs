namespace Gu.Inject
{
    using Gu.Inject.Shims;

    internal static class ConcurrentDictionaryPool<TKey, TValue>
    {
        private static readonly ConcurrentQueue<ConcurrentDictionary<TKey, TValue>> Cache = new ConcurrentQueue<ConcurrentDictionary<TKey, TValue>>();

        internal static ConcurrentDictionary<TKey, TValue> Borrow()
        {
            return Cache.TryDequeue(out ConcurrentDictionary<TKey, TValue> result)
                ? result
                : new ConcurrentDictionary<TKey, TValue>();
        }

        internal static void Return(ConcurrentDictionary<TKey, TValue> dictionary)
        {
            if (dictionary == null)
            {
                return;
            }

            dictionary.Clear();
            Cache.Enqueue(dictionary);
        }
    }
}
