using System.Data.Entity;
using DAL.Interface;

namespace DAL.EF
{
    /// <inheritdoc />
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DbContext _context;

        public UnitOfWork(DbContext context)
        {
            _context = context;
        }

        /// <inheritdoc />
        public void Commit()
        {
            _context?.SaveChanges();            
        }

        /// <inheritdoc />
        public void Dispose()
        {
            _context?.Dispose();            
        }
    }
}