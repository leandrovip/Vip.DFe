using System.ComponentModel;
using Vip.DFe.Attributes;
using Vip.DFe.Enum;
using Vip.DFe.Serializer;

namespace Vip.DFe.NFe.NotaFiscal.Cana
{
    /// <summary>
    ///     ZC10 - Grupo Deduções – Taxas e Contribuições
    /// </summary>
    public sealed class NFeCanaDeducao : GenericClone<NFeCanaDeducao>, INotifyPropertyChanged
    {
        #region Events

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region Properties

        /// <summary>
        ///     ZC11 - Descrição da Dedução
        /// </summary>
        [DFeElement(TipoCampo.Str, "xDed", Id = "ZC11", Min = 1, Max = 60, Ocorrencia = Ocorrencia.Obrigatoria)]
        public string XDed { get; set; }

        /// <summary>
        ///     ZC12 - Valor da Dedução
        /// </summary>
        [DFeElement(TipoCampo.De2, "vDed", Id = "ZC12", Min = 3, Max = 15, Ocorrencia = Ocorrencia.Obrigatoria)]
        public decimal VDed { get; set; }

        /// <summary>
        ///     ZC13 - Valor dos Fornecimentos
        /// </summary>
        [DFeElement(TipoCampo.De2, "vFor", Id = "ZC13", Min = 3, Max = 15, Ocorrencia = Ocorrencia.Obrigatoria)]
        public decimal VFor { get; set; }

        /// <summary>
        ///     ZC14 - Valor Total da Dedução
        /// </summary>
        [DFeElement(TipoCampo.De2, "vTotDed", Id = "ZC14", Min = 3, Max = 15, Ocorrencia = Ocorrencia.Obrigatoria)]
        public decimal VTotDed { get; set; }

        /// <summary>
        ///     ZC15 - Valor Líquido dos Fornecimentos
        /// </summary>
        [DFeElement(TipoCampo.De2, "vLiqFor", Id = "ZC15", Min = 3, Max = 15, Ocorrencia = Ocorrencia.Obrigatoria)]
        public decimal VLiqFor { get; set; }

        #endregion
    }
}