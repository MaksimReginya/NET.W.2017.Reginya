using System;
using System.Linq;
using NUnit.Framework;

namespace Queue.NUnitTests
{
    [TestFixture]
    public class QueueNUnitTests
    {                        
        [TestCase(new[] { 1, 2, 3, 4, 5 })]
        [TestCase(new[] { -1, -2, -3, -4, -5 })]
        [TestCase(new[] { 1 })]
        public void ClearTest(int[] array)
        {
            var queue = new Queue<int>(array);
            queue.Clear();
            Assert.AreEqual(0, queue.Count);
        }

        [TestCase(new[] { 1, 2, 3, 4, 5 }, 6)]
        [TestCase(new[] { -1, -2, -3, -4, -5 }, -6)]
        [TestCase(new[] { 1 }, 2)]
        public void EnqueueTest(int[] array, int item)
        {
            var queue = new Queue<int>(array);
            queue.Enqueue(item);            
            Assert.IsTrue(queue.Contains(item));
            Assert.IsTrue(queue.Dequeue().Equals(array[0]));
        }

        [TestCase(new[] { 1, 2, 3, 4, 5 })]
        [TestCase(new[] { -1, -2, -3, -4, -5 })]
        [TestCase(new[] { 1 })]
        public void DequeueTest(int[] array)
        {
            var queue = new Queue<int>(array);
            var element = queue.Dequeue();
            Assert.AreEqual(array[0], element);
            Assert.AreEqual(array.Length - 1, queue.Count);
        }

        [TestCase(new[] { 1, 2, 3, 4, 5 })]
        [TestCase(new[] { -1, -2, -3, -4, -5 })]
        [TestCase(new[] { 1 })]
        public void PeekTest(int[] array)
        {
            var queue = new Queue<int>(array);
            var element = queue.Peek();
            Assert.AreEqual(array[0], element);
            Assert.AreEqual(array.Length, queue.Count);
        }

        [TestCase(new[] { 1, 2, 3, 4, 5 })]
        [TestCase(new[] { -1, -2, -3, -4, -5 })]
        [TestCase(new[] { 1 })]
        public void IsEmptyTest(int[] array)
        {
            var queue = new Queue<int>(array);
            queue.Clear();
            Assert.AreEqual(queue.Count, 0);
            Assert.IsTrue(queue.IsEmpty());
        }

        [TestCase(new[] { 1, 2, 3, 4, 5 })]        
        public void ForeachTest(int[] array)
        {
            var queue = new Queue<int>(array);
            int i = 1;
            foreach (var element in queue)
            {
                Assert.AreEqual(i++, element);
            }

            var iterator = queue.GetEnumerator();
            i = 1;
            while (iterator.MoveNext())
            {
                Assert.AreEqual(i++, iterator.Current);
            }

            iterator.Dispose();
        }

        [TestCase(new[] { 1, 2, 3, 4, 5 })]
        public void ForeachInvalidOperationExceptionTest(int[] array)
        {
            var queue = new Queue<int>(array);
            Assert.Throws<InvalidOperationException>(() =>
            {
                foreach (var element in queue)
                {
                    queue.Dequeue();
                }
            });
        }
    }
}
