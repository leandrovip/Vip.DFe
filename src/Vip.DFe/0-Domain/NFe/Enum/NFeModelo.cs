using System.ComponentModel;
using Vip.DFe.Attributes;

namespace Vip.DFe.NFe.Enum
{
    public enum NFeModelo : byte
    {
        [DFeEnum("55")] [Description("NFe")] NFe = 55,
        [DFeEnum("65")] [Description("NFCe")] NFCe = 65
    }
}