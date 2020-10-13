using System.ComponentModel;
using Vip.DFe.Attributes;

namespace Vip.DFe.NFe.Enum
{
    public enum NFeRefNfpMod
    {
        [DFeEnum("01")] [Description("01 - Nota Fiscal")]
        NF = 1,

        [DFeEnum("04")] [Description("04 - Nota Fiscal Produtor")]
        NFProdutor = 4
    }
}