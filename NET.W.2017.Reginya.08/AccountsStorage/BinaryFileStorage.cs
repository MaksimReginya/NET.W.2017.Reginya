using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using BankAccountLogic;
using BankAccountLogic.AccountTypes;

namespace AccountsStorage
{
    /// <summary>
    /// Binary storage of accounts
    /// </summary>
    public class BinaryFileStorage : IAccountsStorage
    {
        private readonly string _path;

        /// <summary>
        /// Initializes a new instance of the <see cref="BinaryFileStorage"/> class.
        /// </summary>
        /// <param name="path"> The path to the file. </param>
        public BinaryFileStorage(string path)
        {
            _path = path;
        }

        /// <inheritdoc />
        /// <summary>
        /// Saves accounts in the binary file storage.
        /// </summary>
        /// <param name="accounts"> Accounts to save. </param>   
        /// <exception cref="ArgumentNullException">Thrown when accounts list is null</exception>     
        public void Save(IEnumerable<BankAccount> accounts)
        {
            if (accounts == null)
                throw new ArgumentNullException(nameof(accounts));

            using (var writer = new BinaryWriter(File.Open(_path, FileMode.Create), Encoding.UTF8))
            {
                foreach (var account in accounts)
                {
                    writer.Write(account.GetType().AssemblyQualifiedName);
                    writer.Write(account.AccountNumber);
                    writer.Write(account.OwnerFirstName);
                    writer.Write(account.OwnerLastName);
                    writer.Write(account.Balance);
                    writer.Write(account.Bonus);
                }
            }
        }

        /// <inheritdoc />
        /// <summary>
        /// Loads accounts from a binary file storage.
        /// </summary>
        /// <returns> Accounts from the binary storage.</returns>
        /// <exception cref="FileNotFoundException">Thrown when storage can't be found </exception>
        public IEnumerable<BankAccount> Load()
        {
            if (!File.Exists(_path))
                throw new FileNotFoundException($"Binary file storage can't be found at {_path}.");

            var accounts = new List<BankAccount>();

            using (var reader = new BinaryReader(File.Open(_path, FileMode.Open), Encoding.UTF8))
            {
                while (reader.BaseStream.Position < reader.BaseStream.Length)
                {
                    string typeName = reader.ReadString();
                    Type type = Type.GetType(typeName);
                    string accountNumber = reader.ReadString();
                    string ownerFirstName = reader.ReadString();
                    string ownerLastName = reader.ReadString();
                    decimal balance = reader.ReadDecimal();
                    decimal bonus = reader.ReadDecimal();
                    var account = Activator.CreateInstance(type, accountNumber, ownerFirstName,
                        ownerLastName, balance, bonus) as BankAccount;
                    accounts.Add(account);
                }
            }

            return accounts;
        }
    }
}
