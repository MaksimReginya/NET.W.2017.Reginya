using System.Collections;
using System.Collections.Generic;

namespace Task1.Solution
{
    public interface IVerifier : IEnumerable<IVerification>, IEnumerable
    {
        void Add(IVerification verification);
    }
}
