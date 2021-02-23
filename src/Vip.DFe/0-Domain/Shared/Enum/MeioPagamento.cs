using System.ComponentModel;
using Vip.DFe.Attributes;

namespace Vip.DFe.Shared.Enum
{
    /// <summary>
    ///     Código do Meio de Pagamento
    /// </summary>
    public enum MeioPagamento
    {
        [DFeEnum("01")] [Description("1 - Dinheiro")]
        Dinheiro = 1,

        [DFeEnum("02")] [Description("2 - Cheque")]
        Cheque = 2,

        [DFeEnum("03")] [Description("3 - Cartão de Crédito")]
        CartaodeCredito = 3,

        [DFeEnum("04")] [Description("4 - Cartão de Débito")]
        CartaodeDebito = 4,

        [DFeEnum("05")] [Description("5 - Crédito Loja")]
        CreditoLoja = 5,

        [DFeEnum("10")] [Description("10 - Vale Alimentação")]
        ValeAlimentacao = 10,

        [DFeEnum("11")] [Description("11 - Vale Refeição")]
        ValeRefeicao = 11,

        [DFeEnum("12")] [Description("12 - Vale Presente")]
        ValePresente = 12,

        [DFeEnum("13")] [Description("13 - Vale Combustível")]
        ValeCombustivel = 13,

        [DFeEnum("15")] [Description("15 - Boleto Bancário")]
        BoletoBancario = 15,

        [DFeEnum("16")] [Description("16 - Depósito Bancário")]
        DepositoBancario = 16,

        [DFeEnum("17")] [Description("17 - Pagamento Instantâneo (PIX)")]
        PagamentoInstantaneo = 17,

        [DFeEnum("18")] [Description("18 - Transferência Bancária")]
        TransferenciaBancaria = 18,

        [DFeEnum("19")] [Description("19 - Programa de Fidelidade")]
        ProgramaFidelidade = 19,

        [DFeEnum("90")] [Description("90 - Sem Pagamento")]
        SemPagamento = 90,

        [DFeEnum("99")] [Description("99 - Outros")]
        Outros = 99
    }
}