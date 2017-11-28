namespace Task5.Solution
{
    public abstract class DocumentPart
    {
        public abstract void Accept(IDocumentPartVisitor documentPartVisitor);

        public string Text { get; set; }
    }
}
