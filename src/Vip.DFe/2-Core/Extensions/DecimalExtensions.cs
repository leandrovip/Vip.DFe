using System;

namespace Vip.DFe.Extensions
{
    internal static class DecimalExtensions
    {
        /// <summary>
        ///     Truncate value
        /// </summary>
        public static decimal Trunc(this decimal value, int decimalPlaces = 2)
        {
            var factor = (decimal) Math.Pow(10, decimalPlaces);
            return Math.Truncate(value * factor) / factor;
        }
    }
}