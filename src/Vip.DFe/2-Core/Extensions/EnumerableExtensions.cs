using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace Vip.DFe.Extensions
{
    internal static class EnumerableExtensions
    {
        /// <summary>
        ///     Transforma uma lista em uma BindingList.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list">A lista</param>
        /// <returns>BindingList</returns>
        public static BindingList<T> ToBindingList<T>(this IEnumerable<T> list)
        {
            var ret = new BindingList<T>();

            foreach (var item in list) ret.Add(item);

            return ret;
        }

        /// <summary>
        ///     Transforma uma lista de string em uma unica string.
        /// </summary>
        /// <param name="array">The array.</param>
        /// <returns>String com todos os dados da lista de strings</returns>
        public static string AsString(this IEnumerable<string> array)
        {
            return string.Join(Environment.NewLine, array);
        }

        /// <summary>
        ///     Faz cast de um ienumerable para outro tipo
        /// </summary>
        /// <param name="lista"></param>
        /// <param name="tipo"></param>
        /// <returns></returns>
        public static IEnumerable Cast(this IEnumerable lista, Type tipo)
        {
            var method = typeof(Enumerable).GetMethod("Cast").MakeGenericMethod(tipo);
            return (IEnumerable) method.Invoke(null, new object[] {lista});
        }
    }
}