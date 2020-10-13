using Vip.DFe.Attributes;
using Vip.DFe.Enum;
using Vip.DFe.Serializer;

namespace Vip.DFe.NFe.ServAutorizacao.Domain
{
    public sealed class NFeInfRec : GenericClone<NFeInfRec>
    {
        #region Properties

        [DFeElement(TipoCampo.Str, "nRec", Id = "AR08", Min = 1, Max = 15, Ocorrencia = Ocorrencia.Obrigatoria)]
        public string NRec { get; set; }

        [DFeElement(TipoCampo.Int, "tMed", Min = 1, Max = 255, Ocorrencia = Ocorrencia.Obrigatoria)]
        public int TMed { get; set; }

        #endregion Properties
    }
}