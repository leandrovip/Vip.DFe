using System.ComponentModel;
using Vip.DFe.Attributes;
using Vip.DFe.Enum;
using Vip.DFe.Serializer;

namespace Vip.DFe.NFe.NotaFiscal.Detalhe.Imposto.IbsCbs
{
    /// <summary>
    ///     Grupo de informações da Tributação Monofásica retida anteriormente da CBS (ad rem e ad valorem)
    ///     <br /> gMonoRet - UB98 (ad rem), UB103 (ad valorem)
    /// </summary>
    public sealed class CbsMonoRet : GenericClone<CbsMonoRet>, INotifyPropertyChanged
    {
        #region Events

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region Properties

        /// <summary>
        ///     Valor da CBS retida anteriormente
        ///     <br /> UB98a (ad rem), UB103a (ad valorem)
        /// </summary>
        [DFeElement(TipoCampo.De2, "vCBSMonoRet", Min = 3, Max = 15, Ocorrencia = Ocorrencia.Obrigatoria)]
        public decimal VCbsMonoRet { get; set; }

        #endregion
    }
}