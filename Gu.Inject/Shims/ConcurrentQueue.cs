namespace Gu.Inject.Shims
{
    using System.Collections.Generic;

    internal class ConcurrentQueue<TElement>
    {
        private readonly object syncRoot = new object();
        private readonly Queue<TElement> collection;

        public ConcurrentQueue()
        {
            this.collection = new Queue<TElement>();
        }

        public bool TryDequeue(out TElement element)
        {
            lock (this.syncRoot)
            {
                if (this.collection.Count == 0)
                {
                    element = default(TElement);
                    return false;
                }
                else
                {
                    element = this.collection.Dequeue();
                    return true;
                }
            }
        }

        public void Enqueue(TElement element)
        {
            lock (this.syncRoot)
            {
                this.collection.Enqueue(element);
            }
        }
    }
}
