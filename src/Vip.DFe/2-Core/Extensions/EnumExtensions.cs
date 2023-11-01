using System;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using Vip.DFe.Attributes;

namespace Vip.DFe.Extensions
{
    internal static class EnumExtensions
    {
        /// <summary>
        ///     Retorna o valor do Enum definido pelo DFeEnumAttribute.
        /// </summary>
        public static string GetDFeValue<T>(this T value) where T : System.Enum
        {
            var member = typeof(T).GetMember(value.ToString()).FirstOrDefault();
            var enumAttribute = member?.GetCustomAttributes(false).OfType<DFeEnumAttribute>().FirstOrDefault();
            var enumValue = enumAttribute?.Value;
            return enumValue ?? value.ToString();
        }

        public static string GetDescription<T>(this T enumerationValue) where T : struct, IConvertible
        {
            var type = enumerationValue.GetType();
            if (!type.IsEnum)
                return string.Empty;

            var memberInfo = type.GetMember(enumerationValue.ToString(CultureInfo.CurrentCulture));
            if (memberInfo.Length > 0)
            {
                var attrs = memberInfo[0].GetCustomAttributes(typeof(DescriptionAttribute), false);
                if (attrs.Length > 0) return ((DescriptionAttribute) attrs[0]).Description;
            }

            return enumerationValue.ToString(CultureInfo.CurrentCulture);
        }
    }
}