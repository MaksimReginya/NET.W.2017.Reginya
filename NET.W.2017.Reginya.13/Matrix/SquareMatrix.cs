namespace Matrix
{
    /// <inheritdoc />
    /// <summary>
    /// Square matrix(colums and rows count are equal).
    /// </summary>
    public class SquareMatrix<T> : Matrix<T>
    {
        #region Public constructors

        /// <inheritdoc />                        
        public SquareMatrix(int order) : base(order, order)
        {
        }
                
        #endregion
    }
}
