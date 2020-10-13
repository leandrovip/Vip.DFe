using System.ComponentModel;

namespace Vip.DFe.Enum
{
    public enum TipoCertificado
    {
        [Description("A1")] A1 = 0,
        [Description("A1 Arquivo")] A1Arquivo = 1,
        [Description("A1 Bytes")] A1Bytes = 2,
        [Description("A3")] A3 = 3
    }
}