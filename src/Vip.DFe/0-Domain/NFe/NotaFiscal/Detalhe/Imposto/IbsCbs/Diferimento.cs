using System.ComponentModel;
using Vip.DFe.Attributes;
using Vip.DFe.Enum;
using Vip.DFe.Serializer;

namespace Vip.DFe.NFe.NotaFiscal.Detalhe.Imposto.IbsCbs
{
    /// <summary>
    ///     Grupo de campos do Diferimento
    ///     <br /> pDif/vDif - IBS na UF (UB22/UB23), IBS Municipal (UB41/UB42), CBS (UB60/UB61)
    /// </summary>
    public class Diferimento : GenericClone<Diferimento>, INotifyPropertyChanged
    {
        #region Events

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region Properties

        /// <summary>
        ///     Percentual do diferimento
        ///     <br /> UB22 (IBS na UF), UB41 (IBS Municipal), UB60 (CBS)
        /// </summary>
        [DFeElement(TipoCampo.De4, "pDif", Min = 5, Max = 10, Ocorrencia = Ocorrencia.Obrigatoria)]
        public decimal PDif { get; set; }

        /// <summary>
        ///     Valor do diferimento
        ///     <br /> UB23 (IBS na UF), UB42 (IBS Municipal), UB61 (CBS)
        /// </summary>
        [DFeElement(TipoCampo.De2, "vDif", Min = 3, Max = 15, Ocorrencia = Ocorrencia.Obrigatoria)]
        public decimal VDif { get; set; }

        #endregion
    }
}