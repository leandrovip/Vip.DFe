using System.ComponentModel;
using Vip.DFe.Attributes;
using Vip.DFe.Enum;
using Vip.DFe.NFe.Enum;
using Vip.DFe.NFe.Interfaces;
using Vip.DFe.Serializer;
using Vip.DFe.Shared.Enum;

namespace Vip.DFe.NFe.NotaFiscal.Detalhe.Imposto.Estadual
{
    public sealed class Icms00 : GenericClone<Icms00>, INFeIcms
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
        ///     N12- Situação Tributária - 00
        /// </summary>
        [DFeElement(TipoCampo.StrNumberFill, "CST", Id = "N12", Min = 2, Max = 2, Ocorrencia = Ocorrencia.Obrigatoria)]
        public string Cst => "00";

        /// <summary>
        ///     N13 - Modalidade de determinação da BC do ICMS
        /// </summary>
        [DFeElement(TipoCampo.Enum, "modBC", Id = "N13", Min = 1, Max = 1, Ocorrencia = Ocorrencia.Obrigatoria)]
        public NFeModalidadeBC ModBC { get; set; }

        /// <summary>
        ///     N15 - Valor da BC do ICMS - vBC
        /// </summary>
        [DFeElement(TipoCampo.De2, "vBC", Id = "N15", Min = 3, Max = 15, Ocorrencia = Ocorrencia.Obrigatoria)]
        public decimal VBc { get; set; }

        /// <summary>
        ///     N16 - Alíquota do imposto - pICMS
        /// </summary>
        [DFeElement(TipoCampo.De4, "pICMS", Id = "N16", Min = 5, Max = 5, Ocorrencia = Ocorrencia.Obrigatoria)]
        public decimal PIcms { get; set; }

        /// <summary>
        ///     N17 - Valor do ICMS
        /// </summary>
        [DFeElement(TipoCampo.De2, "vICMS", Id = "N17", Min = 3, Max = 15, Ocorrencia = Ocorrencia.Obrigatoria)]
        public decimal VIcms { get; set; }

        /// <summary>
        ///     N17b - Percentual do Fundo de Combate à Pobreza (FCP)
        ///     Versão 4.00
        /// </summary>
        [DFeElement(TipoCampo.De4, "pFCP", Id = "N17b", Min = 5, Max = 5, Ocorrencia = Ocorrencia.NaoObrigatoria)]
        public decimal PFcp { get; set; }

        /// <summary>
        ///     N17c - Valor do Fundo de Combate à Pobreza (FCP)
        ///     Versão 4.00
        /// </summary>
        [DFeElement(TipoCampo.De2, "vFCP", Id = "N17c", Min = 3, Max = 15, Ocorrencia = Ocorrencia.NaoObrigatoria)]
        public decimal VFcp { get; set; }

        #endregion
    }
}