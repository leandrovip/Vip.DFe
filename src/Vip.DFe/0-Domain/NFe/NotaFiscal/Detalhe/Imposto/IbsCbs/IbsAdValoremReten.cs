using System.ComponentModel;
using Vip.DFe.Attributes;
using Vip.DFe.Enum;
using Vip.DFe.Serializer;

namespace Vip.DFe.NFe.NotaFiscal.Detalhe.Imposto.IbsCbs
{
    /// <summary>
    ///     Grupo de informações da Tributação Monofásica sujeita a retenção do IBS (ad valorem)
    /// </summary>
    public class IbsAdValoremReten : GenericClone<IbsAdValoremReten>, INotifyPropertyChanged
    {
        #region Events

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region Properties

        /// <summary>
        ///     UB92a - Valor da Base de Cálculo sujeita a retenção
        /// </summary>
        [DFeElement(TipoCampo.De2, "vBCMonoReten", Id = "UB92a", Min = 3, Max = 15, Ocorrencia = Ocorrencia.Obrigatoria)]
        public decimal VBcMonoReten { get; set; }

        /// <summary>
        ///     UB92b - Alíquota ad valorem do IBS sujeito a retenção
        /// </summary>
        [DFeElement(TipoCampo.De4, "pIBSMonoReten", Id = "UB92b", Min = 5, Max = 10, Ocorrencia = Ocorrencia.Obrigatoria)]
        public decimal PIbsMonoReten { get; set; }

        /// <summary>
        ///     UB92c - Valor do IBS monofásico sujeito a retenção
        /// </summary>
        [DFeElement(TipoCampo.De2, "vIBSMonoReten", Id = "UB92c", Min = 3, Max = 15, Ocorrencia = Ocorrencia.Obrigatoria)]
        public decimal VIbsMonoReten { get; set; }

        #endregion
    }
}