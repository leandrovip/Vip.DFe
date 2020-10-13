using System.ComponentModel;
using Vip.DFe.Attributes;
using Vip.DFe.Enum;
using Vip.DFe.NFe.Enum;
using Vip.DFe.NFe.Interfaces;
using Vip.DFe.Serializer;
using Vip.DFe.Shared.Enum;

namespace Vip.DFe.NFe.NotaFiscal.Detalhe.Imposto.Estadual
{
    public sealed class Icms51 : GenericClone<Icms51>, INFeIcms
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
        public string Cst => "51";

        /// <summary>
        ///     N13 - Modalidade de determinação da BC do ICMS
        /// </summary>
        [DFeElement(TipoCampo.Enum, "modBC", Id = "N13", Min = 1, Max = 1, Ocorrencia = Ocorrencia.NaoObrigatoria)]
        public NFeModalidadeBC? ModBc { get; set; }

        /// <summary>
        ///     N14 - Percentual de redução da BC
        /// </summary>
        [DFeElement(TipoCampo.De4, "pRedBC", Id = "N14", Min = 5, Max = 7, Ocorrencia = Ocorrencia.MaiorQueZero)]
        public decimal PRedBc { get; set; }

        /// <summary>
        ///     N15 - Valor da BC do ICMS
        /// </summary>
        [DFeElement(TipoCampo.De2, "vBC", Id = "N15", Min = 3, Max = 15, Ocorrencia = Ocorrencia.MaiorQueZero)]
        public decimal VBc { get; set; }

        /// <summary>
        ///     N16 - Alíquota do imposto
        /// </summary>
        [DFeElement(TipoCampo.De4, "pICMS", Id = "N16", Min = 5, Max = 7, Ocorrencia = Ocorrencia.MaiorQueZero)]
        public decimal PIcms { get; set; }

        /// <summary>
        ///     N16a - Valor do ICMS da Operação, valor como se não tivesse o diferimento
        /// </summary>
        [DFeElement(TipoCampo.De2, "vICMSOp", Id = "N16a", Min = 3, Max = 15, Ocorrencia = Ocorrencia.MaiorQueZero)]
        public decimal VIcmsOp { get; set; }

        /// <summary>
        ///     N16b - Percentual do diferimento
        /// </summary>
        [DFeElement(TipoCampo.De4, "pDif", Id = "N16b", Min = 5, Max = 7, Ocorrencia = Ocorrencia.MaiorQueZero)]
        public decimal PDif { get; set; }

        /// <summary>
        ///     N16c - Valor do ICMS diferido
        /// </summary>
        [DFeElement(TipoCampo.De2, "vICMSDif", Id = "N16c", Min = 3, Max = 15, Ocorrencia = Ocorrencia.MaiorQueZero)]
        public decimal VIcmsDif { get; set; }

        /// <summary>
        ///     N17 - Valor do ICMS
        /// </summary>
        [DFeElement(TipoCampo.De2, "vICMS", Id = "N17", Min = 3, Max = 15, Ocorrencia = Ocorrencia.MaiorQueZero)]
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
        [DFeElement(TipoCampo.De4, "pFCP", Id = "N17b", Min = 5, Max = 7, Ocorrencia = Ocorrencia.MaiorQueZero)]
        public decimal PFcp { get; set; }

        /// <summary>
        ///     N17c - Valor do Fundo de Combate à Pobreza (FCP)
        ///     Versão 4.00
        /// </summary>
        [DFeElement(TipoCampo.De2, "vFCP", Id = "N17c", Min = 3, Max = 15, Ocorrencia = Ocorrencia.MaiorQueZero)]
        public decimal VFcp { get; set; }

        #endregion

        #region Methods

        private bool ShouldSerializeModBc()
        {
            return ModBc.HasValue;
        }

        #endregion
    }
}