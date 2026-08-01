using System.ComponentModel;
using Vip.DFe.Attributes;

namespace Vip.DFe.NFe.Enum;

/// <summary>
///     Tipo de Nota de Débito
/// </summary>
public enum NFeTipoNotaDebito
{
    /// <summary>
    ///     01 - Transferência de créditos para Cooperativas
    /// </summary>
    [DFeEnum("01")] [Description("01 - Transferência de créditos para Cooperativas")]
    TransferenciaCreditosCooperativas = 1,

    /// <summary>
    ///     02 - Anulação de Crédito por Saídas Imunes/Isentas
    /// </summary>
    [DFeEnum("02")] [Description("02 - Anulação de Crédito por Saídas Imunes/Isentas")]
    AnulacaoCreditoSaidasImunesIsentas = 2,

    /// <summary>
    ///     03 - Débitos de notas fiscais não processadas na apuração
    /// </summary>
    [DFeEnum("03")] [Description("03 - Débitos de notas fiscais não processadas na apuração")]
    DebitoNotasFiscaisNaoProcessadas = 3,

    /// <summary>
    ///     04 - Multa e juros
    /// </summary>
    [DFeEnum("04")] [Description("04 - Multa e juros")]
    MultaJuros = 4,

    /// <summary>
    ///     05 - Transferência de crédito na sucessão
    /// </summary>
    [DFeEnum("05")] [Description("05 - Transferência de crédito na sucessão")]
    TransferenciaCreditoSucessao = 5,

    /// <summary>
    ///     06 - Pagamento antecipado
    /// </summary>
    [DFeEnum("06")] [Description("06 - Pagamento antecipado")]
    PagamentoAntecipado = 6,

    /// <summary>
    ///     07 - Perda em estoque (Perecimento, Perda, Furto, Roubo)
    /// </summary>
    [DFeEnum("07")] [Description("07 - Perda em estoque (Perecimento, Perda, Furto, Roubo)")]
    PerdaEmEstoque = 7,

    /// <summary>
    ///     08 - Desenquadramento do SN
    /// </summary>
    [DFeEnum("08")] [Description("08 - Desenquadramento do SN")]
    DesenquadramentoSimplesNacional = 8,
}