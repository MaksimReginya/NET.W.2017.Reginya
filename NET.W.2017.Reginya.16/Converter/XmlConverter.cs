using System;
using System.Xml;
using Converter.Interfaces;
using Logger.Interfaces;
using Reader.Interfaces;

namespace Converter
{
    /// <inheritdoc />
    /// <summary>
    /// Performs convertion of string set into XmlDocument.
    /// </summary>
    public class XmlConverter : IConverter<string, XmlDocument>
    {
        #region Public constructors
                
        /// <summary>
        /// Initializes a new instance of the <see cref="XmlConverter"/> class.
        /// </summary>
        /// <param name="logger">logger</param>
        public XmlConverter(ILogger logger = null)
        {
            this.Logger = logger;
        }

        #endregion

        #region Public properties
                
        /// <summary>
        /// Logs information about data that can't be converted.
        /// </summary>
        public ILogger Logger { get; set; }

        #endregion

        #region Public methods
                
        /// <inheritdoc />
        /// <summary>
        /// Converts data provided by <paramref name="reader"/> in XmlDocument with <paramref name="transformer"/>.
        /// </summary>        
        /// <param name="reader">Provides converter with data.</param>
        /// <param name="transformer">Performs transformation of data.</param>
        /// <returns>Xml document representing data.</returns>
        /// <exception cref="ArgumentNullException">
        /// Thrown when <paramref name="reader"/> or <paramref name="transformer"/> is null.
        /// </exception>
        public XmlDocument Convert(IReader<string> reader, IXmlTransformer transformer)
        {
            VerifyInput(reader, transformer);

            var xmlDocument = new XmlDocument();
            var xmlDeclaration = xmlDocument.CreateXmlDeclaration("1.0", "utf-8", null);
            xmlDocument.AppendChild(xmlDeclaration);
            
            var xmlRootElement = transformer.GetRootElement(xmlDocument);

            GetChildElements(xmlRootElement, xmlDocument, reader, transformer);

            xmlDocument.AppendChild(xmlRootElement);

            return xmlDocument;
        }

        #endregion

        #region Private methods

        private static void VerifyInput<T>(IReader<T> reader, IXmlTransformer transformer)
        {
            if (ReferenceEquals(reader, null))
            {
                throw new ArgumentNullException(nameof(reader));
            }

            if (ReferenceEquals(transformer, null))
            {
                throw new ArgumentNullException(nameof(transformer));
            }
        }

        private void GetChildElements<T>(XmlNode root, XmlDocument xmlDocument, IReader<T> reader, IXmlTransformer transformer)
        {
            int i = 0;
            foreach (var data in reader.GetData())
            {
                i++;
                XmlNode currentNode;
                try
                {
                    currentNode = transformer.Transform(data as string, xmlDocument);
                }
                catch (Exception)
                {
                    this.Logger?.Warn($"Url address on the {i} line is not valid.");
                    continue;
                }
                
                root.AppendChild(currentNode);
            }
        }

        #endregion
    }
}
