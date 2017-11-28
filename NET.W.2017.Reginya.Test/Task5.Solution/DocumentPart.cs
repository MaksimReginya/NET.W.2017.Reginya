namespace Task5.Solution
{
    public abstract class DocumentPart
    {
        public string Text { get; set; }

        public abstract void Accept(IDocumentPartVisitor documentPartVisitor);        
    }
}
