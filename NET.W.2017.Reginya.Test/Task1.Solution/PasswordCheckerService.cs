using System;

namespace Task1.Solution
{
    public class PasswordCheckerService
    {
        private readonly IRepository _repository;
        private readonly IVerifier _verifier;

        public PasswordCheckerService(IRepository repository, IVerifier verifier)
        {
            VerifyParameteres(repository, verifier);
            _repository = repository;
            _verifier = verifier;
        }
        
        public Tuple<bool, string> VerifyPassword(string password)
        {
            var result = _verifier.VerifyPassword(password);
            if (result.Item1 == false)
            {
                return result;
            }

            _repository.Create(password);

            return result;
        }

        private void VerifyParameteres(IRepository repository, IVerifier verifier)
        {
            if (repository == null)
            {
                throw new ArgumentNullException(nameof(repository));
            }

            if (verifier == null)
            {
                throw new ArgumentNullException(nameof(verifier));
            }
        }
    }
}
