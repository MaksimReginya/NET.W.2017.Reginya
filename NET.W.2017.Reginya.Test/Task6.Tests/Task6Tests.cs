using NUnit.Framework;
using Test6.Solution;

namespace Task6.Tests
{
    [TestFixture]
    public class Task6Tests
    {
        [Test]
        public void Generator_ForSequence1_WithDelegate()
        {
            int[] expected = { 1, 1, 2, 3, 5, 8, 13, 21, 34, 55 };            
            int i = 0;
            var actual = Generator<int>.GenerateSequence(1, 1, 10, (first, second) => first + second);
            foreach (var el in actual)
            {
                Assert.AreEqual(el, expected[i++]);
            }
        }

        [Test]
        public void Generator_ForSequence1_WithInterface()
        {
            int[] expected = { 1, 1, 2, 3, 5, 8, 13, 21, 34, 55 };
            int i = 0;
            var actual = Generator<int>.GenerateSequence(1, 1, 10, new Calculation1());
            foreach (var el in actual)
            {
                Assert.AreEqual(el, expected[i++]);
            }
        }

        [Test]
        public void Generator_ForSequence2_WithDelegate()
        {
            int[] expected = { 1, 2, 4, 8, 16, 32, 64, 128, 256, 512 };            
            int i = 0;
            var actual = Generator<int>.GenerateSequence(1, 2, 10, (first, second) => 6 * second - 8 * first);
            foreach (var el in actual)
            {
                Assert.AreEqual(el, expected[i++]);
            }
        }

        [Test]
        public void Generator_ForSequence2_WithInterface()
        {
            int[] expected = { 1, 2, 4, 8, 16, 32, 64, 128, 256, 512 };
            int i = 0;
            var actual = Generator<int>.GenerateSequence(1, 2, 10, new Calculation2());
            foreach (var el in actual)
            {
                Assert.AreEqual(el, expected[i++]);
            }
        }

        [Test]
        public void Generator_ForSequence3_WithDelegate()
        {            
            double[] expected = { 1, 2, 2.5, 3.3, 4.05757575757576, 4.87086926018965, 5.70389834408211, 6.55785277425587, 7.42763417076325, 8.31053343902137 };            
            int i = 0;
            var actual = Generator<double>.GenerateSequence(1, 2, 10, (first, second) => second + first / second);
            foreach (var el in actual)
            {
                Assert.AreEqual(el, expected[i++], 0.0001);
            }
        }

        [Test]
        public void Generator_ForSequence3_WithInterface()
        {            
            double[] expected = { 1, 2, 2.5, 3.3, 4.05757575757576, 4.87086926018965, 5.70389834408211, 6.55785277425587, 7.42763417076325, 8.31053343902137 };
            int i = 0;
            var actual = Generator<double>.GenerateSequence(1, 2, 10, new Calculation3());
            foreach (var el in actual)
            {
                Assert.AreEqual(el, expected[i++], 0.0001);
            }
        }
    }
}
