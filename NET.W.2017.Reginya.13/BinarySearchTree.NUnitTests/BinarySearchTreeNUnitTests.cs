using System;
using System.Collections.Generic;
using System.Text;
using BinarySearchTree.NUnitTests.Comparers;
using BookLogic;
using NUnit.Framework;

namespace BinarySearchTree.NUnitTests
{
    [TestFixture]
    public class BinarySearchTreeNUnitTests
    {
        public static IEnumerable<TestCaseData> InsertAscendingIntTestCaseData
        {
            get
            {
                yield return new TestCaseData(null, new[] { 1, 2, 3, 4, 5 }).Returns("12345");
                yield return new TestCaseData(null, new[] { 2, 4, 1, 3, 5 }).Returns("12345");
                yield return new TestCaseData(null, new[] { 3, 3, 2, 1, 5 }).Returns("12335");
            }
        }

        public static IEnumerable<TestCaseData> InsertDescendingIntTestCaseData
        {
            get
            {
                yield return new TestCaseData(
                        new IntDescendingComparer(),
                        new[] { 1, 2, 3, 4, 5 })
                    .Returns("54321");
                yield return new TestCaseData(
                        new IntDescendingComparer(),
                        new[] { 2, 4, 1, 3, 5 })
                    .Returns("54321");
                yield return new TestCaseData(
                        new IntDescendingComparer(),
                        new[] { 3, 3, 2, 1, 5 })
                    .Returns("53321");
            }
        }
        
        public static IEnumerable<TestCaseData> InsertStringTestCaseData
        {
            get
            {
                yield return new TestCaseData(null, new[] { "a", "A", "b", "B", "c" }).Returns("aAbBc");
                yield return new TestCaseData(null, new[] { "A", "B", "a", "b", "c" }).Returns("aAbBc");
                yield return new TestCaseData(null, new[] { "b", "b", "A", "a", "c" }).Returns("aAbbc");
            }
        }

        public static IEnumerable<TestCaseData> InsertStringIgnoreCaseTestCaseData
        {
            get
            {
                yield return new TestCaseData(
                        new StringComparerIgnoreCase(),
                        new[] { "a", "A", "b", "B", "c" })
                    .Returns("aAbBc");
                yield return new TestCaseData(
                        new StringComparerIgnoreCase(),
                        new[] { "A", "B", "a", "b", "c" })
                    .Returns("AaBbc");
                yield return new TestCaseData(
                        new StringComparerIgnoreCase(),
                        new[] { "b", "b", "A", "a", "c" })
                    .Returns("Aabbc");
            }
        }

        public static IEnumerable<TestCaseData> InsertBookTestCaseData
        {
            get
            {
                yield return new TestCaseData(
                        null,
                        new[] 
                        {
                            new Book("999-9-99-999999-0", "e", "a", "1", 1, 1, 1d),
                            new Book("999-9-99-999999-1", "d", "b", "1", 1, 1, 1d),
                            new Book("999-9-99-999999-2", "c", "c", "1", 1, 1, 1d),
                            new Book("999-9-99-999999-3", "b", "d", "1", 1, 1, 1d),
                            new Book("999-9-99-999999-4", "a", "e", "1", 1, 1, 1d)                            
                        })
                    .Returns("abcde");                
            }
        }

        public static IEnumerable<TestCaseData> InsertBookByAuthorTestCaseData
        {
            get
            {
                yield return new TestCaseData(
                        new BookComparerByAuthor(),
                        new[] 
                        {
                            new Book("999-9-99-999999-0", "e", "a", "1", 1, 1, 1d),
                            new Book("999-9-99-999999-1", "d", "b", "1", 1, 1, 1d),
                            new Book("999-9-99-999999-2", "c", "c", "1", 1, 1, 1d),
                            new Book("999-9-99-999999-3", "b", "d", "1", 1, 1, 1d),
                            new Book("999-9-99-999999-4", "a", "e", "1", 1, 1, 1d)
                        })
                    .Returns("edcba");
            }
        }

        public static IEnumerable<TestCaseData> InsertPointTestCaseData
        {
            get
            {
                yield return new TestCaseData(
                    null,
                    new Point[] { new Point(1, 1), new Point(2, 2), new Point(3, 3), new Point(3, 3), new Point(4, 4) });                    
                yield return new TestCaseData(
                    null,
                    new Point[] { new Point(8, 8), new Point(2, 2), new Point(3, 3), new Point(3, 3), new Point(4, 4) });
                yield return new TestCaseData(
                    null,
                    new Point[] { new Point(5, 5), new Point(3, 3), new Point(2, 1), new Point(4, 4), new Point(8, 8) });
            }
        }

        public static IEnumerable<TestCaseData> InsertPointOuterComparerTestCaseData
        {
            get
            {
                yield return new TestCaseData(
                        new PointComparer(),
                        new Point[] { new Point(1, 1), new Point(2, 2), new Point(3, 3), new Point(3, 3), new Point(4, 4) })
                    .Returns("1122333344");
                yield return new TestCaseData(
                        new PointComparer(),
                        new Point[] { new Point(8, 8), new Point(2, 2), new Point(3, 3), new Point(3, 3), new Point(4, 4) })
                    .Returns("2233334488");
                yield return new TestCaseData(
                        new PointComparer(),
                        new Point[] { new Point(5, 5), new Point(3, 3), new Point(2, 1), new Point(4, 4), new Point(8, 8) })
                    .Returns("2133445588");
            }
        }

        [Test]
        public void ConstructorTest()
        {
            BinarySearchTree<int> treeInt;
            Assert.DoesNotThrow(() => treeInt = new BinarySearchTree<int>());
            BinarySearchTree<string> treeString;
            Assert.DoesNotThrow(() => treeString = new BinarySearchTree<string>());
            BinarySearchTree<Book> treeBook;
            Assert.DoesNotThrow(() => treeBook = new BinarySearchTree<Book>());
        }

