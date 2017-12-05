namespace Gu.Inject.Shims
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;

    internal class ConcurrentDictionary<TKey, TValue> : IEnumerable<KeyValuePair<TKey, TValue>>
    {
        private readonly object syncRoot = new object();
        private readonly Dictionary<TKey, TValue> collection;

        public ConcurrentDictionary(IEnumerable<KeyValuePair<TKey, TValue>> elements)
        {
            this.collection = elements.ToDictionary(kvp => kvp.Key, kvp => kvp.Value);
        }

        public ConcurrentDictionary()
        {
            this.collection = new Dictionary<TKey, TValue>();
        }

        public int Count
        {
            get
            {
                lock (this.syncRoot)
                {
                    return this.collection.Count;
                }
            }
        }

        public bool TryGetValue(TKey key, out TValue value)
        {
            lock (this.syncRoot)
            {
                return this.collection.TryGetValue(key, out value);
            }
        }

        public TValue GetOrAdd(TKey key, Func<TKey, TValue> factory)
        {
            lock (this.syncRoot)
            {
                bool exists = this.collection.TryGetValue(key, out var value);
                if (exists)
                {
                    return value;
                }
                else
                {
                    value = factory(key);

                    // double checking because the factory can add an element as a side effect
                    exists = this.collection.TryGetValue(key, out var ignored);
                    if (exists)
                    {
                        this.collection.Remove(key);
                    }

                    this.collection.Add(key, value);
                    return value;
                }
            }
        }

        public void Clear()
        {
            lock (this.syncRoot)
            {
                this.collection.Clear();
            }
        }

        public bool TryAdd(TKey key, TValue value)
        {
            lock (this.syncRoot)
            {
                bool exists = this.collection.TryGetValue(key, out TValue ignored);
                if (exists)
                {
                    return false;
                }

                this.collection.Add(key, value);
                return true;
            }
        }

        /// <inheritdoc />
        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
        {
            // TODO: this is shitty implementation even by this impl standards
            lock (this.syncRoot)
            {
                return this.collection.ToList().GetEnumerator();
            }
        }

        /// <inheritdoc />
        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        public TValue AddOrUpdate(TKey key, Func<TKey, TValue> addValueFactory, Func<TKey, TValue, TValue> updateValueFactory)
        {
            lock (this.syncRoot)
            {
                TValue newValue;
                if (this.collection.TryGetValue(key, out var value))
                {
                    newValue = updateValueFactory(key, value);
                    this.collection[key] = newValue;
                }
                else
                {
                    newValue = addValueFactory(key);
                    this.collection.Add(key, newValue);
                }

                return newValue;
            }
        }
    }
}
