using System.ComponentModel;
using Vip.DFe.Attributes;
using Vip.DFe.Enum;
using Vip.DFe.Serializer;

namespace Vip.DFe.SAT.CupomFiscal.Imposto
{
    public sealed class ImpostoPisSt : GenericClone<ImpostoPisSt>, INotifyPropertyChanged
    {
        #region Events

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion Events

        #region Propriedades

        [DFeElement(TipoCampo.De2, "vBC", Id = "R02", Min = 3, Max = 15, Ocorrencia = Ocorrencia.Obrigatoria)]
        public decimal VBc { get; set; }

        [DFeElement(TipoCampo.De4, "pPIS", Id = "R03", Min = 5, Max = 5, Ocorrencia = Ocorrencia.Obrigatoria)]
        public decimal PPis { get; set; }

        [DFeElement(TipoCampo.De4, "qBCProd", Id = "R04", Min = 1, Max = 16, Ocorrencia = Ocorrencia.MaiorQueZero)]
        public decimal QBcProd { get; set; }

        [DFeElement(TipoCampo.De4, "vAliqProd", Id = "R05", Min = 5, Max = 15, Ocorrencia = Ocorrencia.MaiorQueZero)]
        public decimal VAliqProd { get; set; }

        [DFeElement(TipoCampo.De2, "vPIS", Id = "R06", Min = 1, Max = 15, Ocorrencia = Ocorrencia.MaiorQueZero)]
        public decimal VPis { get; set; }

        #endregion Propriedades

        #region Methods

        private bool ShouldSerializeVBc()
        {
            return VAliqProd == 0;
        }

        private bool ShouldSerializePPis()
        {
            return VAliqProd == 0;
        }

        #endregion Methods
    }
}