using System.ComponentModel;
using Vip.DFe.Attributes;
using Vip.DFe.Enum;
using Vip.DFe.NFe.Interfaces;
using Vip.DFe.Serializer;
using Vip.DFe.Shared.Enum;

namespace Vip.DFe.NFe.NotaFiscal.Detalhe.Imposto.Estadual
{
    public class Icms60 : GenericClone<Icms60>, INFeIcms
    {
        #region Events

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region Properties

        /// <summary>
        ///     N11 - Origem da Mercadoria
        /// </summary>
        [DFeElement(TipoCampo.Enum, "orig", Id = "N11", Min = 1, Max = 1, Ocorrencia = Ocorrencia.Obrigatoria)]
        public OrigemMercadoria Origem { get; set; }

        /// <summary>
        ///     N12- Situação Tributária
        /// </summary>
        [DFeElement(TipoCampo.Str, "CST", Id = "N12", Min = 2, Max = 2, Ocorrencia = Ocorrencia.Obrigatoria)]
        public string Cst => "60";

        /// <summary>
        ///     N26 - Valor da BC do ICMS ST retido
        /// </summary>
        [DFeElement(TipoCampo.De2, "vBCSTRet", Id = "N26", Min = 3, Max = 15, Ocorrencia = Ocorrencia.MaiorQueZero)]
        public decimal VBcStRet { get; set; }

        /// <summary>
        ///     N26a - Alíquota suportada pelo Consumidor Final
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
        [DFeElement(TipoCampo.De2, "vICMSSTRet", Id = "N27", Min = 3, Max = 15, Ocorrencia = Ocorrencia.MaiorQueZero)]
        public decimal VIcmsStRet { get; set; }

        /// <summary>
        ///     N27a - Valor da Base de Cálculo do FCP retido anteriormente por ST
        ///     Versão 4.00
        /// </summary>
        [DFeElement(TipoCampo.De2, "vBCFCPSTRet", Id = "N27a", Min = 3, Max = 15, Ocorrencia = Ocorrencia.MaiorQueZero)]
        public decimal VBcFcpStRet { get; set; }

        /// <summary>
        ///     N27b - Percentual do FCP retido anteriormente por Substituição Tributária
        ///     Versão 4.00
        /// </summary>
        [DFeElement(TipoCampo.De4, "pFCPSTRet", Id = "N27b", Min = 5, Max = 7, Ocorrencia = Ocorrencia.MaiorQueZero)]
        public decimal pFCPSTRet { get; set; }

        /// <summary>
        ///     N27d - Valor do FCP retido por Substituição Tributária
        ///     Versão 4.00
        /// </summary>
        [DFeElement(TipoCampo.De2, "vFCPSTRet", Id = "N27d", Min = 3, Max = 15, Ocorrencia = Ocorrencia.MaiorQueZero)]
        public decimal VFcpStRet { get; set; }

        /// <summary>
        ///     N34 - Percentual de redução da base de cálculo efetiva
        /// </summary>
        [DFeElement(TipoCampo.De4, "pRedBCEfet", Id = "N34", Min = 5, Max = 7, Ocorrencia = Ocorrencia.MaiorQueZero)]
        public decimal PRedBcEfet { get; set; }

        /// <summary>
        ///     N35 - Valor da base de cálculo efetiva
        /// </summary>
        [DFeElement(TipoCampo.De2, "vBCEfet", Id = "N35", Min = 3, Max = 15, Ocorrencia = Ocorrencia.MaiorQueZero)]
        public decimal VBcEfet { get; set; }

        /// <summary>
        ///     N36 - Alíquota do ICMS efetiva
        /// </summary>
        [DFeElement(TipoCampo.De4, "pICMSEfet", Id = "N36", Min = 5, Max = 7, Ocorrencia = Ocorrencia.MaiorQueZero)]
        public decimal PIcmsEfet { get; set; }

        /// <summary>
        ///     N37 - Valor do ICMS efetivo
        /// </summary>
        [DFeElement(TipoCampo.De2, "vICMSEfet", Id = "N37", Min = 3, Max = 15, Ocorrencia = Ocorrencia.MaiorQueZero)]
        public decimal VIcmsEfet { get; set; }

        #endregion
    }
}