using System.ComponentModel;
using Vip.DFe.Attributes;
using Vip.DFe.Enum;
using Vip.DFe.Serializer;

namespace Vip.DFe.NFe.NotaFiscal.Detalhe.Imposto.IbsCbs
{
    /// <summary>
    ///     Grupo de informações da Tributação Monofásica sujeita a retenção do IBS (ad rem)
    /// </summary>
    public class IbsAdRemReten : GenericClone<IbsAdRemReten>, INotifyPropertyChanged
    {
        #region Events

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region Properties

        /// <summary>
        ///     UB87a - Quantidade tributada sujeita a retenção
        /// </summary>
        [DFeElement(TipoCampo.De4, "qBCMonoReten", Id = "UB87a", Min = 5, Max = 16, Ocorrencia = Ocorrencia.Obrigatoria)]
        public decimal QbcMonoReten { get; set; }

        /// <summary>
        ///     UB87b - Alíquota ad rem do IBS sujeito a retenção
        /// </summary>
        [DFeElement(TipoCampo.De4, "adRemIBSReten", Id = "UB87b", Min = 5, Max = 10, Ocorrencia = Ocorrencia.Obrigatoria)]
        public decimal AdRemIbsReten { get; set; }

        /// <summary>
        ///     UB87c - Valor do IBS monofásico sujeito a retenção
        /// </summary>
        [DFeElement(TipoCampo.De2, "vIBSMonoReten", Id = "UB87c", Min = 3, Max = 15, Ocorrencia = Ocorrencia.Obrigatoria)]
        public decimal VIbsMonoReten { get; set; }

        #endregion
    }
}