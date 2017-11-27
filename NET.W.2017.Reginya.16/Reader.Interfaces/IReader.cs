using System.Collections.Generic;

namespace Reader.Interfaces
{
    /// <summary>
    /// Interface for data readers.
    /// </summary>    
    public interface IReader<out T>
    {
        /// <summary>
        /// Gets the data from some storage.
        /// </summary>
        /// <returns>Set of data elements.</returns>
        IEnumerable<T> GetData();
    }
}
