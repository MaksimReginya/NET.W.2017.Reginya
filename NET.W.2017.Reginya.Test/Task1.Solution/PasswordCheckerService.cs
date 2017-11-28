using System;
using System.Text;

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
            
            var result = new StringBuilder();            
            foreach (var verification in verifier)
            {
                var temp = verification.VerifyPassword(password);
                if (!temp.Item1)
                {                    
                    return new Tuple<bool, string>(false, temp.Item2);
                }

                result.Append(temp.Item2);
            }
                        
            _repository.Create(password);                        
            return new Tuple<bool, string>(true, result.ToString());
        }        
    }
}
