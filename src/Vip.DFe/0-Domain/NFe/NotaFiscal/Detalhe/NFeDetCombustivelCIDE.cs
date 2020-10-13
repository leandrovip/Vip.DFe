using System.ComponentModel;
using Vip.DFe.Attributes;
using Vip.DFe.Enum;
using Vip.DFe.Serializer;

namespace Vip.DFe.NFe.NotaFiscal.Detalhe
{
    public class NFeDetCombustivelCIDE : GenericClone<NFeDetCombustivelCIDE>, INotifyPropertyChanged
    {
        #region Events

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region Properties

        /// <summary>
        ///     LA08 - BC da CIDE
        /// </summary>
        [DFeElement(TipoCampo.De4, "qBCProd", Id = "LA08", Min = 5, Max = 16, Ocorrencia = Ocorrencia.Obrigatoria)]
        public decimal QBCProd { get; set; }

        /// <summary>
        ///     LA09 - Valor da alíquota da CIDE
        /// </summary>
        [DFeElement(TipoCampo.De4, "vAliqProd", Id = "LA09", Min = 4, Max = 15, Ocorrencia = Ocorrencia.Obrigatoria)]
        public decimal vAliqProd { get; set; }

        /// <summary>
        ///     LA10 - Valor da CIDE
        /// </summary>
        [DFeElement(TipoCampo.De2, "vCIDE", Id = "LA10", Min = 3, Max = 15, Ocorrencia = Ocorrencia.Obrigatoria)]
        public decimal vCIDE { get; set; }

        #endregion
    }
}