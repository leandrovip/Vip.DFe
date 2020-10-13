using System.ComponentModel;
using Vip.DFe.Attributes;

namespace Vip.DFe.NFe.Enum
{
    public enum NFeDestinoOperacao
    {
        [DFeEnum("1")] [Description("1 - Operação Interna")]
        Interna = 1,

        [DFeEnum("2")] [Description("2 - Operação Interestadual")]
        Interestadual = 2,

        [DFeEnum("3")] [Description("3 - Operação Exterior")]
        Exterior = 3
    }
}