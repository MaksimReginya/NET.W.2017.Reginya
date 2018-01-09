namespace DAL.Interface.DTO
{
    /// <summary>
    /// Decorator of bank account entity that is used to be saved in repository.
    /// </summary>
    public class DtoAccount
    {
        #region Public properties
                
        /// <summary>
        /// Type of bank account.
        /// </summary>
        public string AccountType { get; set; }

        /// <summary>
        /// Unique account number.
        /// </summary>
        public string AccountNumber { get; set; }

        /// <summary>
        /// Account owner.
        /// </summary>
        public DtoAccountOwner AccountOwner { get; set; }

        /// <summary>
        /// Balance of the account.
        /// </summary>
        public decimal Balance { get; set; }

        /// <summary>
        /// Bonus of the account.
        /// </summary>
        public int Bonus { get; set; }

        #endregion       
    }
}