        [TestCase(3, 2, 1, ExpectedResult = "321")]
        [TestCase(1, 2, 3, ExpectedResult = "123")]
        [TestCase(2, 4, 1, 3, 5, ExpectedResult = "21435")]
        [TestCase(3, 3, 2, 1, 5, ExpectedResult = "32135")]
        public string InsertTestPreorder(params int[] array)
        {
            var tree = new BinarySearchTree<int>();
            foreach (var value in array)
            {
                tree.Insert(value);
            }

            var sb = new StringBuilder();
            foreach (var value in tree.Preorder())
            {
                sb.Append(value);
            }

            return sb.ToString();
        }

        [Test, TestCase(3, 2, 1, ExpectedResult = "123")]
        [TestCase(1, 2, 3, ExpectedResult = "123")]
        [TestCase(2, 4, 1, 3, 5, ExpectedResult = "12345")]
        [TestCase(3, 3, 2, 1, 5, ExpectedResult = "12335")]
        public string InsertTestInorder(params int[] array)
        {
            var tree = new BinarySearchTree<int>();
            foreach (var value in array)
            {
                tree.Insert(value);
            }

            var sb = new StringBuilder();
            foreach (var value in tree)
            {
                sb.Append(value);
            }

            return sb.ToString();
        }

        [Test, TestCase(3, 2, 1, ExpectedResult = "123")]
        [TestCase(1, 2, 3, ExpectedResult = "321")]
        [TestCase(2, 4, 1, 3, 5, ExpectedResult = "13542")]
        [TestCase(3, 3, 2, 1, 5, ExpectedResult = "12533")]
        public string InsertTestPostorder(params int[] array)
        {
            var tree = new BinarySearchTree<int>();
            foreach (var value in array)
            {
                tree.Insert(value);
            }

            var sb = new StringBuilder();
            foreach (var value in tree.Postorder())
            {
                sb.Append(value);
            }

            return sb.ToString();
        }

        [Test, TestCaseSource(nameof(InsertAscendingIntTestCaseData))]
        public string InsertTestIntNativeComparer(IComparer<int> comparer, params int[] array)
        {
            BinarySearchTree<int> tree = new BinarySearchTree<int>();
            foreach (var value in array)
            {
                tree.Insert(value);
            }

            var sb = new StringBuilder();
            foreach (var value in tree)
            {
                sb.Append(value);
            }

            return sb.ToString();
        }

        [Test, TestCaseSource(nameof(InsertDescendingIntTestCaseData))]
        public string InsertTestIntOuterComparer(IComparer<int> comparer, params int[] array)
        {
            BinarySearchTree<int> tree = new BinarySearchTree<int>(comparer);
            foreach (var value in array)
            {
                tree.Insert(value);
            }

            var sb = new StringBuilder();
            foreach (var value in tree)
            {
                sb.Append(value);
            }

            return sb.ToString();
        }

        [Test, TestCaseSource(nameof(InsertStringTestCaseData))]
        public string InsertTestStringNativeComparer(IComparer<string> comparer, params string[] array)
        {
            BinarySearchTree<string> tree = new BinarySearchTree<string>();
            foreach (var value in array)
            {
                tree.Insert(value);
            }

            var sb = new StringBuilder();
            foreach (var value in tree)
            {
                sb.Append(value);
            }

            return sb.ToString();
        }

        [Test, TestCaseSource(nameof(InsertStringIgnoreCaseTestCaseData))]
        public string InsertTestStringOuterComparer(IComparer<string> comparer, params string[] array)
        {
            BinarySearchTree<string> tree = new BinarySearchTree<string>(comparer);
            foreach (var value in array)
            {
                tree.Insert(value);
            }

            var sb = new StringBuilder();
            foreach (var value in tree)
            {
                sb.Append(value);
            }

            return sb.ToString();
        }
                
        [Test, TestCaseSource(nameof(InsertBookTestCaseData))]
        public string InsertTestBookNativeComparer(IComparer<Book> comparer, params Book[] array)
        {
            BinarySearchTree<Book> tree = new BinarySearchTree<Book>();
            foreach (var value in array)
            {
                tree.Insert(value);
            }

            var sb = new StringBuilder();
            foreach (var value in tree)
            {
                sb.Append(value.Title);
            }

            return sb.ToString();
        }

        [Test, TestCaseSource(nameof(InsertBookByAuthorTestCaseData))]
        public string InsertTestBookOuterComparer(IComparer<Book> comparer, params Book[] array)
        {
            var tree = new BinarySearchTree<Book>(comparer);
            foreach (var value in array)
            {
                tree.Insert(value);
            }

            var sb = new StringBuilder();
            foreach (var value in tree)
            {
                sb.Append(value.Title);
            }

            return sb.ToString();
        }

        [Test, TestCaseSource(nameof(InsertPointTestCaseData))]
        public void InsertTestPointNativeComparer(IComparer<Point> comparer, params Point[] array)
        {
            Assert.Throws<ArgumentNullException>(() => new BinarySearchTree<Point>());           
        }

        [Test, TestCaseSource(nameof(InsertPointOuterComparerTestCaseData))]
        public string InsertTestPointOuterComparer(IComparer<Point> comparer, params Point[] array)
        {
            var tree = new BinarySearchTree<Point>(comparer);
            foreach (var value in array)
            {
                tree.Insert(value);
            }

            var sb = new StringBuilder();
            foreach (var value in tree)
            {
                sb.Append(value);
            }

            return sb.ToString();
        }
    }
}
