using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task5.Solution
{
    public class DocumentConverter : IConverter
    {        
        public string Convert(Document document, ConversionType conversionType)
        {
            switch (conversionType)
            {
                case ConversionType.Html:
                    return Convert(document, ToHtml);
                case ConversionType.PlainText:
                    return Convert(document, ToPlainText);
                case ConversionType.LaTeX:
                    return Convert(document, ToLaTeX);
                default:
                    throw new InvalidOperationException();
            }
        }

        private string Convert(Document document, Func<DocumentPart, string> convertion)
        {
            string output = string.Empty;

            foreach (DocumentPart part in document.Parts)
            {
                output += $"{convertion(part)}\n";
            }

            return output;
        }

        private string ToHtml(DocumentPart part)
            => part.ToHtml();

        private string ToPlainText(DocumentPart part)
            => part.ToPlainText();

        private string ToLaTeX(DocumentPart part)
            => part.ToLaTeX();
    }
}
