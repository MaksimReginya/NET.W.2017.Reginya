using System;

namespace Matrix
{
    /// <inheritdoc />
    /// <summary>
    /// Symmetrical matrix(equals to transpose matrix).
    /// </summary>
    public class SymmetricalMatrix<T> : SquareMatrix<T>
    {
        #region Public constructors

        /// <inheritdoc />
        public SymmetricalMatrix(int order)
        {
            if (order < 1)
            {
                throw new ArgumentException("Matrix order must be greater than 0.");
            }

            this.Items = new T[(((order * order) - order) / 2) + order];
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
                if (j - i < 0)
                {
                    this.Swap(ref i, ref j);
                }

                return this.Items[(i * this.RowCount) + j - this.CalculateEmpty(i)];
            }

            set
            {
                this.VerifyIndexes(i, j);
                if (j - i < 0)
                {
                    this.Swap(ref i, ref j);
                }

                var oldValue = this.Items[(i * this.RowCount) + j - this.CalculateEmpty(i)];
                this.SetValue(value, (i * this.RowCount) + j - this.CalculateEmpty(i));
                this.OnElementChanged(this, new MatrixEventArgs<T>(i, j, oldValue, value));
            }
        }
        
        #endregion

        #region Protected methods
                
        protected void SetValue(T value, int i)
        {            
            this.Items[i] = value;            
        }
       
        #endregion

        #region Private methods

        private void Swap(ref int i, ref int j)
        {
            int temp = i;
            i = j;
            j = temp;
        }

        private int CalculateEmpty(int row)
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
