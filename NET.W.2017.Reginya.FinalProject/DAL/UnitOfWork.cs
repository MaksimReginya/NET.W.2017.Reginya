using System;
using System.Data.Entity;
using DAL.Interface;

namespace DAL
{
    /// <inheritdoc cref="IUnitOfWork"/>
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        #region Private fields
                
        private readonly DbContext _context;
        private bool _disposed;

        #endregion

        #region Public constructors
        
        /// <summary>
        /// Initializes a new instance of the <see cref="UnitOfWork"/> class.
        /// </summary>
        /// <param name="context"></param>
        public UnitOfWork(DbContext context)
        {
            _context = context;
        }

        #endregion

        #region Implementation of IUnitOfWork

        /// <inheritdoc />
        public void Commit()
        {
            if (_disposed)
            {
                throw new ObjectDisposedException(nameof(UnitOfWork));
            }

            _context?.SaveChanges();
        }

        /// <inheritdoc />
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion

        #region Protected methods
                
        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
            {
                return;
            }

            try
            {
                if (disposing)
                {
                    _context?.Dispose();
                }
            }
            finally
            {
                _disposed = true;
            }
        }

        #endregion
    }
}
