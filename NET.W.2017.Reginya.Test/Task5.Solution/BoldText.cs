namespace Task5.Solution
{
    public class BoldText : DocumentPart
    {
        public override void Accept(IDocumentPartVisitor documentPartVisitor)
        {
            documentPartVisitor.Visit(this);
        }
    }
}