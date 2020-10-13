using System.ComponentModel;
using Vip.DFe.Attributes;

namespace Vip.DFe.SAT.Enum
{
    /// <summary>
    ///     Indicador de rateio do Desconto sobre subtotal entre itens sujeitos à tributação pelo ISSQN.
    ///     <para />
    ///     <br />S = Sim
    ///     <br />N = Não
    /// </summary>
    public enum RatIssqn
    {
        [DFeEnum("S")] [Description("Sim")] Sim,
        [DFeEnum("N")] [Description("Não")] Nao
    }
}