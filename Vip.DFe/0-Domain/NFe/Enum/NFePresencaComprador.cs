using System.ComponentModel;
using Vip.DFe.Attributes;

namespace Vip.DFe.NFe.Enum
{
    public enum NFePresencaComprador
    {
        [DFeEnum("0")] [Description("0 - Não")]
        Nao = 0,

        [DFeEnum("1")] [Description("1 - Presencial")]
        Presencial = 1,

        [DFeEnum("2")] [Description("2 - Não Presencial, pela internet")]
        Internet = 2,

        [DFeEnum("3")] [Description("3 - Não Presencial por Teleatendimento")]
        Teleatendimento = 3,

        [DFeEnum("4")] [Description("4 - NFCe com entrega a domicílio")]
        NFCeEntregaDomicilio = 4,

        [DFeEnum("5")] [Description("5 - Presencial fora do estabelecimento")]
        PresencialForaEstabelecimento = 5,

        [DFeEnum("9")] [Description("9 - Outros")]
        Outros = 9
    }
}