using System.ComponentModel;
using Vip.DFe.Attributes;

namespace Vip.DFe.NFe.Enum
{
    public enum NFeRefEcfMod
    {
        [DFeEnum("2B")] [Description("2B - Cupom fiscal emitido por maquina registradora")]
        MaquinaRegistradora = 0,

        [DFeEnum("2C")] [Description("2B - Cupom fiscal emitido por PDV")]
        PDV = 1,

        [DFeEnum("2D")] [Description("2B - Cupom fiscal emitido por ECF")]
        ECF = 2
    }
}