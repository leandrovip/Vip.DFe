using System.ComponentModel;
using Vip.DFe.Attributes;

namespace Vip.DFe.NFe.Enum
{
    public enum NFeIndIntermed
    {
        [DFeEnum("0")] [Description("0 - Operação sem Intermediador")]
        SemIntermediador = 0,

        [DFeEnum("1")] [Description("1 - Operação em site ou plataforma de terceiros")]
        ComIntermediador = 1,
    }
}