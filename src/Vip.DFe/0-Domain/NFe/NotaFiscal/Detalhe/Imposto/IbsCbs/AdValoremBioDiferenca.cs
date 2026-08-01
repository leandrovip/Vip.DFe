using System.ComponentModel;
using Vip.DFe.Attributes;
using Vip.DFe.Enum;
using Vip.DFe.Serializer;

namespace Vip.DFe.NFe.NotaFiscal.Detalhe.Imposto.IbsCbs
{
    /// <summary>
    ///     Grupo de informações do diferimento do biodiesel na Tributação Monofásica do IBS/CBS (ad valorem)
    ///     <br /> pDif/vDif - IBS (UB94a/UB94b), CBS (UB104a/UB104b)
    /// </summary>
    public class AdValoremBioDiferenca : GenericClone<AdValoremBioDiferenca>, INotifyPropertyChanged
    {
        #region Events

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region Properties

        /// <summary>
        ///     Percentual do diferimento do imposto monofásico
        ///     <br /> UB94a (IBS), UB104a (CBS)
        /// </summary>
        [DFeElement(TipoCampo.De4, "pDif", Min = 5, Max = 10, Ocorrencia = Ocorrencia.Obrigatoria)]
        public decimal PDif { get; set; }

        /// <summary>
        ///     Valor do imposto monofásico diferido
        ///     <br /> UB94b (IBS), UB104b (CBS)
        /// </summary>
        [DFeElement(TipoCampo.De2, "vDif", Min = 3, Max = 15, Ocorrencia = Ocorrencia.Obrigatoria)]
        public decimal VDif { get; set; }

        #endregion
    }
}