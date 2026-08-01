using System.ComponentModel;
using Vip.DFe.Attributes;
using Vip.DFe.Enum;
using Vip.DFe.Serializer;

namespace Vip.DFe.NFe.NotaFiscal.Detalhe.Imposto.IbsCbs
{
    /// <summary>
    ///     Grupo de informações da Tributação Monofásica retida anteriormente do IBS (ad valorem)
    /// </summary>
    public class IbsAdValoremRet : GenericClone<IbsAdValoremRet>, INotifyPropertyChanged
    {
        #region Events

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region Properties

        /// <summary>
        ///     UB93a - Valor da Base de Cálculo retida anteriormente
        /// </summary>
        [DFeElement(TipoCampo.De2, "vBCMonoRet", Id = "UB93a", Min = 3, Max = 15, Ocorrencia = Ocorrencia.Obrigatoria)]
        public decimal VBcMonoRet { get; set; }

        /// <summary>
        ///     UB93b - Alíquota ad valorem do IBS retido anteriormente
        /// </summary>
        [DFeElement(TipoCampo.De4, "pIBSMonoRet", Id = "UB93b", Min = 5, Max = 10, Ocorrencia = Ocorrencia.Obrigatoria)]
        public decimal PIbsMonoRet { get; set; }

        /// <summary>
        ///     UB93c - Valor do IBS retido anteriormente
        /// </summary>
        [DFeElement(TipoCampo.De2, "vIBSMonoRet", Id = "UB93c", Min = 3, Max = 15, Ocorrencia = Ocorrencia.Obrigatoria)]
        public decimal VIbsMonoRet { get; set; }

        #endregion
    }
}