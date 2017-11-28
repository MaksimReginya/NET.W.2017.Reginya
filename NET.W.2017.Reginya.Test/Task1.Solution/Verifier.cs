using System;
using System.Collections;
using System.Collections.Generic;

namespace Task1.Solution
{
    public class Verifier : IVerifier
    {
        #region Private fields
        
        private readonly List<IVerification> _verificatons = new List<IVerification>();

        #endregion

        #region Public constructors
                
        public Verifier()
        {            
        }

        public Verifier(IEnumerable<IVerification> verificatons)
        {
            if (!ReferenceEquals(verificatons, null))
            {
                _verificatons = new List<IVerification>(verificatons);
            }            
        }

        #endregion

        #region IEnumerable<IVerification> and IEnumerable implementation

        public IEnumerator<IVerification> GetEnumerator()
            => _verificatons.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        #endregion

        #region Public methods

        public void Add(IVerification verification)
        {
            if (ReferenceEquals(verification, null))
            {
                throw new ArgumentNullException(nameof(verification));
            }

            _verificatons.Add(verification);
        }

        #endregion
    }
}
