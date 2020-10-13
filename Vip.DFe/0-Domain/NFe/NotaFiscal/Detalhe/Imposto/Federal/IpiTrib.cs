using System.ComponentModel;
using Vip.DFe.Attributes;
using Vip.DFe.Enum;
using Vip.DFe.NFe.Interfaces;
using Vip.DFe.Serializer;

namespace Vip.DFe.NFe.NotaFiscal.Detalhe.Imposto.Federal
{
    /// <summary>
    ///     O07 (IPITrib) - Grupo do CST 00, 49, 50 e 99
    /// </summary>
    public sealed class IpiTrib : GenericClone<IpiTrib>, INFeIpi
    {
        #region Events

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region Properties

        /// <summary>
        ///     O09 - Código da Situação Tributária do IPI: CST 00, 49, 50, 99
        /// </summary>
        [DFeElement(TipoCampo.StrNumberFill, "CST", Id = "O09", Min = 2, Max = 2, Ocorrencia = Ocorrencia.Obrigatoria)]
        public string Cst { get; set; }

        /// <summary>
        ///     O10 - Valor da BC do IPI
        /// </summary>
        [DFeElement(TipoCampo.De2, "vBC", Id = "O10", Min = 3, Max = 15, Ocorrencia = Ocorrencia.MaiorQueZero)]
        public decimal VBc { get; set; }

        /// <summary>
        ///     O13 - Alíquota do IPI
        /// </summary>
        [DFeElement(TipoCampo.De4, "pIPI", Id = "O13", Min = 5, Max = 7, Ocorrencia = Ocorrencia.MaiorQueZero)]
        public decimal PIpi { get; set; }

        /// <summary>
        ///     O11 - Quantidade total na unidade padrão para tributação (somente para os produtos tributados por unidade)
        /// </summary>
        [DFeElement(TipoCampo.De4, "qUnid", Id = "O11", Min = 5, Max = 15, Ocorrencia = Ocorrencia.MaiorQueZero)]
        public decimal QUnid { get; set; }

        /// <summary>
        ///     O12 - Valor por Unidade Tributável
        /// </summary>
        [DFeElement(TipoCampo.De4, "vUnid", Id = "O12", Min = 5, Max = 15, Ocorrencia = Ocorrencia.MaiorQueZero)]
        public decimal VUnid { get; set; }

        /// <summary>
        ///     O14 - Valor do IPI
        /// </summary>
        [DFeElement(TipoCampo.De2, "vIPI", Id = "O14", Min = 3, Max = 15, Ocorrencia = Ocorrencia.Obrigatoria)]
        public decimal VIpi { get; set; }

        #endregion

        #region Methods

        private bool ShouldSerializeVBc()
        {
            return PIpi > 0;
        }

        private bool ShouldSerializeQUnid()
        {
            return VUnid > 0;
        }

        #endregion
    }
}