using System.ComponentModel;
using Vip.DFe.Attributes;

namespace Vip.DFe.NFe.Enum;

/// <summary>
///     Tipo de operação com o ente governamental
/// </summary>
public enum NFeTipoOperacaoGov
{
    [DFeEnum("1")] [Description("1 - Fornecimento com pagamento posterior")]
    FornecimentoPagamentoPosterior = 1,

    [DFeEnum("2")] [Description("2 - Recebimento do pagamento com fornecimento já realizado")]
    RecebimentoComFornecimentoRealizado = 2,

    [DFeEnum("3")] [Description("3 - Fornecimento com pagamento já realizado")]
    FornecimentoPagamentoRealizado = 3,

    [DFeEnum("4")] [Description("4 - Recebimento do pagamento com fornecimento posterior")]
    RecebimentoComFornecimentoPosterior = 4,
}