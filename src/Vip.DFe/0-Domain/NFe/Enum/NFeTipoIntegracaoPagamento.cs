using System.ComponentModel;
using Vip.DFe.Attributes;

namespace Vip.DFe.NFe.Enum
{
    /// <summary>
    ///     Tipo de Integração para pagamento
    ///     <para />
    ///     <br />1 - Pagamento integrado com o sistema de automação da empresa (TEF, Comércio Eletrônico)
    ///     <br />2 - Pagamento não integrado com o sistema de automação da empresa(Ex.: equipamento POS)
    /// </summary>
    public enum NFeTipoIntegracaoPagamento
    {
        [DFeEnum("1")] [Description("1 - Pagamento Integrado TEF")]
        PagamentoIntegradoTEF = 1,

        [DFeEnum("2")] [Description("2 - Pagamento não Integrado POS")]
        PagamentoNaoIntegradoPOS = 2,
    }
}