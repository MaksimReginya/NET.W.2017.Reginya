using System;

namespace Task5.Solution.DocumentPartVisitors
{
    public class PlainTextConverter : DocumentConverter
    {
        public override void Visit(BoldText boldText)
        {
            ConvertedDocument += "**" + boldText.Text + "**" + Environment.NewLine;
        }

        public override void Visit(Hyperlink hyperlink)
        {
            ConvertedDocument += hyperlink.Text + " [" + hyperlink.Url + "]" + Environment.NewLine;
        }

        public override void Visit(PlainText plainText)
        {
            ConvertedDocument += plainText.Text + Environment.NewLine;
        }
    }
}
