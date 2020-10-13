using System.ComponentModel;
using Vip.DFe.Attributes;
using Vip.DFe.Enum;
using Vip.DFe.SAT.Interfaces;
using Vip.DFe.Serializer;

namespace Vip.DFe.SAT.CupomFiscal.Imposto
{
    public sealed class ImpostoIssqn : GenericClone<ImpostoIssqn>, ICFeImposto
    {
        #region Events

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion Events

        #region Propriedades

        [DFeElement(TipoCampo.De2, "vDeducISSQN", Id = "U02", Min = 3, Max = 15, Ocorrencia = Ocorrencia.Obrigatoria)]
        public decimal VDeducIssqn { get; set; }

        [DFeElement(TipoCampo.De2, "vBC", Id = "U03", Min = 3, Max = 15, Ocorrencia = Ocorrencia.MaiorQueZero)]
        public decimal VBc { get; set; }

        [DFeElement(TipoCampo.De2, "vAliq", Id = "U04", Min = 5, Max = 5, Ocorrencia = Ocorrencia.Obrigatoria)]
        public decimal VAliq { get; set; }

        [DFeElement(TipoCampo.De2, "vISSQN", Id = "U05", Min = 3, Max = 15, Ocorrencia = Ocorrencia.MaiorQueZero)]
        public decimal VIssqn { get; set; }

        [DFeElement(TipoCampo.Int, "cMunFG", Id = "U06", Min = 7, Max = 7, Ocorrencia = Ocorrencia.NaoObrigatoria)]
        public int CMunFg { get; set; }

        [DFeElement(TipoCampo.Str, "cListServ", Id = "U07", Min = 5, Max = 5, Ocorrencia = Ocorrencia.NaoObrigatoria)]
        public string CListServ { get; set; }

        [DFeElement(TipoCampo.Str, "cServTribMun", Id = "U08", Min = 20, Max = 20, Ocorrencia = Ocorrencia.NaoObrigatoria)]
        public string CServTribMun { get; set; }

        [DFeElement(TipoCampo.Int, "cNatOp", Id = "U09", Min = 2, Max = 2, Ocorrencia = Ocorrencia.Obrigatoria)]
        public int CNatOp { get; set; }

        [DFeElement(TipoCampo.Str, "indIncFisc", Id = "U10", Min = 1, Max = 1, Ocorrencia = Ocorrencia.Obrigatoria)]
        public string IndIncFisc { get; set; }

        #endregion Propriedades
    }
}