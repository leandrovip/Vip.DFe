using System.ComponentModel;
using Vip.DFe.Attributes;
using Vip.DFe.Enum;
using Vip.DFe.Serializer;

namespace Vip.DFe.NFe.NotaFiscal.Detalhe.Imposto.IbsCbs
{
    /// <summary>
    ///     Grupo de campos da redução de alíquota
    ///     <br /> pRedAliq/pAliqEfet - IBS na UF (UB27/UB28), IBS Municipal (UB46/UB47), CBS (UB65/UB66)
    /// </summary>
    public class ReducaoAliquota : GenericClone<ReducaoAliquota>, INotifyPropertyChanged
    {
        #region Events

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region Properties

        /// <summary>
        ///     Percentual de redução de alíquota do cClassTrib
        ///     <br /> UB27 (IBS na UF), UB46 (IBS Municipal), UB65 (CBS)
        /// </summary>
        [DFeElement(TipoCampo.De4, "pRedAliq", Min = 5, Max = 10, Ocorrencia = Ocorrencia.Obrigatoria)]
        public decimal PRedAliq { get; set; }

        /// <summary>
        ///     Alíquota Efetiva que será aplicada a Base de Cálculo (em percentual)
        ///     <br /> UB28 (IBS na UF), UB47 (IBS Municipal), UB66 (CBS)
        /// </summary>
        [DFeElement(TipoCampo.De4, "pAliqEfet", Min = 5, Max = 10, Ocorrencia = Ocorrencia.Obrigatoria)]
        public decimal PAliqEfet { get; set; }

        #endregion
    }
}