using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task2.Solution;

namespace Task2.Tests
{
    class Program
    {
        static void Main(string[] args)
        {
            RandomFileGenerator generator = new RandomBytesFileGenerator();
            generator.GenerateFiles(2, 1024);

            generator = new RandomCharsFileGenerator();
            generator.GenerateFiles(2, 1024);            
        }
    }
}
