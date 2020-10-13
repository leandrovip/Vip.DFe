using System.ComponentModel;
using Vip.DFe.Attributes;
using Vip.DFe.Enum;
using Vip.DFe.SAT.Interfaces;
using Vip.DFe.Serializer;

namespace Vip.DFe.SAT.CupomFiscal.Imposto
{
    public sealed class ImpostoPisOutr : ICFePis
    {
        #region Events

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion Events

        #region Propriedades

        [DFeElement(TipoCampo.Str, "CST", Id = "Q07", Min = 2, Max = 2, Ocorrencia = Ocorrencia.Obrigatoria)]
        public string Cst { get; set; }

        [DFeElement(TipoCampo.De4, "qBCProd", Id = "Q11", Min = 1, Max = 16, Ocorrencia = Ocorrencia.MaiorQueZero)]
        public decimal QBcProd { get; set; }

        [DFeElement(TipoCampo.De4, "vAliqProd", Id = "Q12", Min = 5, Max = 15, Ocorrencia = Ocorrencia.MaiorQueZero)]
        public decimal VAliqProd { get; set; }

        [DFeElement(TipoCampo.De2, "vBC", Id = "Q08", Min = 3, Max = 15, Ocorrencia = Ocorrencia.Obrigatoria)]
        public decimal VBc { get; set; }

        [DFeElement(TipoCampo.De4, "pPIS", Id = "Q09", Min = 5, Max = 5, Ocorrencia = Ocorrencia.Obrigatoria)]
        public decimal PPis { get; set; }

        [DFeElement(TipoCampo.De2, "vPIS", Id = "Q10", Min = 1, Max = 15, Ocorrencia = Ocorrencia.MaiorQueZero)]
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