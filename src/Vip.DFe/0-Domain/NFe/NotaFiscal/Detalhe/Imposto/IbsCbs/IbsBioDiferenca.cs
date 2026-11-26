using System.ComponentModel;
using Vip.DFe.Attributes;
using Vip.DFe.Enum;
using Vip.DFe.Serializer;

namespace Vip.DFe.NFe.NotaFiscal.Detalhe.Imposto.IbsCbs
{
    /// <summary>
    ///     Grupo de informações sobre mistura de EAC com gasolina A em percentual inferior ou superior ao obrigatório (IBS)
    ///     <br /> gpBioDiferenca - UB89 (ad rem), UB94 (ad valorem)
    /// </summary>
    public sealed class IbsBioDiferenca : GenericClone<IbsBioDiferenca>, INotifyPropertyChanged
    {
        #region Events

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region Properties

        /// <summary>
        ///     Quantidade de Biocombustível (EAC) a recolher ou a ressarcir
        ///     <br /> UB89a (ad rem), UB94a (ad valorem)
        /// </summary>
        [DFeElement(TipoCampo.De4, "qBCBioComb", Min = 5, Max = 16, Ocorrencia = Ocorrencia.Obrigatoria)]
        public decimal QBcBioComb { get; set; }

        /// <summary>
        ///     Valor do IBS correspondente a diferença em relação ao pBioObrigatorio
        ///     <br /> UB89b (ad rem), UB94b (ad valorem)
        /// </summary>
        [DFeElement(TipoCampo.De2, "vIBSDiferenca", Min = 3, Max = 15, Ocorrencia = Ocorrencia.Obrigatoria)]
        public decimal VIbsDiferenca { get; set; }

        #endregion
    }
}