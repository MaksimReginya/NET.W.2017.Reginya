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

            var checkerService = new PasswordCheckerService(repository, verifier);

            if (checkerService.VerifyPassword("").Item1 == true)
            {
                throw new Exception();
            }

            if (checkerService.VerifyPassword("abc").Item1 == true)
            {
                throw new Exception();
            }

            if (checkerService.VerifyPassword("abcabcabcabcabcabc").Item1 == true)
            {
                throw new Exception();
            }

            if (checkerService.VerifyPassword("awdwadawdawd").Item1 == true)
            {
                throw new Exception();
            }

            if (checkerService.VerifyPassword("qwerty123").Item1 == false)
            {
                throw new Exception();
            }
        }
    }
}
