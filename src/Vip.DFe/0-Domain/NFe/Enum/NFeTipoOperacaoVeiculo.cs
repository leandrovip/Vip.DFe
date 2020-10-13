using System.ComponentModel;
using Vip.DFe.Attributes;

namespace Vip.DFe.NFe.Enum
{
    public enum NFeTipoOperacaoVeiculo
    {
        [DFeEnum("0")] [Description("0 - Outros")]
        Outros = 0,

        [DFeEnum("1")] [Description("1 - Venda Concessionária")]
        VendaConcessionaria = 1,

        [DFeEnum("2")] [Description("2 - Faturamento direto para consumidor final")]
        FaturamentoDiretoConsumidorFinal = 2,

        [DFeEnum("3")] [Description("3 - Venda direta para grandes consumidores")]
        VendaDiretaGrandesConsumidores = 3,
    }
}