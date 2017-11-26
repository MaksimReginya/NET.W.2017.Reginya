namespace Converter.Interfaces
{
    /// <summary>
    /// Interface of any data converter.
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
        /// Converts <paramref name="data"/> from input to output format.
        /// </summary>
        /// <param name="data">Data to convert.</param>
        /// <returns>Converted <paramref name="data"/>.</returns>
        T1 Convert(T data);
    }
}
