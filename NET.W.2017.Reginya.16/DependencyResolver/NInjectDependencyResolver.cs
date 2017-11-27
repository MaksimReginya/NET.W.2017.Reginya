using System.Xml;
using Converter;
using Converter.Interfaces;
using Logger;
using Logger.Interfaces;
using Ninject;
using Reader;
using Reader.Interfaces;

namespace DependencyResolver
{
    public class NInjectDependencyResolver
    {
        public static void Configure(IKernel kernel)
        {
            kernel.Bind<IReader<string>>().To<FileReader>().WithConstructorArgument("urls.txt");
            kernel.Bind<IXmlTransformer>().To<UrlToXmlTransformer>();

            kernel.Bind<ILogger>().To<NLogger>().WithConstructorArgument("Program");
            var logger = kernel.Get<ILogger>();
                        
            kernel.Bind<IConverter<string, XmlDocument>>().To<XmlConverter>().WithConstructorArgument(logger);                                
        }
    }
}
