using System;

namespace Vip.DFe.Helpers
{
    public static class DFeHelper
    {
        /// <summary>
        ///     Gera um número aleatório para utilizar, por exemplo, como código numérico que compõe a chave de acesso.
        ///     <para />
        ///     Mínimo 1, máximo 99999999
        /// </summary>
        /// <returns></returns>
        public static int GerarNumeroAleatorio()
        {
            return StaticRandom.Next(1, 99999999);
        }

        /// <summary>
        ///     Gera um número de lote
        /// </summary>
        public static string GerarNumeroLote()
        {
            return $"{DateTime.Now.Day:00}" +
                   $"{DateTime.Now.Month:00}" +
                   $"{DateTime.Now.Year % 100}" +
                   $"{DateTime.Now.Hour:00}" +
                   $"{DateTime.Now.Minute:00}" +
                   $"{DateTime.Now.Second:00}";
        }
    }
}