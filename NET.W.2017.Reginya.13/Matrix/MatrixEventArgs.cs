using System;

namespace Matrix
{
    /// <inheritdoc />
    /// <summary>
    /// Class that contains event data for ElementChanged event of Matrix.
    /// </summary>    
    public class MatrixEventArgs<T> : EventArgs
    {
        /// <inheritdoc />
        /// <summary>
        /// Initializes a new instance of the <see cref="MatrixEventArgs{T}"/> class.
        /// </summary>
        /// <param name="row">Index of matrix element row.</param>
        /// <param name="column">Index of matrix element column.</param>
        /// <param name="oldValue">Old value of matrix element.</param>
        /// <param name="newValue">New value of matrix element.</param>
        public MatrixEventArgs(int row, int column, T oldValue, T newValue)
        {
            this.Row = row;
            this.Column = column;
            this.OldValue = oldValue;
            this.NewValue = newValue;
        }

        /// <summary>
        /// Index of matrix element row.
        /// </summary>
        public int Row { get; }

        /// <summary>
        /// Index of matrix element column.
        /// </summary>
        public int Column { get; }

        /// <summary>
        /// Old value of matrix element.
        /// </summary>
        public T OldValue { get; }

        /// <summary>
        /// New value of matrix element.
        /// </summary>
        public T NewValue { get; }
    }
}
