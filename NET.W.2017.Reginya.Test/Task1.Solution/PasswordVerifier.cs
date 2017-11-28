using System;
using System.Collections.Generic;
using System.Linq;

namespace Task1.Solution
{
    public class PasswordVerifier : IVerifier
    {
        public Tuple<bool, string> VerifyPassword(string password)
        {
            if (password == null)
                throw new ArgumentException($"{nameof(password)} is null arg");

            if (password == string.Empty)
                return Tuple.Create(false, $"{nameof(password)} is empty ");
            
            if (password.Length <= 7)
                return Tuple.Create(false, $"{nameof(password)} length too short");
            
            if (password.Length >= 15)
                return Tuple.Create(false, $"{nameof(password)} length too long");
            
            if (!password.Any(char.IsLetter))
                return Tuple.Create(false, $"{nameof(password)} hasn't alphanumerical chars");

            if (!password.Any(char.IsNumber))
                return Tuple.Create(false, $"{nameof(password)} hasn't digits");

            return Tuple.Create(true, "Password is Ok. User was created");
        }
    }
}
