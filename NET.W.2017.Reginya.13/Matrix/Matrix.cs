using System;
using System.Collections;
using System.Collections.Generic;

namespace Matrix
{    
    /// <summary>
    /// Two-dimensional array of elements of type <see cref="T"/>.
    /// </summary>
    /// <typeparam name="T">Type of stored elements.</typeparam>
    public class Matrix<T> : IEquatable<Matrix<T>>, IEnumerable<T>, IEnumerable
    {             
        #region Public constuctors
                                
        /// <summary>
        /// Initializes a new instance of the <see cref="Matrix"/> class with specified dimensional sizes.
        /// </summary>
        /// <param name="rowCount">Count of rows.</param>
        /// <param name="columnCount">Count of columns.</param>
        public Matrix(int rowCount, int columnCount)
        {
            if (rowCount < 1 || columnCount < 1)
            {
                throw new ArgumentException("Rows and columns count must be greater than 0.");
            }

            Items = new T[rowCount, columnCount];
            this.RowCount = rowCount;
            this.ColumnCount = columnCount;
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
        /// Count of rows in matrix.
        /// </summary>
        public int RowCount { get; protected set; }

        /// <summary>
        /// Count of columns in matrix.
        /// </summary>
        public int ColumnCount { get; protected set; }
                  
        #endregion

        #region Protected properties

        protected T[,] Items { get; set; }

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
                return Items[i, j];
            }

            set
            {
                VerifyIndexes(i, j);
                var oldValue = Items[i, j];
                SetValue(value, i, j);
                OnElementChanged(this, new MatrixEventArgs<T>(i, j, oldValue, value));
            }
        }

        #endregion

        #region Public methods

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
           
            if ((other.RowCount != this.RowCount) || (other.ColumnCount != this.ColumnCount))
            {
                return false;
            }

            var equalityComparer = EqualityComparer<T>.Default;
            for (int i = 0; i < this.RowCount; i++)
            {
                for (int j = 0; j < this.ColumnCount; j++)
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
            for (int i = 0; i < RowCount; i++)
            {
                for (int j = 0; j < ColumnCount; j++)
                {
                    yield return Items[i, j];
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

        #region Overriding methods of class Object

        /// <inheritdoc/>
        public override bool Equals(object other)
        {
            if (ReferenceEquals(null, other))
            {
                return false;
            }

            if (ReferenceEquals(this, other))
            {
                return true;
            }

            if (other.GetType() != this.GetType())
            {
                return false;
            }

            return this.Equals((Matrix<T>)other);
        }

        /// <inheritdoc/>
        public override int GetHashCode()
            => Items != null ? Items.GetHashCode() ^ this.ColumnCount ^ this.RowCount : 0;        

        #endregion

        #region Protected methods

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
        /// Sets the value of the matrix cell.
        /// </summary>
        /// <param name="value">Element value.</param>
        /// <param name="i">Row index.</param>
        /// <param name="j">Column index.</param>
        protected virtual void SetValue(T value, int i, int j)
            => this.Items[i, j] = value;

        #endregion

        #region Private methods

        private void VerifyIndexes(int i, int j)
        {
            if (i < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(i), $"{nameof(i)} must not be negative.");
            }

            if (i > this.RowCount)
            {
                throw new ArgumentOutOfRangeException(nameof(i), $"{nameof(i)} must be less than matrix size.");
            }

            if (j < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(j), $"{nameof(j)} must not be negative.");
            }

            if (j > this.ColumnCount)
            {
                throw new ArgumentOutOfRangeException(nameof(j), $"{nameof(j)} must be less than matrix size.");
            }
        }

        #endregion
    }
}
