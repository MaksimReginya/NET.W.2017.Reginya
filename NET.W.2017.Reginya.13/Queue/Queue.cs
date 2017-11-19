using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Queue
{
    /// <summary>
    /// Represents functionality of the queue.
    /// </summary>    
    /// <typeparam name="T">Element type of queue.</typeparam>
    public class Queue<T> : IEnumerable<T>, IEnumerable
    {
        #region Private fields
        
        private const int DefaultCapacity = 15;

        private T[] _items;        
        private int _head;
        private int _tail;
        private int _version;

        #endregion

        #region Public constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="Queue"/> class.
        /// </summary>
        public Queue()
        {
            _items = new T[DefaultCapacity];
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Queue"/> class with specified capacity.
        /// </summary>
        /// <param name="capacity">Queue capacity</param>
        /// <exception cref="ArgumentException">
        /// Thrown when queue capacity is negative.
        /// </exception>
        public Queue(int capacity)
        {
            if (capacity < 0)
            {
                throw new ArgumentException($"{nameof(capacity)} must not be negative.", nameof(capacity));
            }

            _items = new T[capacity];
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Queue"/> class with specified with the specified basic collection of elements.
        /// </summary>
        /// <param name="items">Basic queue elements</param>
        /// <exception cref="ArgumentNullException">
        /// Thrown when <paramref name="items"/> is null.
        /// </exception>
        public Queue(IEnumerable<T> items)
        {
            if (ReferenceEquals(items, null))
            {
                throw new ArgumentNullException(nameof(items));
            }

            var enumerable = items as T[] ?? items.ToArray();
            _items = new T[enumerable.Length];
            Count = _items.Length;
            _tail = Count - 1;
            Array.Copy(enumerable, _items, _items.Length);
        }

        #endregion

        #region Public properties
                
        /// <summary>
        /// Number of elements in queue.
        /// </summary>
        public int Count { get; private set; }

        #endregion

        #region IEnumerable and IEnumerable<T> implementation

        public Enumerator GetEnumerator() =>
            new Enumerator(this);

        /// <inheritdoc />
        IEnumerator<T> IEnumerable<T>.GetEnumerator() =>
            this.GetEnumerator();

        /// <inheritdoc />
        IEnumerator IEnumerable.GetEnumerator() =>
            this.GetEnumerator();

        #endregion 
        
        #region Public methods
        
        /// <summary>
        /// Clears the queue.
        /// </summary>
        public void Clear()
        {
            _head = 0;
            _tail = 0;
            Count = 0;
            _version++;
            Array.Clear(_items, 0, _items.Length);
        }

        /// <summary>
        /// Inserts an <paramref name="item"/> in the end of the queue.
        /// </summary>
        /// <param name="item">Inserting element.</param>
        /// <exception cref="ArgumentNullException">
        /// Thrown when <paramref name="item"/> is null.
        /// </exception>
        public void Enqueue(T item)
        {
            if (ReferenceEquals(item, null))
            {
                throw new ArgumentNullException(nameof(item));
            }

            if (Count == _items.Length)
            {
                Array.Resize(ref _items, _items.Length * 2);
            }

            if (_items.Length - 1 - _head < _items.Length / 2)
            {
                CompressQueue();
            }

            _items[++_tail] = item;
            Count++;
            _version++;
        }

        /// <summary>
        /// Returns an element at the front of the queue.
        /// </summary>
        /// <returns>Element at the front of the queue.</returns>
        /// <exception cref="InvalidOperationException">
        /// Thrown when queue is empty.
        /// </exception>
        public T Dequeue()
        {
            if (_items.Length == 0)
            {
                throw new InvalidOperationException("Queue is empty.");
            }

            var result = _items[_head];
            _items[_head++] = default(T);
            Count--;
            _version++;
            return result;
        }

        /// <summary>
        /// Gets the element at the front of the queue without removing it.
        /// </summary>
        /// <returns>Element at the front of the queue.</returns>
        /// <exception cref="InvalidOperationException">
        /// Thrown when the queue is empty.
        /// </exception>
        public T Peek()
        {
            if (_items.Length == 0)
            {
                throw new InvalidOperationException("Queue is empty.");
            }

            return _items[_head];
        }

        /// <summary>
        /// Checks if the current instance of the queue contains elements.
        /// </summary>
        /// <returns>True if the queue is empty, and otherwise.</returns>
        public bool IsEmpty()
            => this.Count == 0;

        #endregion

        #region Private methods

        private void CompressQueue()
        {
            var newQueue = new T[Count];
            Array.Copy(_items, _head, newQueue, 0, Count);            
            _tail -= _head;
            _head = 0;
            _items = newQueue;
        }

        private T GetElement(int index)
        {
            if (index < 0 || index > this.Count)
            {
                throw new ArgumentOutOfRangeException(nameof(index));
            }

            return _items[index];
        }

        #endregion

        /// <summary>
        /// The enumerator of queue.
        /// </summary>        
        public struct Enumerator : IEnumerator<T>, IEnumerator
        {
            #region Private fields            
     
            private readonly Queue<T> _queue;            
            private int _index;            
            private readonly int _version;

            #endregion           

            #region Public constructors

            /// <summary>
            /// Initializes a new instance of the <see cref="Enumerator"/> structure.           
            /// </summary>
            /// <param name="queue">The target queue for the enumerator.</param>
            /// <exception cref="ArgumentNullException">
            /// Thrown when <paramref name="queue"/> is null.
            /// </exception>
            internal Enumerator(Queue<T> queue)
            {
                if (ReferenceEquals(queue, null))
                {
                    throw new ArgumentNullException(nameof(queue));
                }

                _queue = queue;
                _index = -1;
                _version = queue._version;
            }

            #endregion 

            #region IEnumerator<T> and IEnumerator implementation

            /// <inheritdoc />
            public T Current => _queue.GetElement(_index);

            /// <inheritdoc />
            object IEnumerator.Current => this.Current;

            /// <inheritdoc />
            public void Dispose()
            {
                _index = -1;
            }

            /// <inheritdoc />
            public bool MoveNext()
            {
                if (_version != _queue._version)
                {
                    throw new InvalidOperationException("It's not allowed to change collection items inside enumerator");
                }

                return ++_index < _queue.Count;
            }

            /// <inheritdoc />
            public void Reset()
            {
                if (_version != _queue._version)
                {
                    throw new InvalidOperationException("It's not allowed to change collection items inside enumerator");
                }

                _index = -1;
            }

            #endregion             
        }
    }
}
