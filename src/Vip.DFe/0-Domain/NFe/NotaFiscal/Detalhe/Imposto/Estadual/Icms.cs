using System.ComponentModel;
using Vip.DFe.Attributes;
using Vip.DFe.NFe.Interfaces;

namespace Vip.DFe.NFe.NotaFiscal.Detalhe.Imposto.Estadual
{
    public sealed class Icms : GenericClone<Icms>, INFeImposto
    {
        #region Events

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region Properties

        /// <summary>
        ///     <para>N02 (ICMS00) - Grupo Tributação do ICMS= 00</para>
        ///     <para>N03 (ICMS10) - Grupo Tributação do ICMS = 10 </para>
        ///     <para>N04 (ICMS20) - Grupo Tributação do ICMS = 20</para>
        ///     <para>N05 (ICMS30) - Grupo Tributação do ICMS = 30</para>
        ///     <para>N06 (ICMS40) - Grupo Tributação ICMS = 40, 41, 50</para>
        ///     <para>N07 (ICMS51) - Grupo Tributação do ICMS = 51</para>
        ///     <para>N08 (ICMS60) - Grupo Tributação do ICMS = 60</para>
        ///     <para>N09 (ICMS70) - Grupo Tributação do ICMS = 70</para>
        ///     <para>N10 (ICMS90) - Grupo Tributação do ICMS = 90</para>
        ///     <para>
        ///         N10a (ICMSPart) - Grupo de Partilha do ICMS entre a UF de origem e UF de destino ou a UF definida na
        ///         legislação.
        ///     </para>
        ///     <para>
        ///         N10b (ICMSST) - Grupo de Repasse de ICMS ST retido anteriormente em operações interestaduais com repasses
        ///         através do Substituto Tributário
        ///     </para>
        ///     <para>N10c (ICMSSN101) - Grupo CRT=1 – Simples Nacional e CSOSN=101</para>
        ///     <para>N10d (ICMSSN102) - Grupo CRT=1 – Simples Nacional e CSOSN=102, 103, 300 ou 400</para>
        ///     <para>N10e (ICMSSN201) - Grupo CRT=1 – Simples Nacional e CSOSN=201</para>
        ///     <para>N10f (ICMSSN202) - Grupo CRT=1 – Simples Nacional e CSOSN=202 ou 203</para>
        ///     <para>N10g (ICMSSN500) - Grupo CRT=1 – Simples Nacional e CSOSN = 500</para>
        ///     <para>N10h (ICMSSN900) - Grupo CRT=1 – Simples Nacional e CSOSN=900</para>
        /// </summary>
        [DFeItem(typeof(Icms00), "ICMS00")]
        [DFeItem(typeof(Icms10), "ICMS10")]
        [DFeItem(typeof(Icms20), "ICMS20")]
        [DFeItem(typeof(Icms30), "ICMS30")]
        [DFeItem(typeof(Icms40), "ICMS40")]
        [DFeItem(typeof(Icms51), "ICMS51")]
        [DFeItem(typeof(Icms60), "ICMS60")]
        [DFeItem(typeof(Icms70), "ICMS70")]
        [DFeItem(typeof(Icms90), "ICMS90")]
        [DFeItem(typeof(IcmsPart), "ICMSPart")]
        [DFeItem(typeof(IcmsSt), "ICMSST")]
        [DFeItem(typeof(IcmsSn101), "ICMSSN101")]
        [DFeItem(typeof(IcmsSn102), "ICMSSN102")]
        [DFeItem(typeof(IcmsSn201), "ICMSSN201")]
        [DFeItem(typeof(IcmsSn202), "ICMSSN202")]
        [DFeItem(typeof(IcmsSn500), "ICMSSN500")]
        [DFeItem(typeof(IcmsSn900), "ICMSSN900")]
        public INFeIcms Tipo { get; set; }

        #endregion

        #region Methods

        #endregion
    }
}