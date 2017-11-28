using System.Collections.Generic;

namespace Task5.Solution.DocumentPartVisitors
{
    public abstract class DocumentConverter : IDocumentPartVisitor
    {
        public string ConvertedDocument { get; protected set; } = string.Empty;

        public abstract void Visit(BoldText boldText);

        public abstract void Visit(Hyperlink hyperlink);

        public abstract void Visit(PlainText plainText);

        public string ConvertDocument(IEnumerable<DocumentPart> parts)
        {
            foreach (var part in parts)
            {                
                part.Accept(this);
            }

            return ConvertedDocument;
        }
    }
}
