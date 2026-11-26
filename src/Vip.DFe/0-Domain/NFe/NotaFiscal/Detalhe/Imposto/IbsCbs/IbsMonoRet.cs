using System.ComponentModel;
using Vip.DFe.Attributes;
using Vip.DFe.Enum;
using Vip.DFe.Serializer;

namespace Vip.DFe.NFe.NotaFiscal.Detalhe.Imposto.IbsCbs
{
    /// <summary>
    ///     Grupo de informações da Tributação Monofásica retida anteriormente do IBS (ad rem e ad valorem)
    ///     <br /> gMonoRet - UB88 (ad rem), UB93 (ad valorem)
    /// </summary>
    public sealed class IbsMonoRet : GenericClone<IbsMonoRet>, INotifyPropertyChanged
    {
        #region Events

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region Properties

        /// <summary>
        ///     Valor do IBS retido anteriormente
        ///     <br /> UB88a (ad rem), UB93a (ad valorem)
        /// </summary>
        [DFeElement(TipoCampo.De2, "vIBSMonoRet", Min = 3, Max = 15, Ocorrencia = Ocorrencia.Obrigatoria)]
        public decimal VIbsMonoRet { get; set; }

        #endregion
    }
}
