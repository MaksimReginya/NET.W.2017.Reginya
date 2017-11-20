using System;

namespace Matrix
{
    /// <inheritdoc />
    /// <summary>
    /// Diagonal matrix(contains elements only on the diagonal).
    /// </summary>
    public class DiagonalMatrix<T> : SquareMatrix<T>
    {
        #region Public constructors

        /// <inheritdoc />
        public DiagonalMatrix(int order)
        {
            if (order < 1)
            {
                throw new ArgumentException("Matrix order must be greater than 0.");
            }

            this.Items = new T[order];
            this.RowCount = order;
            this.ColumnCount = order;
        }

        #endregion

        #region Protected properties

        protected new T[] Items { get; set; }

        #endregion

        #region Indexers

        public override T this[int i, int j]
        {
            get
            {
                this.VerifyIndexes(i, j);
                if (i != j)
                {
                    return default(T);
                }

                return this.Items[i];
            }

            set
            {
                this.VerifyIndexes(i, j);
                if (i != j)
                {
                    return;
                }

                var oldValue = this.Items[i];
                this.SetValue(value, i, j);
                this.OnElementChanged(this, new MatrixEventArgs<T>(i, j, oldValue, value));
            }
        }

        #endregion

        #region Protected methods

        /// <inheritdoc />
        /// <exception cref="InvalidOperationException">
        /// Thrown when setting element is not on the diagonal.
        /// </exception>
        protected override void SetValue(T value, int i, int j)
        {            
            this.Items[i] = value;
        }
        
        #endregion        
    }
}
