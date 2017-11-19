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
        public DiagonalMatrix(int order) : base(order)
        {
        }

        #endregion

        #region Protected methods

        /// <inheritdoc />
        /// <exception cref="InvalidOperationException">
        /// Thrown when setting element is not on the diagonal.
        /// </exception>
        protected override void SetValue(T value, int i, int j)
        {
            if (i != j)
            {
                throw new InvalidOperationException("Diagonal matrix can have elements only on the diagonal.");
            }

            this._matrix[i, j] = value;
        }

        #endregion        
    }
}
