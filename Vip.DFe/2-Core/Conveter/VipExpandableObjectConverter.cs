using System;
using System.ComponentModel;
using System.Globalization;

namespace Vip.DFe.Conveter
{
    public class VipExpandableObjectConverter : ExpandableObjectConverter
    {
        /// <summary>
        ///     Converts to.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="culture">The culture.</param>
        /// <param name="value">The value.</param>
        /// <param name="destType">Type of the dest.</param>
        /// <returns>System.Object.</returns>
        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destType)
        {
            if (value != null && destType == typeof(string)) return $"({value.GetType().Name})";

            return base.ConvertTo(context, culture, value, destType);
        }
    }
}