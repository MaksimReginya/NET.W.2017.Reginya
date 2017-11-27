using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Reader.Interfaces;

namespace Reader
{
    /// <inheritdoc />
    /// <summary>
    /// Delivers string data representing URL from a text file.
    /// </summary>
    public class FileReader : IReader<string>
    {
        #region Private fields
                
        private string _filePath;

        #endregion

        #region Public constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="FileReader"/> class.
        /// </summary>
        /// <param name="filePath">Path to the target file.</param>
        /// <exception cref="ArgumentException">Exception thrown when
        /// <paramref name="filePath"/> is invalid.</exception>
        public FileReader(string filePath)
        {
            FilePath = filePath;
        }

        #endregion

        #region Public properties
                
        /// <summary>
        /// Path to the target file.
        /// </summary>
        /// <exception cref="ArgumentException">
        /// Thrown when path to file in <paramref name="value"/> is not valid.
        /// </exception>
        public string FilePath
        {
            get => _filePath;

            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException($"{nameof(FilePath)} can't be null or empty.", nameof(FilePath));
                }

                _filePath = value;
            }
        }

        #endregion

        #region IReader implementation
                
        /// <inheritdoc />
        public IEnumerable<string> GetData()
        {
            using (var streamReader = new StreamReader(File.Open(FilePath, FileMode.Open), Encoding.UTF8))
            {
                while (!streamReader.EndOfStream)
                {
                    yield return streamReader.ReadLine()?.Trim();
                }
            }
        }

        #endregion
    }
}
