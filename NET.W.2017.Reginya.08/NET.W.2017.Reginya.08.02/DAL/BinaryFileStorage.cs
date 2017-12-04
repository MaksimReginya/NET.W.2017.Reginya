using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using DAL.Interface;
using DAL.Interface.DTO;

namespace DAL
{
    /// <inheritdoc />
    /// <summary>
    /// Binary storage of accounts
    /// </summary>
    public class BinaryFileStorage : IBankAccountRepository
    {
        #region Private fields

        private readonly string _filePath;
        private readonly List<DtoAccount> _accounts;

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
            _accounts = new List<DtoAccount>();

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
        public void AddAccount(DtoAccount account)
        {
            if (ReferenceEquals(account, null))
            {
                throw new ArgumentNullException(nameof(account));
            }

            if (_accounts.Any(dalAccount => dalAccount.AccountNumber == account.AccountNumber))
            {
                throw new RepositoryException("Account already exists in repository.");
            }

            AppendAccountToFile(account);
            _accounts.Add(account);
        }

        /// <inheritdoc />
        public DtoAccount GetAccount(string accountNumber)
        {
            if (string.IsNullOrWhiteSpace(accountNumber))
            {
                throw new ArgumentException(nameof(accountNumber));
            }

            return _accounts.FirstOrDefault(account => account.AccountNumber == accountNumber);
        }

        /// <inheritdoc />
        public void UpdateAccount(DtoAccount account)
        {
            if (ReferenceEquals(account, null))
            {
                throw new ArgumentNullException(nameof(account));
            }

            if (!_accounts.Any(dalAccount => dalAccount.AccountNumber == account.AccountNumber))
            {
                throw new RepositoryException("Account can't be found in repository.");
            }

            _accounts.RemoveAll(dalAccount => dalAccount.AccountNumber == account.AccountNumber);
            _accounts.Add(account);
            WriteAccountsToFile();
        }

        /// <inheritdoc />
        public void RemoveAccount(DtoAccount account)
        {
            if (ReferenceEquals(account, null))
            {
                throw new ArgumentNullException(nameof(account));
            }

            if (!_accounts.Any(dalAccount => dalAccount.AccountNumber == account.AccountNumber))
            {
                throw new RepositoryException("Account can't be found in repository.");
            }

            _accounts.RemoveAll(dalAccount => dalAccount.AccountNumber == account.AccountNumber);
            WriteAccountsToFile();
        }

        /// <inheritdoc />
        public IEnumerable<DtoAccount> GetAllAccounts() =>
            new List<DtoAccount>(_accounts);

        #endregion

        #region Private methods

        private static DtoAccount ReadAccountFromFile(BinaryReader reader)
        {
            string typeName = reader.ReadString();
            string accountNumber = reader.ReadString();
            string ownerFirstName = reader.ReadString();
            string ownerLastName = reader.ReadString();
            decimal balance = reader.ReadDecimal();
            int bonus = reader.ReadInt32();

            return new DtoAccount
            {
                AccountType = typeName,
                AccountNumber = accountNumber,
                OwnerFirstName = ownerFirstName,
                OwnerLastName = ownerLastName,
                Balance = balance,
                Bonus = bonus
            };
        }

        private static void WriteAccountToFile(BinaryWriter writer, DtoAccount account)
        {
            writer.Write(account.AccountType);
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

        private void AppendAccountToFile(DtoAccount account)
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
