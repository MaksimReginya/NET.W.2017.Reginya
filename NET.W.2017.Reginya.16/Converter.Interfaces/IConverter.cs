using Reader.Interfaces;

namespace Converter.Interfaces
{
    /// <summary>
    /// Interface of data converter.
    /// </summary>
    /// <typeparam name="T">
    /// The type of input data.
    /// </typeparam>
    /// <typeparam name="T1">
    /// The type of output data.
    /// </typeparam>
    public interface IConverter<in T, out T1>
    {
        /// <summary>
        /// Converts data provided by <paramref name="reader"/> with <paramref name="transformer"/>.
        /// </summary>
        /// <param name="reader">Provides data to converter.</param>
        /// <param name="transformer">Performs transformation of data.</param>
        /// <returns>Converted data.</returns>
        T1 Convert(IReader<T> reader, IXmlTransformer transformer);
    }
}
