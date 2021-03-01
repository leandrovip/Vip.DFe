using System.ComponentModel;
using Vip.DFe.Attributes;
using Vip.DFe.Enum;
using Vip.DFe.NFe.Interfaces;
using Vip.DFe.Serializer;
using Vip.DFe.Shared.Enum;

namespace Vip.DFe.NFe.NotaFiscal.Detalhe.Imposto.Estadual
{
    public sealed class IcmsSn500 : GenericClone<IcmsSn500>, INFeIcms
    {
        #region Events

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region Construtor

        public IcmsSn500()
        {
            Csosn = "500";
        }

        #endregion

        #region Properties

        /// <summary>
        ///     N11 - Origem da Mercadoria
        /// </summary>
        [DFeElement(TipoCampo.Enum, "orig", Id = "N11", Min = 1, Max = 1, Ocorrencia = Ocorrencia.Obrigatoria)]
        public OrigemMercadoria Origem { get; set; }

        /// <summary>
        ///     N12a - Código de Situação da Operação – Simples Nacional
        /// </summary>
        [DFeElement(TipoCampo.StrNumberFill, "CSOSN", Id = "N12a", Min = 3, Max = 3, Ocorrencia = Ocorrencia.Obrigatoria)]
        public string Csosn { get; private set; }

        /// <summary>
        ///     N26 - Valor da BC do ICMS ST retido
        /// </summary>
        [DFeElement(TipoCampo.De2, "vBCSTRet", Id = "N26", Min = 1, Max = 15, Ocorrencia = Ocorrencia.Obrigatoria)]
        public decimal VBcStRet { get; set; }

        /// <summary>
        ///     N26a  - Alíquota suportada pelo Consumidor Final
        /// </summary>
        [DFeElement(TipoCampo.De4, "pST", Id = "N26a", Min = 1, Max = 7, Ocorrencia = Ocorrencia.Obrigatoria)]
        public decimal PSt { get; set; }

        /// <summary>
        ///     N26b - Valor ICMS próprio do substituto cobrado em operação anterior
        /// </summary>
        [DFeElement(TipoCampo.De2, "vICMSSubstituto", Id = "N26b", Min = 1, Max = 15, Ocorrencia = Ocorrencia.Obrigatoria)]
        public decimal VIcmsSubstituto { get; set; }

        /// <summary>
        ///     N27 - Valor do ICMS ST retido
        /// </summary>
        [DFeElement(TipoCampo.De2, "vICMSSTRet", Id = "N27", Min = 1, Max = 15, Ocorrencia = Ocorrencia.Obrigatoria)]
        public decimal VIcmsStRet { get; set; }

        /// <summary>
        ///     N27a - Valor da Base de Cálculo do FCP
        ///     Versão 4.00
        /// </summary>
        [DFeElement(TipoCampo.De2, "vBCFCPSTRet", Id = "N27a", Min = 3, Max = 15, Ocorrencia = Ocorrencia.NaoObrigatoria)]
        public decimal VBcFcpStRet { get; set; }

        /// <summary>
        ///     N27b - Percentual do FCP retido anteriormente por Substituição Tributária
        /// </summary>
        [DFeElement(TipoCampo.De4, "pFCPSTRet", Id = "N27b", Min = 5, Max = 7, Ocorrencia = Ocorrencia.NaoObrigatoria)]
        public decimal PFcpStRet { get; set; }

        /// <summary>
        ///     N27d - Valor do FCP retido anteriormente por Substituição Tributária
        /// </summary>
        [DFeElement(TipoCampo.De2, "vFCPSTRet", Id = "N27d", Min = 3, Max = 15, Ocorrencia = Ocorrencia.NaoObrigatoria)]
        public decimal VFcpStRet { get; set; }

        /// <summary>
        ///     N34 - Percentual de redução da base de cálculo efetiva
        /// </summary>
        [DFeElement(TipoCampo.De4, "pRedBCEfet", Id = "N34", Min = 5, Max = 7, Ocorrencia = Ocorrencia.NaoObrigatoria)]
        public decimal PRedBcEfet { get; set; }

        /// <summary>
        ///     N35 - Valor da base de cálculo efetiva
        /// </summary>
        [DFeElement(TipoCampo.De2, "vBCEfet", Id = "N35", Min = 3, Max = 15, Ocorrencia = Ocorrencia.NaoObrigatoria)]
        public decimal VBcEfet { get; set; }

        /// <summary>
        ///     N36 - Alíquota do ICMS efetiva
        /// </summary>
        [DFeElement(TipoCampo.De4, "pICMSEfet", Id = "N36", Min = 5, Max = 7, Ocorrencia = Ocorrencia.NaoObrigatoria)]
        public decimal PIcmsEfet { get; set; }

        /// <summary>
        ///     N37 - Valor do ICMS efetivo
        /// </summary>
        [DFeElement(TipoCampo.De2, "vICMSEfet", Id = "N37", Min = 3, Max = 15, Ocorrencia = Ocorrencia.NaoObrigatoria)]
        public decimal VIcmsEfet { get; set; }

        #endregion
    }
}