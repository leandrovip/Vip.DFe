using System.ComponentModel;
using Vip.DFe.Conveter;
using Vip.DFe.SAT.Enum;
using Vip.DFe.Shared.Enum;

namespace Vip.DFe.SAT.Configuration
{
    /// <summary>
    ///     Classe SATConfig. Não pode ser herdada
    /// </summary>
    [TypeConverter(typeof(VipExpandableObjectConverter))]
    public sealed class SatConfig
    {
        #region Constructor

        /// <summary>
        ///     Initializes a new instance of the <see cref="SatConfig" /> class.
        /// </summary>
        internal SatConfig(CFeService parent)
        {
            Parent = parent;
            InfCFeVersaoDadosEnt = 0.07M;
            IdeCNPJ = "11111111111111";
            IdeNumeroCaixa = 1;
            IdeTpAmb = TipoAmbiente.Homologacao;
            EmitCNPJ = @"11111111111111";
            EmitIE = string.Empty;
            EmitIM = string.Empty;
            EmitCRegTrib = RegimeTributario.Normal;
            EmitCRegTribISSQN = RegTribIssqn.Nenhum;
            EmitIndRatISSQN = RatIssqn.Nao;
            IsUtf8 = false;
            ValidarNumeroSessaoResposta = false;
            NumeroTentativasValidarSessao = 1;
        }

        #endregion Constructor

        #region Propriedades

        internal CFeService Parent { get; }

        public decimal InfCFeVersaoDadosEnt { get; set; }

        public string IdeCNPJ { get; set; }

        public int IdeNumeroCaixa { get; set; }

        public TipoAmbiente IdeTpAmb { get; set; }

        public string EmitCNPJ { get; set; }

        public string EmitIE { get; set; }

        public string EmitIM { get; set; }

        public RegimeTributario EmitCRegTrib { get; set; }

        public RegTribIssqn EmitCRegTribISSQN { get; set; }

        public RatIssqn EmitIndRatISSQN { get; set; }

        public bool IsUtf8 { get; set; }

        public bool ValidarNumeroSessaoResposta { get; set; }

        public int NumeroTentativasValidarSessao { get; set; }

        public bool RemoverAcentos { get; set; }

        #endregion Propriedades
    }
}