namespace Matrix
{
    /// <inheritdoc />
    /// <summary>
    /// Square matrix(colums and rows count are equal).
    /// </summary>
    public class SquareMatrix<T> : Matrix<T>
    {
        #region Private fields

        private readonly T[,] _items;

        #endregion

        #region Public constructors        

        /// <inheritdoc />
        public SquareMatrix(int order) : base(order)
        {
            this._items = new T[this.Order, this.Order];
        }

        /// <inheritdoc />
        public SquareMatrix(T[,] elements) : base(elements)
        {
            this._items = new T[this.Order, this.Order];
            for (int i = 0; i < this.Order; i++)
            {
                for (int j = 0; j < this.Order; j++)
                {
                    this.SetValue(elements[i, j], i, j);
                }
            }
        }

        #endregion

        #region Protected properties

        /// <inheritdoc />
        protected override void SetValue(T value, int i, int j) =>
            this._items[i, j] = value;

        /// <inheritdoc />
        protected override T GetValue(int i, int j) =>
            this._items[i, j];

        #endregion
    }
}
