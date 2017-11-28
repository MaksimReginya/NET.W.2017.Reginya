using System;

namespace Task1.Solution
{
    public interface IVerifier
    {
        Tuple<bool, string> VerifyPassword(string password);
    }
}
