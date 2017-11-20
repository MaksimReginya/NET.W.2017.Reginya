using System;

namespace Matrix
{
    /// <summary>
    /// Extends the functionality of a matrix.
    /// </summary>
    public static class MatrixExtension
    {
        /// <summary>
        /// Summarizes two matrices.
        /// </summary>        
        /// <param name="lhs">First addendum</param>
        /// <param name="rhs">Second addendum</param>        
        /// <returns>Sum of two matrices.</returns>
        /// <exception cref="ArgumentNullException">
        /// Thrown when <paramref name="rhs"/> is null.
        /// </exception>
        /// <exception cref="InvalidOperationException">
        /// Thrown when the addition operation is impossible.
        /// </exception>
        public static Matrix<T> Add<T>(this Matrix<T> lhs, Matrix<T> rhs)
        {
            try
            {
                return lhs.Add(rhs, (arg1, arg2) => (dynamic)arg1 + (dynamic)arg2);
            }
            catch (Exception e)
            {
                throw new InvalidOperationException(e.Message, e);
            }
        }

        /// <summary>
        /// Summarizes two matrices.
        /// </summary>        
        /// <param name="lhs">First addendum</param>
        /// <param name="rhs">Second addendum</param>
        /// <param name="sumOperation">Addition operation of two matrix elements.</param>
        /// <returns>Sum of two matrices.</returns>
        /// <exception cref="ArgumentNullException">
        /// Thrown when <paramref name="rhs"/> is null.
        /// </exception>
        /// <exception cref="InvalidOperationException">
        /// Thrown when the addition operation is impossible.
        /// </exception>
        public static Matrix<T> Add<T>(this Matrix<T> lhs, Matrix<T> rhs, Func<T, T, T> sumOperation)
        {
            if (ReferenceEquals(rhs, null))
            {
                throw new ArgumentNullException(nameof(rhs));
            }

            if (lhs.RowCount != rhs.RowCount || lhs.ColumnCount != rhs.ColumnCount)
            {
                throw new InvalidOperationException("Matrixes must have equal dimensional lengths.");
            }

            // BAD DECISION!!!
            // TODO: Change type checking
            Matrix<T> result;
            if (lhs.GetType() == typeof(SquareMatrix<T>) || rhs.GetType() == typeof(SquareMatrix<T>))
            {
                result = new SquareMatrix<T>(lhs.RowCount);
            }
            else if (lhs.GetType() == typeof(SymmetricalMatrix<T>) || rhs.GetType() == typeof(SymmetricalMatrix<T>))
            {
                result = new SymmetricalMatrix<T>(lhs.RowCount);
            }
            else
            {
                result = new DiagonalMatrix<T>(lhs.RowCount);
            }            

            for (int i = 0; i < lhs.RowCount; i++)
            {
                for (int j = 0; j < lhs.ColumnCount; j++)
                {
                    result[i, j] = sumOperation(lhs[i, j], rhs[i, j]);
                }
            }

            return result;
        }
    }
}
