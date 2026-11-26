using System.ComponentModel;
using Vip.DFe.Attributes;

namespace Vip.DFe.Shared.Enum
{
    /// <summary>
    ///     Código do Meio de Pagamento (tPag).
    ///     Fonte: Tabela de Meios de Pagamento (SVRS, 04/03/2026) / IT 2024.002.
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

        [DFeEnum("05")] [Description("5 - Cartão da Loja (Private Label), Crediário Digital, Outros Crediários")]
        CreditoLoja = 5,

        [DFeEnum("10")] [Description("10 - Vale Alimentação")]
        ValeAlimentacao = 10,

        [DFeEnum("11")] [Description("11 - Vale Refeição")]
        ValeRefeicao = 11,

        [DFeEnum("12")] [Description("12 - Vale Presente")]
        ValePresente = 12,

        [DFeEnum("13")] [Description("13 - Vale Combustível")]
        ValeCombustivel = 13,

        [DFeEnum("14")] [Description("14 - Duplicata Mercantil")]
        DuplicataMercantil = 14,

        [DFeEnum("15")] [Description("15 - Boleto Bancário")]
        BoletoBancario = 15,

        [DFeEnum("16")] [Description("16 - Depósito Bancário")]
        DepositoBancario = 16,

        [DFeEnum("17")] [Description("17 - Pagamento Instantâneo (PIX) - Dinâmico")]
        PagamentoInstantaneo = 17,

        [DFeEnum("18")] [Description("18 - TED (Transferência Eletrônica Disponível)")]
        TransferenciaBancaria = 18,

        [DFeEnum("19")] [Description("19 - Programa de Fidelidade")]
        ProgramaFidelidade = 19,

        [DFeEnum("20")] [Description("20 - Pagamento Instantâneo (PIX) - Estático")]
        PagamentoInstantaneoEstatico = 20,

        [DFeEnum("21")] [Description("21 - Crédito em Loja")]
        CreditoEmLoja = 21,

        [DFeEnum("22")] [Description("22 - Pagamento Eletrônico não Informado - falha de hardware do sistema emissor")]
        PagamentoEletronicoNaoInformado = 22,

        [DFeEnum("23")] [Description("23 - Pagamento Instantâneo (PIX) - Automático")]
        PagamentoInstantaneoAutomatico = 23,

        [DFeEnum("24")] [Description("24 - TEF \"Book Transfer\"")]
        TefBookTransfer = 24,

        [DFeEnum("90")] [Description("90 - Sem Pagamento")]
        SemPagamento = 90,

        [DFeEnum("91")] [Description("91 - Pagamento Posterior")]
        PagamentoPosterior = 91,

        [DFeEnum("99")] [Description("99 - Outros")]
        Outros = 99
    }
}
