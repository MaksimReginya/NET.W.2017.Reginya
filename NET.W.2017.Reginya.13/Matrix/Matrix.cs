using System;
using System.Collections;
using System.Collections.Generic;

namespace Matrix
{    
    /// <summary>
    /// Two-dimensional square array of elements of type <see cref="T"/>.
    /// </summary>    
    public abstract class Matrix<T> : IEquatable<Matrix<T>>, IEnumerable<T>, IEnumerable
    {
        #region Public constuctors
        
        /// <summary>
        /// Initializes a new matrix of specified <paramref name="order"/>.
        /// </summary>
        /// <param name="order">Matrix order</param>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown when <paramref name="order"/> is not positive.
        /// </exception>
        protected Matrix(int order)
        {
            if (order <= 0)
            {
                throw new ArgumentOutOfRangeException($"{nameof(order)} must be positive.", nameof(order));
            }

            this.Order = order;
        }

        /// <summary>
        /// Initializes a new matrix with specified <paramref name="elements"/>.
        /// </summary>
        /// <param name="elements">Array of matrix elements.</param>
        /// <exception cref="ArgumentNullException">
        /// Thrown when <paramref name="elements"/> is null.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// Thrown when matrix is not square.
        /// </exception>
        protected Matrix(T[,] elements)
        {
            if (ReferenceEquals(elements, null))
            {
                throw new ArgumentNullException(nameof(elements));
            }

            if (elements.GetLength(0) != elements.GetLength(1))
            {
                throw new ArgumentException($"Array of {nameof(elements)} must be square.", nameof(elements));
            }

            this.Order = elements.GetLength(0);
        }
        
        #endregion

        #region Events

        /// <summary>
        /// The event that occurs when any element of the matrix is changed.
        /// </summary>
        public event EventHandler<MatrixEventArgs<T>> ElementChanged = delegate { };

        #endregion

        #region Public properties

        /// <summary>
        /// Order of the matrix.
        /// </summary>
        public int Order { get; protected set; }

        #endregion
        
        #region Indexers

        /// <summary>
        /// Indexer to access matrix elements.
        /// </summary>
        /// <param name="i">Row number.</param>
        /// <param name="j">Column number.</param>
        /// <returns>The value of element.</returns>
        public T this[int i, int j]
        {
            get
            {
                VerifyIndexes(i, j);
                return this.GetValue(i, j);
            }

            set
            {
                VerifyIndexes(i, j);
                var oldValue = this.GetValue(i, j);
                this.SetValue(value, i, j);
                this.OnElementChanged(this, new MatrixEventArgs<T>(i, j, oldValue, value));
            }
        }

        #endregion

        #region Public methods

        /// <summary>
        /// Returns an array of matrix elements.
        /// </summary>
        /// <returns>Array of matrix elements.</returns>
        public T[,] ToArray()
        {
            var result = new T[this.Order, this.Order];

            for (int i = 0; i < this.Order; i++)
            {
                for (int j = 0; j < this.Order; j++)
                {
                    result[i, j] = this.GetValue(i, j);
                }
            }

            return result;
        }

        /// <summary>
        /// Strictly typed Equals method. 
        /// </summary>
        /// <param name="other">Matrix to check equality with current instance.</param>
        /// <returns>True if equals, false if not.</returns>
        public bool Equals(Matrix<T> other)
        {
            if (ReferenceEquals(null, other))
            {
                return false;
            }

            if (ReferenceEquals(this, other))
            {
                return true;
            }

            if (this.Order != other.Order)
            {
                return false;
            }

            var equalityComparer = EqualityComparer<T>.Default;
            for (int i = 0; i < this.Order; i++)
            {
                for (int j = 0; j < this.Order; j++)
                {
                    if (!equalityComparer.Equals(this[i, j], other[i, j]))
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        #endregion

        #region IEnumerable<T> and IEnumerable implementation

        /// <inheritdoc />
        public IEnumerator<T> GetEnumerator()
        {
            for (int i = 0; i < this.Order; i++)
            {
                for (int j = 0; j < this.Order; j++)
                {
                    yield return this.GetValue(i, j);
                }
            }
        }

        /// <inheritdoc />
        IEnumerator IEnumerable.GetEnumerator() =>
            this.GetEnumerator();

        #endregion

        #region IEquatable<Matrix<T>> implementation

        /// <inheritdoc/>
        bool IEquatable<Matrix<T>>.Equals(Matrix<T> other)
        {
            return this.Equals(other);
        }

        #endregion

        #region Protected methods

        /// <summary>
        /// Sets the value of the matrix cell.
        /// </summary>
        /// <param name="value">Element value.</param>
        /// <param name="i">Row index.</param>
        /// <param name="j">Column index.</param>
        protected abstract void SetValue(T value, int i, int j);

        /// <summary>
        /// Gets the value of the matrix cell.
        /// </summary>        
        /// <param name="i">Row index.</param>
        /// <param name="j">Column index.</param>
        protected abstract T GetValue(int i, int j);

        /// <summary>
        /// Invokes ElementChanged event.
        /// </summary>
        /// <param name="sender">Sender of the event.</param>
        /// <param name="args">Arguments of the event.</param>
        protected virtual void OnElementChanged(object sender, MatrixEventArgs<T> args)
        {
            var temp = ElementChanged;
            temp?.Invoke(sender, args);
        }

        /// <summary>
        /// Verifies indexes of row(i) and column(j) in matrix.
        /// </summary>
        /// <param name="i">Index of row.</param>
        /// <param name="j">Index of column.</param>
        protected void VerifyIndexes(int i, int j)
        {
            if (i < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(i), $"{nameof(i)} must not be negative.");
            }

            if (i > this.Order)
            {
                throw new ArgumentOutOfRangeException(nameof(i), $"{nameof(i)} must be less than matrix size.");
            }

            if (j < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(j), $"{nameof(j)} must not be negative.");
            }

            if (j > this.Order)
            {
                throw new ArgumentOutOfRangeException(nameof(j), $"{nameof(j)} must be less than matrix size.");
            }
        }

        #endregion
    }
}
