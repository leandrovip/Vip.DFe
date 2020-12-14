using System.ComponentModel;
using Vip.DFe.Attributes;

namespace Vip.DFe.NFe.Enum
{
    public enum NFeFinalidade
    {
        [DFeEnum("1")] [Description("1 - NFe Normal")]
        Normal = 1,

        [DFeEnum("2")] [Description("2 - NFe Complementar")]
        Complementar = 2,

        [DFeEnum("3")] [Description("3 - NFe de Ajuste")]
        Ajuste = 3,

        [DFeEnum("4")] [Description("4 - Devolução de Mercadoria")]
        Devolucao = 4
    }
}