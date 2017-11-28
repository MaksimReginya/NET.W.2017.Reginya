using System;

namespace Task5.Solution.DocumentPartVisitors
{
    public class HtmlConverter : DocumentConverter
    {
        public override void Visit(BoldText boldText)
        {
            ConvertedDocument += "<b>" + boldText.Text + "</b>" + Environment.NewLine;
        }

        public override void Visit(Hyperlink hyperlink)
        {
            ConvertedDocument += "<a href=\"" + hyperlink.Url + "\">" + hyperlink.Text + "</a>" + Environment.NewLine;
        }

        public override void Visit(PlainText plainText)
        {
            ConvertedDocument += plainText.Text + Environment.NewLine;
        }
    }
}
