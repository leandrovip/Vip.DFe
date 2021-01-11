﻿using System;

namespace Vip.DFe.Danfe.Modelo
{
    public class DuplicataViewModel
    {
        #region Properties

        /// <summary>
        ///     <para>Número da Duplicata</para>
        ///     <para>Tag nDup</para>
        /// </summary>
        public string Numero { get; set; }

        /// <summary>
        ///     <para>Data de vencimento</para>
        ///     <para>Tag dVenc</para>
        /// </summary>
        public DateTime? Vecimento { get; set; }

        /// <summary>
        ///     <para>Valor da duplicata</para>
        ///     <para>Tag vDup</para>
        /// </summary>
        public double? Valor { get; set; }

        #endregion
    }
}