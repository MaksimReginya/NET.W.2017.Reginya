using System;

namespace Algorithm
{
    /// <summary>
    /// Describes some predicate that is true or false
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IPredicate<in T>
    {
        /// <summary>
        /// Determines if predicate is true for data
        /// </summary>
        /// <param name="data">Predicate parameter</param>
        /// <returns>State of predicate</returns>
        bool IsTrue(T data);
    }
}
