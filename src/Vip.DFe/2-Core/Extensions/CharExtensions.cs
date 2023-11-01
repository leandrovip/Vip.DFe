using Vip.DFe.Exception;

namespace Vip.DFe.Extensions
{
    internal static class CharExtensions
    {
        /// <summary>
        ///     Convert to Int32
        /// </summary>
        /// <param name="value">Value to convert</param>
        /// <param name="defaultValue">Default return value</param>
        /// <returns>System.Int32</returns>
        /// <exception cref="VipException">Erro ao converter string</exception>
        public static int ToInt(this char value, int defaultValue = -1)
        {
            try
            {
                if (!int.TryParse(value.ToString(), out var converted))
                    converted = defaultValue;
                return converted;
            }
            catch (System.Exception ex)
            {
                throw new VipException("Erro ao converter string", ex);
            }
        }
    }
}