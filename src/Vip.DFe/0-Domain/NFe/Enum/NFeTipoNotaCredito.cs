using System.ComponentModel;
using Vip.DFe.Attributes;

namespace Vip.DFe.NFe.Enum;

/// <summary>
///     Tipo de Nota de Crédito
/// </summary>
public enum NFeTipoNotaCredito
{
    /// <summary>
    ///     01 - Multa e juros
    /// </summary>
    [DFeEnum("01")] [Description("01 - Multa e juros")]
    MultaJuros = 1,

    /// <summary>
    ///     02 - Apropriação de crédito presumido de IBS sobre o saldo devedor na ZFM (art. 450, § 1º, LC 214/25)
    /// </summary>
    [DFeEnum("02")] [Description("02 - Apropriação de crédito presumido de IBS sobre o saldo devedor na ZFM (art. 450, 1º, LC 214/25)")]
    ApropriacaoCreditoPresumidoIBSZFM = 2,

    /// <summary>
    ///     03 - Retorno por recusa total na entrega ou por não localização do destinatário na tentativa de entrega
    /// </summary>
    [DFeEnum("03")] [Description("03 - Retorno por recusa total na entrega ou por não localização do destinatário na tentativa de entrega")]
    RetornoEntrega = 3,

    /// <summary>
    ///     04 - Redução de valores
    /// </summary>
    [DFeEnum("04")] [Description("04 - Redução de valores")]
    ReducaoValores = 4,

    /// <summary>
    ///     05 - Transferência de crédito na sucessão
    /// </summary>
    [DFeEnum("05")] [Description("05 - Transferência de crédito na sucessão")]
    TransferenciaCreditoSucessao = 5,

    /// <summary>
    ///     06 - Retorno por recusa parcial na entrega
    /// </summary>
    [DFeEnum("06")] [Description("06 - Retorno por recusa parcial na entrega")]
    RetornoRecusaParcialEntrega = 6,
}