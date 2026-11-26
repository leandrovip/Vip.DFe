using System.ComponentModel;
using Vip.DFe.Attributes;
using Vip.DFe.Enum;
using Vip.DFe.Serializer;

namespace Vip.DFe.NFe.NotaFiscal.Detalhe.Imposto.IbsCbs
{
    /// <summary>
    ///     Grupo de informações sobre mistura de EAC com gasolina A em percentual inferior ou superior ao obrigatório (CBS)
    ///     <br /> gpBioDiferenca - UB99 (ad rem), UB104 (ad valorem)
    /// </summary>
    public sealed class CbsBioDiferenca : GenericClone<CbsBioDiferenca>, INotifyPropertyChanged
    {
        #region Events

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region Properties

        /// <summary>
        ///     Quantidade de Biocombustível (EAC) a recolher ou a ressarcir
        ///     <br /> UB99a (ad rem), UB104a (ad valorem)
        /// </summary>
        [DFeElement(TipoCampo.De4, "qBCBioComb", Min = 5, Max = 16, Ocorrencia = Ocorrencia.Obrigatoria)]
        public decimal QBcBioComb { get; set; }

        /// <summary>
        ///     Valor da CBS correspondente a diferença em relação ao pBioObrigatorio
        ///     <br /> UB99b (ad rem), UB104b (ad valorem)
        /// </summary>
        [DFeElement(TipoCampo.De2, "vCBSDiferenca", Min = 3, Max = 15, Ocorrencia = Ocorrencia.Obrigatoria)]
        public decimal VCbsDiferenca { get; set; }

        #endregion
    }
}