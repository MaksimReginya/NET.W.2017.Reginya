using System;
using System.Text;
using System.Web;
using System.Xml;
using Converter.Interfaces;

namespace Converter
{
    /// <inheritdoc />
    /// <summary>
    /// Converts url to xml format.
    /// </summary>
    public class UrlToXmlTransformer : IXmlTransformer
    {
        #region Public constants

        public const string RootElement = "urlAddresses";

        #endregion

        #region Public methods

        public XmlNode GetRootElement(XmlDocument xmlDocument)
            => xmlDocument.CreateElement(RootElement);

        /// <inheritdoc />
        /// <summary>
        /// Сonverts url string to xml format.
        /// </summary>
        /// <param name="data">String with url address.</param>
        /// <param name="xmlDocument">Document that contains resulting node.</param>
        /// <returns>Xml representation of url address.</returns>
        /// <exception cref="ArgumentException">
        /// Thrown when url in <paramref name="data"/> is not valid.
        /// </exception>
        public XmlNode Transform(string data, XmlDocument xmlDocument)
        {
            VerifyUrl(data);

            var result = new StringBuilder(data.Length);
            var uri = new Uri(data);            

            result.Append("<urlAddress>");

            AppendHost(result, uri);
            AppendUriPath(result, uri);
            AppendParameters(result, uri);

            result.Append("</urlAddress>");

            var xmlDocumentFragment = xmlDocument.CreateDocumentFragment();
            xmlDocumentFragment.InnerXml = result.ToString();

            return xmlDocumentFragment;
        }

        #endregion

        #region Private methods

        private static void VerifyUrl(string data)
        {
            if (string.IsNullOrWhiteSpace(data))
            {
                throw new ArgumentException("Url address can't be null or empty.", nameof(data));
            }

            if (!Uri.IsWellFormedUriString(data, UriKind.RelativeOrAbsolute))
            {
                throw new ArgumentException("Bad format of url address.", nameof(data));
            }
        }

        private static void AppendHost(StringBuilder result, Uri uri)
        {
            result.Append($"<host name=\"{uri.Host}\"/>");
        }
        
        private static void AppendUriPath(StringBuilder result, Uri uri)
        {
            if (uri.Segments.Length <= 1)
            {
                return;
            }

            result.Append("<uri>");
            foreach (var segment in uri.Segments)
            {
                var temp = segment.Trim('/', ' ');
                if (!string.IsNullOrWhiteSpace(temp))
                {
                    result.Append($"<segment>{temp}</segment>");
                }
            }

            result.Append("</uri>");
        }

        private static void AppendParameters(StringBuilder result, Uri uri)
        {
            if (string.IsNullOrWhiteSpace(uri.Query))
            {
                return;
            }

            result.Append("<parameters>");

            var keyValueCollection = HttpUtility.ParseQueryString(uri.Query);
            foreach (var key in keyValueCollection.AllKeys)
            {
                var xmlParameter = $"<parameter value=\"{keyValueCollection[key]}\" key=\"{key}\"/>";
                result.Append(xmlParameter);
            }          

            result.Append("</parameters>");
        }
        
        #endregion
    }
}
