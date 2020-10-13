using System.ComponentModel;
using Vip.DFe.Attributes;
using Vip.DFe.Enum;
using Vip.DFe.Serializer;

namespace Vip.DFe.NFe.NotaFiscal.Detalhe.Imposto.Federal
{
    public sealed class II : GenericClone<II>, INotifyPropertyChanged
    {
        #region Events

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region Properties

        /// <summary>
        ///     P02 - Valor BC do Imposto de Importação
        /// </summary>
        [DFeElement(TipoCampo.De2, "vBC", Id = "P02", Min = 3, Max = 15, Ocorrencia = Ocorrencia.Obrigatoria)]
        public decimal VBc { get; set; }

        /// <summary>
        ///     P03 - Valor despesas aduaneiras
        /// </summary>
        [DFeElement(TipoCampo.De2, "vDespAdu", Id = "P03", Min = 3, Max = 15, Ocorrencia = Ocorrencia.Obrigatoria)]
        public decimal VDespAdu { get; set; }

        /// <summary>
        ///     P04 - Valor Imposto de Importação
        /// </summary>
        [DFeElement(TipoCampo.De2, "vII", Id = "P04", Min = 3, Max = 15, Ocorrencia = Ocorrencia.Obrigatoria)]
        public decimal VIi { get; set; }

        /// <summary>
        ///     P05 - Valor Imposto sobre Operações Financeiras
        /// </summary>
        [DFeElement(TipoCampo.De2, "vIOF", Id = "P05", Min = 3, Max = 15, Ocorrencia = Ocorrencia.Obrigatoria)]
        public decimal VIof { get; set; }

        #endregion
    }
}