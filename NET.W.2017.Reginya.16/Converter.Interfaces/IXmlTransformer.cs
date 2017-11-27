using System.Xml;

namespace Converter.Interfaces
{
    public interface IXmlTransformer
    {
        /// <summary>
        /// Gets the root element of xml tree.
        /// </summary>        
        /// <param name="xmlDocument">Document to create node.</param>
        /// <returns>The root element of xml tree.</returns>
        XmlNode GetRootElement(XmlDocument xmlDocument);

        /// <summary>
        /// Transforms <paramref name="data"/> from input to output format.
        /// </summary>
        /// <param name="data">Data to transform.</param>
        /// <param name="xmlDocument">Document that contains resulting node.</param>
        /// <returns>Transformed <paramref name="data"/>.</returns>
        XmlNode Transform(string data, XmlDocument xmlDocument);
    }
}
