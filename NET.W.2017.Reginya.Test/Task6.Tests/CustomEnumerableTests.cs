using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Test6.Solution;

namespace Task6.Tests
{
    [TestFixture]
    public class CustomEnumerableTests
    {
        [Test]
        public void Generator_ForSequence1()
        {
            int[] expected = { 1, 1, 2, 3, 5, 8, 13, 21, 34, 55 };
            var generator = new Generator<int>((first, second) => first + second);

            int i = 0;
            var actual = generator.Generate(1, 1, 10);
            foreach (var el in actual)
            {
                Assert.AreEqual(el, expected[i++]);
            }
        }

        [Test]
        public void Generator_ForSequence2()
        {
            int[] expected = { 1, 2, 4, 8, 16, 32, 64, 128, 256, 512 };

            var generator = new Generator<int>((first, second) => 6*second - 8*first);

            int i = 0;
            var actual = generator.Generate(1, 2, 10);
            foreach (var el in actual)
            {
                Assert.AreEqual(el, expected[i++]);
            }
        }

        [Test]
        public void Generator_ForSequence3()
        {
            double eps = 0.0001;
            double[] expected = { 1, 2, 2.5, 3.3, 4.05757575757576, 4.87086926018965, 5.70389834408211, 6.55785277425587, 7.42763417076325, 8.31053343902137 };

            var generator = new Generator<double>((first, second) => second + first/second);

            int i = 0;
            var actual = generator.Generate(1, 2, 10);
            foreach (var el in actual)
            {
                Assert.IsTrue(Math.Abs(el - expected[i++]) < eps);
            }
        }
    }
}
