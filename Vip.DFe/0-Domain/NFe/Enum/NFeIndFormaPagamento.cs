using System.ComponentModel;
using Vip.DFe.Attributes;

namespace Vip.DFe.NFe.Enum
{
    /// <summary>
    ///     Indicador da Forma de Pagamento
    /// </summary>
    public enum NFeIndFormaPagamento
    {
        [DFeEnum("0")] [Description("0 - Pagamento à Vista")]
        PagamentoVista = 0,

        [DFeEnum("1")] [Description("1 - Pagamento à Prazo")]
        PagamentoPrazo = 1
    }
}