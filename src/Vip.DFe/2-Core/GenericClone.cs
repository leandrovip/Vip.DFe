using System;
using System.Linq;
using System.Reflection;

namespace Vip.DFe
{
    /// <summary>
    ///     Classe GenericClone implementação generica da interface ICloneable.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class GenericClone<T> : ICloneable where T : class
    {
        /// <summary>
        ///     Clones this instance.
        /// </summary>
        /// <returns>T.</returns>
        public T Clone()
        {
            var props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            var ret = Activator.CreateInstance(typeof(T)) as T;

            foreach (var prop in props.Where(prop => null != prop.GetSetMethod()))
            {
                var value = prop.GetValue(this, null);
                prop.SetValue(ret, value is ICloneable cloneable ? cloneable.Clone() : value, null);
            }

            return ret;
        }

        /// <inheritdoc />
        object ICloneable.Clone()
        {
            return Clone();
        }
    }
}