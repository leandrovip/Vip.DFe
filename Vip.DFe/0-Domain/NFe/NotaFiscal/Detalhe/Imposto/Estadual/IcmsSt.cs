using System.ComponentModel;
using Vip.DFe.Attributes;
using Vip.DFe.Enum;
using Vip.DFe.NFe.Interfaces;
using Vip.DFe.Serializer;
using Vip.DFe.Shared.Enum;

namespace Vip.DFe.NFe.NotaFiscal.Detalhe.Imposto.Estadual
{
    public sealed class IcmsSt : GenericClone<IcmsSt>, INFeIcms
    {
        #region Events

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region Constructors

        public IcmsSt()
        {
            Cst = "41";
        }

        #endregion

        #region Properties

        /// <summary>
        ///     N11 - Origem da Mercadoria
        /// </summary>
        [DFeElement(TipoCampo.Enum, "orig", Id = "N11", Min = 1, Max = 1, Ocorrencia = Ocorrencia.Obrigatoria)]
        public OrigemMercadoria Origem { get; set; }

        /// <summary>
        ///     N12- Situação Tributária - CST 41, 60
        /// </summary>
        [DFeElement(TipoCampo.StrNumberFill, "CST", Id = "N12", Min = 2, Max = 2, Ocorrencia = Ocorrencia.Obrigatoria)]
        public string Cst { get; set; }

        /// <summary>
        ///     N26 - Valor da BC do ICMS ST retido
        /// </summary>
        [DFeElement(TipoCampo.De2, "vBCSTRet", Id = "N26", Min = 3, Max = 15, Ocorrencia = Ocorrencia.Obrigatoria)]
        public decimal VBcStRet { get; set; }

        /// <summary>
        ///     N26a - Valor ICMS próprio do substituto cobrado em operação anterior
        /// </summary>
        [DFeElement(TipoCampo.De4, "pST", Id = "N26a", Min = 5, Max = 7, Ocorrencia = Ocorrencia.MaiorQueZero)]
        public decimal PSt { get; set; }

        /// <summary>
        ///     N26b - Valor ICMS próprio do substituto cobrado em operação anterior
        /// </summary>
        [DFeElement(TipoCampo.De2, "vICMSSubstituto", Id = "N26b", Min = 3, Max = 15, Ocorrencia = Ocorrencia.MaiorQueZero)]
        public decimal VIcmsSubstituto { get; set; }

        /// <summary>
        ///     N27 - Valor do ICMS ST retido
        /// </summary>
        [DFeElement(TipoCampo.De2, "vICMSSTRet", Id = "N27", Min = 3, Max = 15, Ocorrencia = Ocorrencia.Obrigatoria)]
        public decimal VIcmsStRet { get; set; }

        /// <summary>
        ///     N31 - Valor da BC do ICMS ST da UF destino
        /// </summary>
        [DFeElement(TipoCampo.De2, "vBCSTDest", Id = "N31", Min = 3, Max = 15, Ocorrencia = Ocorrencia.Obrigatoria)]
        public decimal VBcStDest { get; set; }

        /// <summary>
        ///     N32 - Valor do ICMS ST da UF destino
        /// </summary>
        [DFeElement(TipoCampo.De2, "vICMSSTDest", Id = "N32", Min = 3, Max = 15, Ocorrencia = Ocorrencia.Obrigatoria)]
        public decimal VIcmsStDest { get; set; }

        #endregion
    }
}