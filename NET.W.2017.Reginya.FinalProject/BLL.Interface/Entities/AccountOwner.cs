using System;
using System.Collections.Generic;
using System.Net.Mail;

namespace BLL.Interface.Entities
{    
    public class AccountOwner
    {
        #region Private fields

        private string _firstName;
        private string _lastName;
        private string _email;        

        #endregion

        #region Public constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="AccountOwner"/> class.
        /// </summary>        
        /// <param name="email">User's email.</param>
        /// <param name="firstName">User's first name.</param>
        /// <param name="lastName">User's last name.</param>        
        public AccountOwner(string firstName, string lastName, string email)
        {
            this.Email = email;
            this.FirstName = firstName;
            this.LastName = lastName;
            this.Accounts = new List<BankAccount>();
        }

        #endregion

        #region Public properties

        /// <summary>
        /// Owner's first name.
        /// </summary>
        public string FirstName
        {
            get => _firstName;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException($"{nameof(FirstName)} can't be empty.", nameof(FirstName));
                }

                _firstName = value;
            }
        }

        /// <summary>
        /// Owner's last name.
        /// </summary>
        public string LastName
        {
            get => _lastName;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException($"{nameof(LastName)} can't be empty.", nameof(LastName));
                }

                _lastName = value;
            }
        }

        /// <summary>
        /// Owner's last name.
        /// </summary>
        public string Email
        {
            get => _email;
            set
            {
                VerifyEmailAddress(value);
                _email = value;
            }
        }
       
        /// <summary>
        /// Collection of owner's accounts.
        /// </summary>
        public List<BankAccount> Accounts { get; set; }

        #endregion

        #region Private methods
       
        private static void VerifyEmailAddress(string email)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(email))
                {
                    throw new ArgumentException();
                }

                var mailAddress = new MailAddress(email);
            }
            catch (Exception)
            {
                throw new ArgumentException($"{nameof(Email)} is invalid.", nameof(Email));
            }
        }

        #endregion
    }
}
