using System;
using System.Collections.Generic;
using BookLogic;
using NUnit.Framework;

namespace BookLogic.NUnitTests
{
    [TestFixture]
    public class BookTests
    {
        #region ToString test

        public static IEnumerable<TestCaseData> ToStringTestData
        {
            get
            {
                yield return new TestCaseData(string.Empty).Returns("ISBN 13: 978-0-7356-6745-7, Jeffrey Richter, CLR via C#, Microsoft Press, 2012, 826, 59,99р.");
                yield return new TestCaseData("AT").Returns("Jeffrey Richter, CLR via C#");
                yield return new TestCaseData("ATPY").Returns("Jeffrey Richter, CLR via C#, Microsoft Press, 2012");
                yield return new TestCaseData("IATPYPC").Returns("ISBN 13: 978-0-7356-6745-7, Jeffrey Richter, CLR via C#, Microsoft Press, 2012, 826");                
            }
        }

        [Test, TestCaseSource(nameof(ToStringTestData))]
        public string BookToStringTests(string format)
        {
            var book = new Book("978-0-7356-6745-7", "Jeffrey Richter", "CLR via C#", "Microsoft Press", 2012, 826, 59.99d);

            return book.ToString(format);
        }

        [TestCase("W")]
        [TestCase("IA")]
        [TestCase("IATPYC")]
        public void BookToStringFormatExceptionTests(string format)
        {
            var book = new Book("978-0-7356-6745-7", "Jeffrey Richter", "CLR via C#", "Microsoft Press", 2012, 826, 59.99d);

            Assert.Throws<FormatException>(() => book.ToString(format));
        }

        #endregion 

        #region Custom formatter tests

        [TestCase("{0:IAT}", ExpectedResult = "ISBN 13: 978-0-7356-6745-7, Jeffrey Richter, CLR via C#")]        
        public string BookToStringCustomFormatterTests(string format)
        {
            var book = new Book("978-0-7356-6745-7", "Jeffrey Richter", "CLR via C#", "Microsoft Press", 2012, 826, 59.99d);

            return string.Format(new CustomBookFormatter(), format, book);
        }

        [TestCase("{0:IATP}")]        
        public void BookToStringCustomFormatterFormatExceptionTests(string format)
        {
            var book = new Book("978-0-7356-6745-7", "Jeffrey Richter", "CLR via C#", "Microsoft Press", 2012, 826, 59.99d);

            Assert.Throws<FormatException>(() => string.Format(new CustomBookFormatter(), format, book));
        }

        #endregion 
    }
}
