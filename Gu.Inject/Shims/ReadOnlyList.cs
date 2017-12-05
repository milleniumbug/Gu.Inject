namespace Gu.Inject.Shims
{
    using System.Collections;
    using System.Collections.Generic;

    /// <summary>
    /// IReadOnlyList shim for .NET 3.5
    /// </summary>
    /// <typeparam name="TElement">Element type</typeparam>
    internal class ReadOnlyList<TElement> : IEnumerable<TElement>
    {
        private readonly IList<TElement> collection;

        /// <summary>
        /// Initializes a new instance of the <see cref="ReadOnlyList{TElement}"/> class.
        /// Creates a read-only list from the input list. The elements are NOT copied, so the modifications to the input list are reflected in this list.
        /// </summary>
        /// <param name="elements">Input list</param>
        public ReadOnlyList(IList<TElement> elements)
        {
            this.collection = elements;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ReadOnlyList{TElement}"/> class.
        /// Constructs an empty read-only list.
        /// </summary>
        public ReadOnlyList()
        {
            this.collection = new TElement[0];
        }

        /// <summary>
        /// Gets the number of the elements in the collection.
        /// </summary>
        public int Count => this.collection.Count;

        /// <summary>
        /// Accesses an element at the specified index.
        /// </summary>
        /// <param name="index">Position of the accessed element in the list</param>
        public TElement this[int index] => this.collection[index];

        /// <inheritdoc />
        public IEnumerator<TElement> GetEnumerator()
        {
            return this.collection.GetEnumerator();
        }

        /// <inheritdoc />
        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}
