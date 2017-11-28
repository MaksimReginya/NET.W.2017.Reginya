namespace Task5.Solution
{
    public class PlainText : DocumentPart
    {
        public override void Accept(IDocumentPartVisitor documentPartVisitor)
        {
            documentPartVisitor.Visit(this);
        }
    }
}
