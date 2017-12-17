using Task2.Solution;

namespace Task2.Tests
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            RandomFileGenerator generator = new RandomBytesFileGenerator();
            generator.GenerateFiles(2, 1024);

            generator = new RandomCharsFileGenerator();
            generator.GenerateFiles(2, 1024);            
        }
    }
}
