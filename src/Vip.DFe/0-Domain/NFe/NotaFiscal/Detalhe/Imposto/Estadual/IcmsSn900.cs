using System.ComponentModel;
using Vip.DFe.Attributes;
using Vip.DFe.Enum;
using Vip.DFe.NFe.Enum;
using Vip.DFe.NFe.Interfaces;
using Vip.DFe.Serializer;
using Vip.DFe.Shared.Enum;

namespace Vip.DFe.NFe.NotaFiscal.Detalhe.Imposto.Estadual
{
    public sealed class IcmsSn900 : GenericClone<IcmsSn900>, INFeIcms
    {
        #region Events

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region Constructor

        public IcmsSn900()
        {
            Csosn = "900";
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
        ///     N13 - Modalidade de determinação da BC do ICMS
        /// </summary>
        [DFeElement(TipoCampo.Enum, "modBC", Id = "N13", Min = 1, Max = 1, Ocorrencia = Ocorrencia.NaoObrigatoria)]
        public NFeModalidadeBC? ModBc { get; set; }

        /// <summary>
        ///     N15 - Valor da BC do ICMS
        /// </summary>
        [DFeElement(TipoCampo.De2, "vBC", Id = "N15", Min = 3, Max = 15, Ocorrencia = Ocorrencia.MaiorQueZero)]
        public decimal VBc { get; set; }

        /// <summary>
        ///     N14 - Percentual de redução da BC
        /// </summary>
        [DFeElement(TipoCampo.De4, "pRedBC", Id = "N14", Min = 5, Max = 7, Ocorrencia = Ocorrencia.MaiorQueZero)]
        public decimal PRedBc { get; set; }

        /// <summary>
        ///     N16 - Alíquota do imposto
        /// </summary>
        [DFeElement(TipoCampo.De4, "pICMS", Id = "N16", Min = 5, Max = 7, Ocorrencia = Ocorrencia.MaiorQueZero)]
        public decimal PIcms { get; set; }

        /// <summary>
        ///     N17 - Valor do ICMS
        /// </summary>
        [DFeElement(TipoCampo.De2, "vICMS", Id = "N17", Min = 3, Max = 15, Ocorrencia = Ocorrencia.MaiorQueZero)]
        public decimal VIcms { get; set; }

        /// <summary>
        ///     N18 - Modalidade de determinação da BC do ICMS ST
        /// </summary>
        [DFeElement(TipoCampo.Enum, "modBCST", Id = "N18", Min = 1, Max = 1, Ocorrencia = Ocorrencia.NaoObrigatoria)]
        public NFeModalidadeBCST? ModBcSt { get; set; }

        /// <summary>
        ///     N19 - Percentual da margem de valor Adicionado do ICMS ST
        /// </summary>
        [DFeElement(TipoCampo.De4, "pMVAST", Id = "N19", Min = 5, Max = 7, Ocorrencia = Ocorrencia.MaiorQueZero)]
        public decimal PMvaSt { get; set; }

        /// <summary>
        ///     N20 - Percentual da Redução de BC do ICMS ST
        /// </summary>
        [DFeElement(TipoCampo.De4, "pRedBCST", Id = "N20", Min = 5, Max = 7, Ocorrencia = Ocorrencia.MaiorQueZero)]
        public decimal PRedBcSt { get; set; }

        /// <summary>
        ///     N21 - Valor da BC do ICMS ST
        /// </summary>
        [DFeElement(TipoCampo.De2, "vBCST", Id = "N21", Min = 3, Max = 15, Ocorrencia = Ocorrencia.MaiorQueZero)]
        public decimal VBcSt { get; set; }

        /// <summary>
        ///     N22 - Alíquota do imposto do ICMS ST
        /// </summary>
        [DFeElement(TipoCampo.De4, "pICMSST", Id = "N22", Min = 5, Max = 7, Ocorrencia = Ocorrencia.MaiorQueZero)]
        public decimal PIcmsSt { get; set; }

        /// <summary>
        ///     N23 - Valor do ICMS ST
        /// </summary>
        [DFeElement(TipoCampo.De2, "vICMSST", Id = "N23", Min = 3, Max = 15, Ocorrencia = Ocorrencia.MaiorQueZero)]
        public decimal VIcmsSt { get; set; }

        /// <summary>
        ///     N23a - Valor da Base de Cálculo do FCP retido por Substituição Tributária
        ///     Versão 4.00
        /// </summary>
        [DFeElement(TipoCampo.De2, "vBCFCPST", Id = "N23a", Min = 3, Max = 15, Ocorrencia = Ocorrencia.MaiorQueZero)]
        public decimal VBcFcpSt { get; set; }

        /// <summary>
        ///     N23b - Percentual do FCP retido por Substituição Tributária
        ///     Versão 4.00
        /// </summary>
        [DFeElement(TipoCampo.De4, "pFCPST", Id = "N23b", Min = 5, Max = 7, Ocorrencia = Ocorrencia.MaiorQueZero)]
        public decimal PFcpSt { get; set; }

        /// <summary>
        ///     N23d - Valor do FCP retido por Substituição Tributária
        ///     Versão 4.00
        /// </summary>
        [DFeElement(TipoCampo.De2, "vFCPST", Id = "N23d", Min = 3, Max = 15, Ocorrencia = Ocorrencia.MaiorQueZero)]
        public decimal VFcpSt { get; set; }

        /// <summary>
        ///     N29 - pCredSN - Alíquota aplicável de cálculo do crédito (Simples Nacional).
        /// </summary>
        [DFeElement(TipoCampo.De4, "pCredSN", Id = "N29", Min = 5, Max = 7, Ocorrencia = Ocorrencia.MaiorQueZero)]
        public decimal PCredSn { get; set; }

        /// <summary>
        ///     N30 - Valor crédito do ICMS que pode ser aproveitado nos termos do art. 23 da LC 123 (Simples Nacional)
        /// </summary>
        [DFeElement(TipoCampo.De2, "vCredICMSSN", Id = "N30", Min = 3, Max = 15, Ocorrencia = Ocorrencia.MaiorQueZero)]
        public decimal VCredIcmsSn { get; set; }

        #endregion

        #region Methods

        private bool ShouldSerializeModBc() => ModBc.HasValue;

        private bool ShouldSerializeModBcSt() => ModBcSt.HasValue;

        #endregion
    }
}