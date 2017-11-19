using System;

namespace Matrix
{
    /// <inheritdoc />
    /// <summary>
    /// Class that contains event data for ElementChanged event of Matrix.
    /// </summary>
    /// <typeparam name="T">Matrix element type.</typeparam>
    public class MatrixEventArgs<T> : EventArgs
    {
        /// <inheritdoc />
        /// <summary>
        /// Initializes a new instance of the <see cref="MatrixEventArgs{T}"/> class.
        /// </summary>
        /// <param name="row">Matrix element row.</param>
        /// <param name="column">Matrix element column.</param>
        /// <param name="oldValue">Matrix element old value.</param>
        /// <param name="newValue">Matrix element new value.</param>
        public MatrixEventArgs(int row, int column, T oldValue, T newValue)
        {
            this.Row = row;
            this.Column = column;
            this.OldValue = oldValue;
            this.NewValue = newValue;
        }

        /// <summary>
        /// Matrix element row.
        /// </summary>
        public int Row { get; }

        /// <summary>
        /// Matrix element column.
        /// </summary>
        public int Column { get; }

        /// <summary>
        /// Matrix element old value.
        /// </summary>
        public T OldValue { get; }

        /// <summary>
        /// Matrix element new value.
        /// </summary>
        public T NewValue { get; }
    }
}
