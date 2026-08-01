using System.ComponentModel;
using Vip.DFe.Attributes;
using Vip.DFe.Enum;
using Vip.DFe.Serializer;

namespace Vip.DFe.NFe.NotaFiscal.Detalhe.Imposto.IbsCbs
{
    /// <summary>
    ///     Grupo de informações da Tributação Monofásica retida anteriormente da CBS (ad rem)
    /// </summary>
    public class CbsAdRemRet : GenericClone<CbsAdRemRet>, INotifyPropertyChanged
    {
        #region Events

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region Properties

        /// <summary>
        ///     UB98a - Quantidade tributada retida anteriormente
        /// </summary>
        [DFeElement(TipoCampo.De4, "qBCMonoRet", Id = "UB98a", Min = 5, Max = 16, Ocorrencia = Ocorrencia.Obrigatoria)]
        public decimal QbcMonoRet { get; set; }

        /// <summary>
        ///     UB98b - Alíquota ad rem da CBS retida anteriormente
        /// </summary>
        [DFeElement(TipoCampo.De4, "adRemCBSRet", Id = "UB98b", Min = 5, Max = 10, Ocorrencia = Ocorrencia.Obrigatoria)]
        public decimal AdRemCbsRet { get; set; }

        /// <summary>
        ///     UB98c - Valor da CBS retida anteriormente
        /// </summary>
        [DFeElement(TipoCampo.De2, "vCBSMonoRet", Id = "UB98c", Min = 3, Max = 15, Ocorrencia = Ocorrencia.Obrigatoria)]
        public decimal VCbsMonoRet { get; set; }

        #endregion
    }
}