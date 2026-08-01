using System.ComponentModel;
using Vip.DFe.Attributes;
using Vip.DFe.Enum;
using Vip.DFe.Serializer;

namespace Vip.DFe.NFe.NotaFiscal.Detalhe.Imposto.IbsCbs
{
    /// <summary>
    ///     Grupo de informações da Tributação Monofásica retida anteriormente da CBS (ad valorem)
    /// </summary>
    public class CbsAdValoremRet : GenericClone<CbsAdValoremRet>, INotifyPropertyChanged
    {
        #region Events

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region Properties

        /// <summary>
        ///     UB103a - Valor da Base de Cálculo retida anteriormente
        /// </summary>
        [DFeElement(TipoCampo.De2, "vBCMonoRet", Id = "UB103a", Min = 3, Max = 15, Ocorrencia = Ocorrencia.Obrigatoria)]
        public decimal VBcMonoRet { get; set; }

        /// <summary>
        ///     UB103b - Alíquota ad valorem da CBS retida anteriormente
        /// </summary>
        [DFeElement(TipoCampo.De4, "pCBSMonoRet", Id = "UB103b", Min = 5, Max = 10, Ocorrencia = Ocorrencia.Obrigatoria)]
        public decimal PCbsMonoRet { get; set; }

        /// <summary>
        ///     UB103c - Valor da CBS retida anteriormente
        /// </summary>
        [DFeElement(TipoCampo.De2, "vCBSMonoRet", Id = "UB103c", Min = 3, Max = 15, Ocorrencia = Ocorrencia.Obrigatoria)]
        public decimal VCbsMonoRet { get; set; }

        #endregion
    }
}