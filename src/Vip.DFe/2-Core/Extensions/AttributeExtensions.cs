using System;
using System.Linq;
using System.Reflection;

namespace Vip.DFe.Extensions
{
    internal static class AttributeExtensions
    {
        public static TValue GetAttributeValue<TAttribute, TValue>(
            this ICustomAttributeProvider type,
            Func<TAttribute, TValue> valueSelector)
            where TAttribute : Attribute
        {
            var att = type.GetCustomAttributes(
                    typeof(TAttribute), true
                )
                .FirstOrDefault() as TAttribute;

            return att != null ? valueSelector(att) : default;
        }

        public static TAttribute GetAttribute<TAttribute>(this ICustomAttributeProvider provider) where TAttribute : Attribute
        {
            var att = provider.GetCustomAttributes(typeof(TAttribute), true).FirstOrDefault() as TAttribute;
            return att;
        }

        public static TAttribute[] GetAttributes<TAttribute>(this ICustomAttributeProvider type)
            where TAttribute : Attribute
        {
            var att = type.GetCustomAttributes(typeof(TAttribute), true)
                .Cast<TAttribute>().ToArray();
            return att;
        }

        public static bool HasAttribute<T>(this ICustomAttributeProvider provider) where T : Attribute
        {
            var atts = provider.GetCustomAttributes(typeof(T), true);
            return atts.Length > 0;
        }
    }
}