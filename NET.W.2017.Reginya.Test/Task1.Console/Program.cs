using System;
using Task1.Solution;

namespace Task1.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            var repository = new SqlRepository();
            var verifier = new PasswordVerifier();

            var checkerService = new PasswordCheckerService(repository);

            if (checkerService.VerifyPassword("", verifier).Item1)
            {
                throw new Exception();
            }

            if (checkerService.VerifyPassword("abc", verifier).Item1)
            {
                throw new Exception();
            }

            if (checkerService.VerifyPassword("abcabcabcabcabcabc", verifier).Item1)
            {
                throw new Exception();
            }

            if (checkerService.VerifyPassword("awdwadawdawd", verifier).Item1)
            {
                throw new Exception();
            }

            if (!checkerService.VerifyPassword("qwerty123", verifier).Item1)
            {
                throw new Exception();
            }
        }
    }
}
