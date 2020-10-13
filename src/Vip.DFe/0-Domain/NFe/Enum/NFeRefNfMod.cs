using System.ComponentModel;
using Vip.DFe.Attributes;

namespace Vip.DFe.NFe.Enum
{
    public enum NFeRefNfMod
    {
        [DFeEnum("01")] [Description("Modelo 01")]
        Modelo1 = 1,

        [DFeEnum("02")] [Description("Modelo 02")]
        Modelo2 = 2,
    }
}