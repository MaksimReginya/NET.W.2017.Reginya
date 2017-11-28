using System;

namespace Task1.Solution
{
    public class PasswordCheckerService
    {
        private readonly IRepository _repository;        

        public PasswordCheckerService(IRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));            
        }
        
        public Tuple<bool, string> VerifyPassword(string password, IVerifier verifier)
        {
            if (verifier == null)
            {
                throw new ArgumentNullException(nameof(verifier));
            }

            var result = verifier.VerifyPassword(password);
            if (result.Item1)
            {
                _repository.Create(password);
            }
            
            return result;
        }        
    }
}
