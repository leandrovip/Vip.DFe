using System.ComponentModel;
using Vip.DFe.Attributes;

namespace Vip.DFe.NFe.Enum
{
    public enum NFeTipo
    {
        [DFeEnum("0")] [Description("0 - Entrada")]
        Entrada = 0,

        [DFeEnum("1")] [Description("1 - Saída")]
        Saida = 1
    }
}