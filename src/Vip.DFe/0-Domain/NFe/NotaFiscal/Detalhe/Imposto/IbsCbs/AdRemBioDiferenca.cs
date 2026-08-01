using System.ComponentModel;
using Vip.DFe.Attributes;
using Vip.DFe.Enum;
using Vip.DFe.Serializer;

namespace Vip.DFe.NFe.NotaFiscal.Detalhe.Imposto.IbsCbs
{
    /// <summary>
    ///     Grupo de informações do diferimento do biodiesel na Tributação Monofásica do IBS/CBS (ad rem)
    ///     <br /> pDif/vDif - IBS (UB89a/UB89b), CBS (UB99a/UB99b)
    /// </summary>
    public class AdRemBioDiferenca : GenericClone<AdRemBioDiferenca>, INotifyPropertyChanged
    {
        #region Events

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region Properties

        /// <summary>
        ///     Percentual do diferimento do imposto monofásico
        ///     <br /> UB89a (IBS), UB99a (CBS)
        /// </summary>
        [DFeElement(TipoCampo.De4, "pDif", Min = 5, Max = 10, Ocorrencia = Ocorrencia.Obrigatoria)]
        public decimal PDif { get; set; }

        /// <summary>
        ///     Valor do imposto monofásico diferido
        ///     <br /> UB89b (IBS), UB99b (CBS)
        /// </summary>
        [DFeElement(TipoCampo.De2, "vDif", Min = 3, Max = 15, Ocorrencia = Ocorrencia.Obrigatoria)]
        public decimal VDif { get; set; }

        #endregion
    }
}