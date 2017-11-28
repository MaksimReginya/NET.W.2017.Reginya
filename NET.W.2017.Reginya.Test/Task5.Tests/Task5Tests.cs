using System.Collections.Generic;
using Moq;
using NUnit.Framework;
using Task5.Solution;
using Task5.Solution.DocumentPartVisitors;

namespace Task5.Tests
{
    [TestFixture]
    public class Task5Tests
    {
        [Test]
        public void DocumentConverter_VisitMethod()
        {
            var parts = new List<DocumentPart>
            {
                new PlainText { Text = "Some plain text" },
                new Hyperlink { Text = "google.com", Url = "https://www.google.by/" },
                new BoldText { Text = "Some bold text" }
            };

            var documentConverterMock = new Mock<DocumentConverter>();
            var document = new Document(parts);

            document.Convert(documentConverterMock.Object);

            documentConverterMock.Verify(visitor => visitor.Visit(It.Is<PlainText>(text => text.Text.Equals(parts[0].Text))), Times.Once);
            documentConverterMock.Verify(visitor => visitor.Visit(It.Is<Hyperlink>(text => text.Text.Equals(parts[1].Text))), Times.Once);
            documentConverterMock.Verify(visitor => visitor.Visit(It.Is<BoldText>(text => text.Text.Equals(parts[2].Text))), Times.Once);
        }

        [Test]
        public void BoldText_AcceptMethod()
        {
            var documentConverterMock = new Mock<DocumentConverter>();
            var boldText = new BoldText { Text = "Some bold text" };

            boldText.Accept(documentConverterMock.Object);

            documentConverterMock.Verify(visitor => visitor.Visit(It.Is<BoldText>(text => text.Text.Equals(boldText.Text))), Times.Once);
        }

        [Test]
        public void Hyperlink_AcceptMethod()
        {
            var documentConverterMock = new Mock<DocumentConverter>();
            var hyperlink = new Hyperlink { Text = "google.com", Url = "https://www.google.by/" };

            hyperlink.Accept(documentConverterMock.Object);

            documentConverterMock.Verify(visitor => visitor.Visit(It.Is<Hyperlink>(text => text.Text.Equals(hyperlink.Text))), Times.Once);
            documentConverterMock.Verify(visitor => visitor.Visit(It.Is<Hyperlink>(text => text.Url.Equals(hyperlink.Url))), Times.Once);
        }

        [Test]
        public void PlainText_AcceptMethod()
        {
            var documentConverterMock = new Mock<DocumentConverter>();
            var plainText = new PlainText { Text = "Some plain text" };

            plainText.Accept(documentConverterMock.Object);

            documentConverterMock.Verify(visitor => visitor.Visit(It.Is<PlainText>(text => text.Text.Equals(plainText.Text))), Times.Once);
        }
    }
}
