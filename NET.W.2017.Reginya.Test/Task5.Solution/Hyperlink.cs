namespace Task5.Solution
{
    public class Hyperlink : DocumentPart
    {
        public string Url { get; set; }

        public override void Accept(IDocumentPartVisitor documentPartVisitor)
        {
            documentPartVisitor.Visit(this);
        }
    }
}
