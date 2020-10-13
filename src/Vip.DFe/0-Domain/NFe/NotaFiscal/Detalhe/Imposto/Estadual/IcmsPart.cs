using System.ComponentModel;
using Vip.DFe.Attributes;
using Vip.DFe.Enum;
using Vip.DFe.NFe.Enum;
using Vip.DFe.NFe.Interfaces;
using Vip.DFe.Serializer;
using Vip.DFe.Shared.Enum;

namespace Vip.DFe.NFe.NotaFiscal.Detalhe.Imposto.Estadual
{
    public class IcmsPart : GenericClone<IcmsPart>, INFeIcms
    {
        #region Events

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region Constructors

        public IcmsPart()
        {
            Cst = "90";
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
        [DFeElement(TipoCampo.StrNumberFill, "CST", Id = "N12", Min = 2, Max = 2, Ocorrencia = Ocorrencia.Obrigatoria)]
        public string Cst { get; set; }

        /// <summary>
        ///     N13 - Modalidade de determinação da BC do ICMS
        /// </summary>
        [DFeElement(TipoCampo.Enum, "modBC", Id = "N13", Min = 1, Max = 1, Ocorrencia = Ocorrencia.Obrigatoria)]
        public NFeModalidadeBC ModBc { get; set; }

        /// <summary>
        ///     N15 - Valor da BC do ICMS
        /// </summary>
        [DFeElement(TipoCampo.De2, "vBC", Id = "N15", Min = 3, Max = 15, Ocorrencia = Ocorrencia.Obrigatoria)]
        public decimal VBc { get; set; }

        /// <summary>
        ///     N14 - Percentual de redução da BC
        /// </summary>
        [DFeElement(TipoCampo.De4, "pRedBC", Id = "N14", Min = 5, Max = 7, Ocorrencia = Ocorrencia.MaiorQueZero)]
        public decimal PRedBc { get; set; }

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
        ///     N18 - Modalidade de determinação da BC do ICMS ST
        /// </summary>
        [DFeElement(TipoCampo.Enum, "modBCST", Id = "N18", Min = 1, Max = 1, Ocorrencia = Ocorrencia.Obrigatoria)]
        public NFeModalidadeBCST ModBcSt { get; set; }

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
        [DFeElement(TipoCampo.De2, "vBCST", Id = "N21", Min = 3, Max = 15, Ocorrencia = Ocorrencia.Obrigatoria)]
        public decimal VBcSt { get; set; }

        /// <summary>
        ///     N22 - Alíquota do imposto do ICMS ST
        /// </summary>
        [DFeElement(TipoCampo.De4, "pICMSST", Id = "N22", Min = 5, Max = 7, Ocorrencia = Ocorrencia.Obrigatoria)]
        public decimal PIcmsSt { get; set; }

        /// <summary>
        ///     N23 - Valor do ICMS ST
        /// </summary>
        [DFeElement(TipoCampo.De2, "vICMSST", Id = "N23", Min = 3, Max = 15, Ocorrencia = Ocorrencia.Obrigatoria)]
        public decimal VIcmsSt { get; set; }

        /// <summary>
        ///     N25 - Percentual da BC operação própria
        /// </summary>
        [DFeElement(TipoCampo.De4, "pBCOp", Id = "N25", Min = 5, Max = 7, Ocorrencia = Ocorrencia.Obrigatoria)]
        public decimal PBcOp { get; set; }

        /// <summary>
        ///     N24 - UF para qual é devido o ICMS ST
        /// </summary>
        [DFeElement(TipoCampo.Str, "UFST", Id = "N24", Min = 2, Max = 2, Ocorrencia = Ocorrencia.Obrigatoria)]
        public string UFST { get; set; }

        #endregion
    }
}