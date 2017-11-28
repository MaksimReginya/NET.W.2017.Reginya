using Task5.Solution;

namespace Task5.Console
{
    using System.Collections.Generic;
    using System;    

    class Program
    {
        static void Main(string[] args)
        {
            List<DocumentPart> parts = new List<DocumentPart>
                {
                    new PlainText {Text = "Some plain text"},
                    new Hyperlink {Text = "google.com", Url = "https://www.google.by/"},
                    new BoldText {Text = "Some bold text"}
                };

            var document = new Document(parts);
            var converter = new DocumentConverter();

            Console.WriteLine(converter.Convert(document, ConversionType.Html));

            Console.WriteLine(converter.Convert(document, ConversionType.PlainText));

            Console.WriteLine(converter.Convert(document, ConversionType.LaTeX));

            Console.ReadKey();
        }
    }
}
