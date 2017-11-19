namespace Matrix
{
    /// <inheritdoc />
    /// <summary>
    /// Symmetrical matrix(equal to transpose matrix).
    /// </summary>
    public class SymmetricalMatrix<T> : SquareMatrix<T>
    {
        #region Public constructors

        /// <inheritdoc />
        public SymmetricalMatrix(int order) : base(order)
        {
        }

        #endregion

        #region Protected methods

        /// <inheritdoc />        
        protected override void SetValue(T value, int i, int j)
        {            
            this._matrix[i, j] = value;
            this._matrix[j, i] = value;
        }

        #endregion        
    }
}
