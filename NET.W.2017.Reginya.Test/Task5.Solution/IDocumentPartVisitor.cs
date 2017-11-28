namespace Task5.Solution
{
    public interface IDocumentPartVisitor
    {
        void Visit(BoldText boldText);

        void Visit(Hyperlink hyperlink);

        void Visit(PlainText plainText);
    }
}
