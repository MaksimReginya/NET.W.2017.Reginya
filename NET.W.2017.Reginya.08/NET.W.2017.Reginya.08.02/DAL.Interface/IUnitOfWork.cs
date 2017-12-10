using System;

namespace DAL.Interface
{
    /// <inheritdoc />
    /// <summary>
    /// 
    /// </summary>
    public interface IUnitOfWork : IDisposable
    {
        /// <summary>
        /// 
        /// </summary>
        void Commit();        
    }
}