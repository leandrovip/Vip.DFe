using System.ComponentModel;
using Vip.DFe.Attributes;
using Vip.DFe.Enum;
using Vip.DFe.Serializer;

namespace Vip.DFe.NFe.NotaFiscal.Detalhe.Imposto.IbsCbs
{
    /// <summary>
    ///     Grupo de informações da Tributação Monofásica retida anteriormente do IBS (ad rem)
    /// </summary>
    public class IbsAdRemRet : GenericClone<IbsAdRemRet>, INotifyPropertyChanged
    {
        #region Events

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region Properties

        /// <summary>
        ///     UB88a - Quantidade tributada retida anteriormente
        /// </summary>
        [DFeElement(TipoCampo.De4, "qBCMonoRet", Id = "UB88a", Min = 5, Max = 16, Ocorrencia = Ocorrencia.Obrigatoria)]
        public decimal QbcMonoRet { get; set; }

        /// <summary>
        ///     UB88b - Alíquota ad rem do IBS retido anteriormente
        /// </summary>
        [DFeElement(TipoCampo.De4, "adRemIBSRet", Id = "UB88b", Min = 5, Max = 10, Ocorrencia = Ocorrencia.Obrigatoria)]
        public decimal AdRemIbsRet { get; set; }

        /// <summary>
        ///     UB88c - Valor do IBS retido anteriormente
        /// </summary>
        [DFeElement(TipoCampo.De2, "vIBSMonoRet", Id = "UB88c", Min = 3, Max = 15, Ocorrencia = Ocorrencia.Obrigatoria)]
        public decimal VIbsMonoRet { get; set; }

        #endregion
    }
}