using System;

namespace Matrix
{
    /// <inheritdoc />
    /// <summary>
    /// Diagonal matrix(contains elements only on the diagonal).
    /// </summary>
    public class DiagonalMatrix<T> : Matrix<T>
    {
        #region Private fields

        private readonly T[] _items;

        #endregion

        #region Public constructors        

        /// <inheritdoc />
        public DiagonalMatrix(int order) : base(order)
        {
            this._items = new T[this.Order];
        }

        /// <inheritdoc />
        public DiagonalMatrix(T[,] elements) : base(elements)
        {
            this._items = new T[this.Order];
            for (int i = 0; i < this.Order; i++)
            {                
                this.SetValue(elements[i, i], i, i); 
            }
        }

        #endregion

        #region Protected methods

        /// <inheritdoc />
        protected override void SetValue(T value, int i, int j)
        {
            if (i == j)
            {
                this._items[i] = value;
            }
        }            

        /// <inheritdoc />
        protected override T GetValue(int i, int j)
            => i == j ? this._items[i] : default(T);

        #endregion       
    }
}
