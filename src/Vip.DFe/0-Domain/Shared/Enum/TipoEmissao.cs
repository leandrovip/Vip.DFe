using System.ComponentModel;
using Vip.DFe.Attributes;

namespace Vip.DFe.Shared.Enum
{
    public enum TipoEmissao
    {
        [DFeEnum("1")] [Description("1 -Emissão normal")]
        Normal = 1,

        [DFeEnum("2")] [Description("2 - Contingência FS-IA")]
        Contingencia = 2,

        [DFeEnum("3")] [Description("3 - Contingência SCAN [OBSOLETO]")]
        SCAN = 3,

        [DFeEnum("4")] [Description("4 - Contingência EPEC")]
        EPEC = 4,

        [DFeEnum("5")] [Description("5 - Contingência FS-DA")]
        FSDA = 5,

        [DFeEnum("6")] [Description("6 - Contingência SVC-AN")]
        SVCAN = 6,

        [DFeEnum("7")] [Description("7 - Contingência SVC-RS")]
        SVCRS = 7,

        [DFeEnum("9")] [Description("9 - Contingência OFF-LINE NFC-e")]
        OffLine = 9
    }
}