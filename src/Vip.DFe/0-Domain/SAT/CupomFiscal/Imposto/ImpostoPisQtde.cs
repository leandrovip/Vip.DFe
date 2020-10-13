using System.ComponentModel;
using Vip.DFe.Attributes;
using Vip.DFe.Enum;
using Vip.DFe.SAT.Interfaces;
using Vip.DFe.Serializer;

namespace Vip.DFe.SAT.CupomFiscal.Imposto
{
    public sealed class ImpostoPisQtde : ICFePis
    {
        #region Events

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion Events

        #region Propriedades

        [DFeElement(TipoCampo.Str, "CST", Id = "Q07", Min = 2, Max = 2, Ocorrencia = Ocorrencia.Obrigatoria)]
        public string Cst { get; set; }

        [DFeElement(TipoCampo.De4, "qBCProd", Id = "Q11", Min = 1, Max = 16, Ocorrencia = Ocorrencia.Obrigatoria)]
        public decimal QBcProd { get; set; }

        [DFeElement(TipoCampo.De4, "vAliqProd", Id = "Q12", Min = 5, Max = 15, Ocorrencia = Ocorrencia.Obrigatoria)]
        public decimal VAliqProd { get; set; }

        [DFeElement(TipoCampo.De2, "vPIS", Id = "Q10", Min = 1, Max = 15, Ocorrencia = Ocorrencia.MaiorQueZero)]
        public decimal VPis { get; set; }

        #endregion Propriedades
    }
}