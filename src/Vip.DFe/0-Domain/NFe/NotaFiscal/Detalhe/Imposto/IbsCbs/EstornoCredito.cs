using System.ComponentModel;
using Vip.DFe.Attributes;
using Vip.DFe.Enum;
using Vip.DFe.Serializer;

namespace Vip.DFe.NFe.NotaFiscal.Detalhe.Imposto.IbsCbs
{
    /// <summary>
    ///     Grupo do Estorno de Crédito
    /// </summary>
    public class EstornoCredito : GenericClone<EstornoCredito>, INotifyPropertyChanged
    {
        #region Events

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region Properties

        /// <summary>
        ///     UB117 - Valor do IBS a ser estornado
        /// </summary>
        [DFeElement(TipoCampo.De2, "vIBSEstCred", Id = "UB117", Min = 3, Max = 15, Ocorrencia = Ocorrencia.Obrigatoria)]
        public decimal VIbsEstCred { get; set; }

        /// <summary>
        ///     UB118 - Valor da CBS a ser estornada
        /// </summary>
        [DFeElement(TipoCampo.De2, "vCBSEstCred", Id = "UB118", Min = 3, Max = 15, Ocorrencia = Ocorrencia.Obrigatoria)]
        public decimal VCbsEstCred { get; set; }

        #endregion
    }
}