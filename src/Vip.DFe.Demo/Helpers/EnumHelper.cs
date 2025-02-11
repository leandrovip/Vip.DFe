using System;
using System.Collections.Generic;
using System.Linq;

namespace Vip.DFe.Demo.Helpers
{
    public static class EnumHelper
    {
        public static IEnumerable<string> GetDescriptions<T>() where T : struct, IConvertible
        {
            var type = typeof(T);

            if (!type.IsEnum || typeof(T) != type)
                return new List<string>();

            try
            {
                return System.Enum.GetValues(type).Cast<T>().Select(item => item.GetDescription()).ToList();
            }
            catch
            {
                return new List<string>();
            }
        }

        public static T GetEnum<T>(string description) where T : struct, IConvertible
        {
            var type = typeof(T);
            var value = System.Enum.GetValues(type).Cast<T>().FirstOrDefault(x => x.GetDescription().ToLower() == description.ToLower());
            return value;
        }
    }
}