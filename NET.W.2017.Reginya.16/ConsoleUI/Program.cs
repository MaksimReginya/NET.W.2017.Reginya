using System;
using System.Xml;
using Converter.Interfaces;
using DependencyResolver;
using Ninject;
using Reader.Interfaces;

namespace ConsoleUI
{
    internal class Program
    {
        private static readonly IKernel Kernel;
        private static readonly IReader<string> Reader;
        private static readonly IXmlTransformer Transformer;

        static Program()
        {
            Kernel = new StandardKernel();
            NInjectDependencyResolver.Configure(Kernel);
            Reader = Kernel.Get<IReader<string>>();
            Transformer = Kernel.Get<IXmlTransformer>();
        }

        private static void Main(string[] args)
        {
            var converter = Kernel.Get<IConverter<string, XmlDocument>>();                        
            var xmlDocument = converter.Convert(Reader, Transformer);
            xmlDocument.Save("result_xml.xml");

            Console.ReadLine();
        }
    }
}
