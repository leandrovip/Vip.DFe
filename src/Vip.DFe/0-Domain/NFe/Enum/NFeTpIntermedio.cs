using System.ComponentModel;
using Vip.DFe.Attributes;

namespace Vip.DFe.NFe.Enum
{
    public enum NFeTpIntermedio
    {
        [DFeEnum("1")] [Description("1 - Importação por conta própria")]
        ContaPropria = 1,

        [DFeEnum("2")] [Description("2 - Importação por conta e ordem")]
        ContaeOrdem = 2,

        [DFeEnum("3")] [Description("3 - Importação por encomenda")]
        PorEncomenda = 3
    }
}