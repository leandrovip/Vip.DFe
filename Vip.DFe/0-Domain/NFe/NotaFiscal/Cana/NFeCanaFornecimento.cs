using System.ComponentModel;
using Vip.DFe.Attributes;
using Vip.DFe.Enum;
using Vip.DFe.Serializer;

namespace Vip.DFe.NFe.NotaFiscal.Cana
{
    /// <summary>
    ///     ZC04 - Grupo Fornecimento diário de cana
    /// </summary>
    public sealed class NFeCanaFornecimento : GenericClone<NFeCanaFornecimento>, INotifyPropertyChanged
    {
        #region Events

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region Properties

        /// <summary>
        ///     ZC05 - Dia
        /// </summary>
        [DFeAttribute(TipoCampo.Int, "dia", Id = "ZC05", Min = 1, Max = 2, Ocorrencia = Ocorrencia.Obrigatoria)]
        public int Dia { get; set; }

        /// <summary>
        ///     ZC06 - Quantidade
        /// </summary>
        [DFeElement(TipoCampo.De10, "qtde", Id = "ZC06", Min = 11, Max = 21, Ocorrencia = Ocorrencia.Obrigatoria)]
        public decimal Qtde { get; set; }

        /// <summary>
        ///     ZC07 - Quantidade Total do Mês
        /// </summary>
        [DFeElement(TipoCampo.De10, "qTotMes", Id = "ZC07", Min = 11, Max = 21, Ocorrencia = Ocorrencia.Obrigatoria)]
        public decimal QTotMes { get; set; }

        /// <summary>
        ///     ZC08 - Quantidade Total Anterior
        /// </summary>
        [DFeElement(TipoCampo.De10, "qTotAnt", Id = "ZC08", Min = 11, Max = 21, Ocorrencia = Ocorrencia.Obrigatoria)]
        public decimal QTotAnt { get; set; }

        /// <summary>
        ///     ZC09 - Quantidade Total Geral
        /// </summary>
        [DFeElement(TipoCampo.De10, "qTotGer", Id = "ZC09", Min = 11, Max = 21, Ocorrencia = Ocorrencia.Obrigatoria)]
        public decimal QTotGer { get; set; }

        #endregion
    }
}