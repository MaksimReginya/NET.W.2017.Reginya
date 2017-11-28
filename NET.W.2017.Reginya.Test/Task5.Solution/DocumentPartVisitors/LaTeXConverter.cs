using System;

namespace Task5.Solution.DocumentPartVisitors
{
    public class LaTeXConverter : DocumentConverter
    {
        public override void Visit(BoldText boldText)
        {
            ConvertedDocument += "\\textbf{" + boldText.Text + "}" + Environment.NewLine;
        }

        public override void Visit(Hyperlink hyperlink)
        {
            ConvertedDocument += "\\href{" + hyperlink.Url + "}{" + hyperlink.Text + "}" + Environment.NewLine;
        }

        public override void Visit(PlainText plainText)
        {
            ConvertedDocument += plainText.Text + Environment.NewLine;
        }
    }
}
