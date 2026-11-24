namespace Vip.DFe.Extensions
{
    /// <summary>
    ///     Classe IntExtensions.
    /// </summary>
    internal static class IntExtensions
    {
        /// <summary>
        ///     Zeroes the fill.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="length">The length.</param>
        /// <returns>System.String.</returns>
        public static string ZeroFill(this int? value, int length)
        {
            return value.HasValue ? value.Value.ToString().ZeroFill(length) : "".ZeroFill(length);
        }

        /// <summary>
        ///     Zeroes the fill.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="length">The length.</param>
        /// <returns>System.String.</returns>
        public static string ZeroFill(this int value, int length)
        {
            return ((int?) value).ZeroFill(length);
        }
    }
}