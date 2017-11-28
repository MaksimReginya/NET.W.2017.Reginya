namespace Task1.Console
{
    using System;
    using Solution;

    internal class Program
    {
        static void Main(string[] args)
        {
            var repository = new SqlRepository();
            var verifier = new Verifier
            {
                new PasswordVerification()
            };

            var checkerService = new PasswordCheckerService(repository);

            Console.WriteLine(checkerService.VerifyPassword("", verifier).Item2);
            Console.WriteLine(checkerService.VerifyPassword("abc", verifier).Item2);            
            Console.WriteLine(checkerService.VerifyPassword("abcabcabcabcabcabc", verifier).Item2);            
            Console.WriteLine(checkerService.VerifyPassword("awdwadawdawd", verifier).Item2);            
            Console.WriteLine(checkerService.VerifyPassword("qwerty123", verifier).Item2);

            Console.ReadKey();
        }
    }
}
