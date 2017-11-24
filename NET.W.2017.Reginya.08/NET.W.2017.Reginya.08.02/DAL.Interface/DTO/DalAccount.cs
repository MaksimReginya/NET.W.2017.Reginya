using System;

namespace DAL.Interface.DTO
{
    /// <summary>
    /// Decorator of bank account entity that is used to be saved in repository.
    /// </summary>
    public class DalAccount : IEquatable<DalAccount>
    {
        #region Public properties
                
        /// <summary>
        /// Type of bank account.
        /// </summary>
        public Type AccountType { get; set; }

        /// <summary>
        /// Unique account number.
        /// </summary>
        public string AccountNumber { get; set; }

        /// <summary>
        /// Owner's first name.
        /// </summary>
        public string OwnerFirstName { get; set; }

        /// <summary>
        /// Owner's last name.
        /// </summary>
        public string OwnerLastName { get; set; }

        /// <summary>
        /// Balance of the account.
        /// </summary>
        public decimal Balance { get; set; }

        /// <summary>
        /// Bonus of the account.
        /// </summary>
        public int Bonus { get; set; }

        #endregion

        #region Overloaded methods of class Object 

        /// <summary>
        /// Determines whether the specified instance of <see cref="DalAccount"/>
        /// class is equal to the current instance.
        /// </summary>
        /// <param name="other">Bank account to compare with the current instance.</param>
        /// <returns>Result of comparison.</returns>
        public override bool Equals(object other)
        {
            if (ReferenceEquals(null, other))
            {
                return false;
            }

            if (ReferenceEquals(this, other))
            {
                return true;
            }

            return other.GetType() != this.GetType() && this.Equals((DalAccount)other);
        }

        /// <summary>
        /// Returns the hash code for this instance of <see cref="DalAccount"/> class.
        /// </summary>
        /// <returns>The hash code for this instance.</returns>
        public override int GetHashCode() => AccountNumber.GetHashCode();
        
        #endregion
        
        #region IEquatable<DalAccount> implementation

        /// <inheritdoc />
        /// <summary>
        /// Compares two accounts on equality.
        /// </summary>
        /// <param name="other">Bank account to compare with the current instance.</param>  
        /// <returns>Result of comparison.</returns>
        public bool Equals(DalAccount other)
        {
            if (ReferenceEquals(null, other))
            {
                return false;
            }

            if (ReferenceEquals(this, other))
            {
                return true;
            }

            return AccountType.Equals(other.AccountType);
        }

        #endregion
    }
}
