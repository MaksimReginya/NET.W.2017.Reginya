using System;

namespace Matrix
{
    /// <inheritdoc />
    /// <summary>
    /// Symmetrical matrix(equals to transpose matrix).
    /// </summary>
    public class SymmetricalMatrix<T> : Matrix<T>
    {
        #region Private fields

        private readonly T[] _items;

        #endregion

        #region Public constructors        

        /// <inheritdoc />
        public SymmetricalMatrix(int order) : base(order)
        {
            this._items = new T[this.Order * this.Order];
        }

        /// <inheritdoc />
        public SymmetricalMatrix(T[,] elements) : base(elements)
        {
            this._items = new T[this.Order * this.Order];
            for (int i = 0; i < this.Order; i++)
            {
                for (int j = 0; j < this.Order; j++)
                {
                    this.SetValue(elements[i, j], i, j);
                }
            }
        }

        #endregion
        
        #region Protected methods

        /// <inheritdoc />
        protected override void SetValue(T value, int i, int j)
            => this._items[CalculateIndex(i, j, this.Order)] = value;

        /// <inheritdoc />
        protected override T GetValue(int i, int j)        
            => this._items[CalculateIndex(i, j, this.Order)];                    

        #endregion

        #region Private methods

        private static int CalculateIndex(int i, int j, int order)
        {
            if (j - i < 0)
            {
                return (j * order) + i - CalculateEmptyCells(j);
            }

            return (i * order) + j - CalculateEmptyCells(i);
        }

        private static int CalculateEmptyCells(int row)
        {
            int res = 0;
            for (int i = 1; i <= row; i++)
            {
                res += i;
            }

            return res;
        }
                
        #endregion
    }
}
