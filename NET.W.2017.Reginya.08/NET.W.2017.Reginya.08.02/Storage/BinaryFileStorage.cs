using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using BankAccountLogic;
using BankAccountLogic.AccountTypes;
using BankAccountLogic.Exceptions;

namespace Storage
{
    /// <inheritdoc />
    /// <summary>
    /// Binary storage of accounts
    /// </summary>
    public class BinaryFileStorage : IStorage
    {
        #region Private fields

        private readonly string _filePath;
        private readonly List<BankAccount> _accounts;

        #endregion         

        #region Public constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="BinaryFileStorage" /> class.
        /// </summary>
        public BinaryFileStorage(string filePath)
        {
            if (string.IsNullOrWhiteSpace(filePath))
            {
                throw new ArgumentException("Path to file is invalid.", nameof(filePath));
            }

            _filePath = filePath;
            _accounts = new List<BankAccount>();

            try
            {
                if (File.Exists(_filePath))
                {
                    ParseFile();
                }
            }
            catch (Exception)
            {
                _accounts.Clear();
            }
        }

        #endregion

        #region IStorage implementation

        /// <inheritdoc />
        public void AddAccount(BankAccount account)
        {
            if (ReferenceEquals(account, null))
            {
                throw new ArgumentNullException(nameof(account));
            }

            if (_accounts.Any(account.Equals))
            {
                throw new AccountAlreadyExistsException(account);
            }

            AppendAccountToFile(account);
            _accounts.Add(account);
        }

        /// <inheritdoc />
        public BankAccount GetAccount(string accountNumber)
        {
            if (string.IsNullOrWhiteSpace(accountNumber))
            {
                throw new ArgumentException(nameof(accountNumber));
            }

            return _accounts.FirstOrDefault(account => account.AccountNumber == accountNumber);
        }

        /// <inheritdoc />
        public void UpdateAccount(BankAccount account)
        {
            if (ReferenceEquals(account, null))
            {
                throw new ArgumentNullException(nameof(account));
            }

            if (!_accounts.Any(account.Equals))
            {
                throw new AccountNotFoundException(account.AccountNumber);
            }

            _accounts.Remove(account);
            _accounts.Add(account);
            WriteAccountsToFile();
        }

        /// <inheritdoc />
        public void RemoveAccount(BankAccount account)
        {
            if (ReferenceEquals(account, null))
            {
                throw new ArgumentNullException(nameof(account));
            }

            if (!_accounts.Any(account.Equals))
            {
                throw new AccountNotFoundException(account.AccountNumber);
            }

            _accounts.Remove(account);
            WriteAccountsToFile();
        }

        /// <inheritdoc />
        public IEnumerable<BankAccount> GetAllAccounts() =>
            new List<BankAccount>(_accounts);

        /// <inheritdoc />
        public void SaveChanges()
        {
            WriteAccountsToFile();
        }

        #endregion 

        #region Private methods

        private static BankAccount ReadAccountFromFile(BinaryReader reader)
        {
            string typeName = reader.ReadString();            
            string accountNumber = reader.ReadString();
            string ownerFirstName = reader.ReadString();
            string ownerLastName = reader.ReadString();
            decimal balance = reader.ReadDecimal();
            int bonus = reader.ReadInt32();

            return Activator.CreateInstance(
                Type.GetType(typeName),
                accountNumber,
                ownerFirstName,
                ownerLastName,
                balance,
                bonus) as BankAccount;
        }

        private static void WriteAccountToFile(BinaryWriter writer, BankAccount account)
        {
            writer.Write(account.GetType().AssemblyQualifiedName);
            writer.Write(account.AccountNumber);
            writer.Write(account.OwnerFirstName);
            writer.Write(account.OwnerLastName);
            writer.Write(account.Balance);
            writer.Write(account.Bonus);
        }        

        private void ParseFile()
        {
            using (var reader = new BinaryReader(File.Open(_filePath, FileMode.Open), Encoding.UTF8))
            {
                while (reader.BaseStream.Position < reader.BaseStream.Length)
                {
                    var account = ReadAccountFromFile(reader);
                    _accounts.Add(account);
                }
            }
        }

        private void AppendAccountToFile(BankAccount account)
        {
            using (var writer = new BinaryWriter(File.Open(_filePath, FileMode.Append), Encoding.UTF8))
            {
                WriteAccountToFile(writer, account);                
            }
        }

        private void WriteAccountsToFile()
        {
            using (var writer = new BinaryWriter(File.Open(_filePath, FileMode.Create), Encoding.UTF8))
            {
                foreach (var account in _accounts)
                {
                    WriteAccountToFile(writer, account);
                }                
            }
        }

        #endregion
    }
}
