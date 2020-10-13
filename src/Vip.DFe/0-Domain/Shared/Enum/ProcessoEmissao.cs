using System.ComponentModel;
using Vip.DFe.Attributes;

namespace Vip.DFe.Shared.Enum
{
    public enum ProcessoEmissao
    {
        [DFeEnum("0")] [Description("0 - Aplicativo do contribuinte")]
        AplicativoContribuinte = 0,

        [DFeEnum("1")] [Description("1 - Avulsa pelo Fisco")]
        AvulsaFisco = 1,

        [DFeEnum("2")] [Description("2 - Avulsa pelo site do fisco")]
        AvulsaSiteFisco = 2,

        [DFeEnum("3")] [Description("3 - Aplicativo do fisco")]
        AplicativoFisco = 3
    }
}