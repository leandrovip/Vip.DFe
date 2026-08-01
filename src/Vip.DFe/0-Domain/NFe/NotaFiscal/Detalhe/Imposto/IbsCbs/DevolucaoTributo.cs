using System.ComponentModel;
using Vip.DFe.Attributes;
using Vip.DFe.Enum;
using Vip.DFe.Serializer;

namespace Vip.DFe.NFe.NotaFiscal.Detalhe.Imposto.IbsCbs
{
    /// <summary>
    ///     Grupo de Informações da devolução de tributos
    ///     <br /> pDevTrib/vDevTrib - IBS na UF (UB24a/UB25), IBS Municipal (UB43a/UB44), CBS (UB62a/UB63)
    /// </summary>
    public class DevolucaoTributo : GenericClone<DevolucaoTributo>, INotifyPropertyChanged
    {
        #region Events

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region Properties

        /// <summary>
        ///     Percentual de devolução do tributo, conforme LC 214/25 art. 118
        ///     <br /> UB24a (IBS na UF), UB43a (IBS Municipal), UB62a (CBS)
        /// </summary>
        [DFeElement(TipoCampo.De4, "pDevTrib", Min = 5, Max = 10, Ocorrencia = Ocorrencia.NaoObrigatoria)]
        public decimal PDevTrib { get; set; }

        /// <summary>
        ///     Valor do tributo devolvido ("cashback" de desconto na própria Nota Fiscal / Fatura)
        ///     <br /> UB25 (IBS na UF), UB44 (IBS Municipal), UB63 (CBS)
        /// </summary>
        [DFeElement(TipoCampo.De2, "vDevTrib", Min = 3, Max = 15, Ocorrencia = Ocorrencia.Obrigatoria)]
        public decimal VDevTrib { get; set; }

        #endregion
    }
}