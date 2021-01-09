using System.ComponentModel;
using System.Xml.Serialization;
using Vip.DFe.Attributes;
using Vip.DFe.Enum;
using Vip.DFe.NFe.Enum;
using Vip.DFe.NFe.Interfaces;
using Vip.DFe.Serializer;
using Vip.DFe.Shared.Enum;

namespace Vip.DFe.NFe.NotaFiscal.Detalhe.Imposto.Estadual
{
    public sealed class Icms20 : GenericClone<Icms20>, INFeIcms
    {
        #region Events

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region Constructor

        public Icms20()
        {
            Cst = "20";
        }

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
        public string Cst { get; private set; }

        /// <summary>
        ///     N13 - Modalidade de determinação da BC do ICMS
        /// </summary>
        [DFeElement(TipoCampo.Enum, "modBC", Id = "N13", Min = 1, Max = 1, Ocorrencia = Ocorrencia.Obrigatoria)]
        public NFeModalidadeBC ModBc { get; set; }

        /// <summary>
        ///     N14 - Percentual de redução da BC
        /// </summary>
        [DFeElement(TipoCampo.De4, "pRedBC", Id = "N14", Min = 5, Max = 7, Ocorrencia = Ocorrencia.Obrigatoria)]
        public decimal PRedBc { get; set; }

        /// <summary>
        ///     N15 - Valor da BC do ICMS
        /// </summary>
        [DFeElement(TipoCampo.De2, "vBC", Id = "N15", Min = 3, Max = 15, Ocorrencia = Ocorrencia.Obrigatoria)]
        public decimal VBc { get; set; }

        /// <summary>
        ///     N16 - Alíquota do imposto
        /// </summary>
        [DFeElement(TipoCampo.De4, "pICMS", Id = "N16", Min = 5, Max = 7, Ocorrencia = Ocorrencia.Obrigatoria)]
        public decimal PIcms { get; set; }

        /// <summary>
        ///     N17 - Valor do ICMS
        /// </summary>
        [DFeElement(TipoCampo.De2, "vICMS", Id = "N17", Min = 3, Max = 15, Ocorrencia = Ocorrencia.Obrigatoria)]
        public decimal VIcms { get; set; }

        /// <summary>
        ///     N17a - Valor da Base de Cálculo do FCP
        ///     Versão 4.00
        /// </summary>
        [DFeElement(TipoCampo.De2, "vBCFCP", Id = "N17a", Min = 3, Max = 15, Ocorrencia = Ocorrencia.MaiorQueZero)]
        public decimal VBcFcp { get; set; }

        /// <summary>
        ///     N17b - Percentual do Fundo de Combate à Pobreza (FCP)
        ///     Versão 4.00
        /// </summary>
        [XmlElement(Order = 9)]
        [DFeElement(TipoCampo.De4, "pFCP", Id = "N17b", Min = 5, Max = 7, Ocorrencia = Ocorrencia.MaiorQueZero)]
        public decimal PFcp { get; set; }

        /// <summary>
        ///     N17c - Valor do Fundo de Combate à Pobreza (FCP)
        ///     Versão 4.00
        /// </summary>
        [DFeElement(TipoCampo.De2, "vFCP", Id = "N17c", Min = 3, Max = 15, Ocorrencia = Ocorrencia.MaiorQueZero)]
        public decimal VFcp { get; set; }

        /// <summary>
        ///     N27a - Valor do ICMS desonerado
        /// </summary>
        [DFeElement(TipoCampo.De2, "vICMSDeson", Id = "N27a", Min = 3, Max = 15, Ocorrencia = Ocorrencia.MaiorQueZero)]
        public decimal VIcmsDeson { get; set; }

        /// <summary>
        ///     N28 - Motivo da desoneração do ICMS
        /// </summary>
        [DFeElement(TipoCampo.Enum, "motDesICMS", Id = "N28", Min = 2, Max = 2, Ocorrencia = Ocorrencia.NaoObrigatoria)]
        public NFeMotivoDesoneracao MotDesIcms { get; set; }

        #endregion

        #region Methods

        private bool ShouldSerializeMotDesIcms() => VIcmsDeson > 0;

        #endregion
    }
}