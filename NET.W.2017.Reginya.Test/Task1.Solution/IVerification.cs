using System;

namespace Task1.Solution
{
    public interface IVerification
    {
        Tuple<bool, string> VerifyPassword(string password);
    }
}
