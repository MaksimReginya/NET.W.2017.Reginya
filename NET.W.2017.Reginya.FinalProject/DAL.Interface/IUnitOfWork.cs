namespace DAL.Interface
{    
    /// <summary>
    /// Unit of work pattern for 
    /// <see cref="IBankAccountRepository"/> and <see cref="IAccountOwnerRepository"/>.
    /// </summary>
    public interface IUnitOfWork
    {
        /// <summary>
        /// Commits all pending changes.
        /// </summary>
        void Commit();        
    }
}