using Task5.Solution;
using Task5.Solution.DocumentPartVisitors;

namespace Task5.Console
{
    using System.Collections.Generic;
    using System;    

    class Program
    {
        static void Main(string[] args)
        {
            var parts = new List<DocumentPart>
            {
                new PlainText {Text = "Some plain text"},
                new Hyperlink {Text = "google.com", Url = "https://www.google.by/"},
                new BoldText {Text = "Some bold text"}
            };

            var document = new Document(parts);
            
            Console.WriteLine(document.Convert(new HtmlConverter()));            
            Console.WriteLine(document.Convert(new LaTeXConverter()));            
            Console.WriteLine(document.Convert(new PlainTextConverter()));

            Console.ReadLine();
        }
    }
}
