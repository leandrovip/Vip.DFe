using System.ComponentModel;
using Vip.DFe.Attributes;

namespace Vip.DFe.NFe.Enum
{
    /// <summary>
    ///     Motivo Desoneração Icms - motDesICMS
    /// </summary>
    public enum NFeMotivoDesoneracao
    {
        /// <summary>
        ///     1 – Táxi
        /// </summary>
        [DFeEnum("1")] [Description("1 - Táxi")]
        Taxi = 1,

        /// <summary>
        ///     2 – Deficiente Físico
        /// </summary>
        [DFeEnum("2")] [Description("2 – Deficiente Físico")]
        DeficienteFisico = 2,

        /// <summary>
        ///     3 – Produtor Agropecuário
        /// </summary>
        [DFeEnum("3")] [Description("3 – Produtor Agropecuário")]
        ProdutorAgropecuario = 3,

        /// <summary>
        ///     4 – Frotista/Locadora
        /// </summary>
        [DFeEnum("4")] [Description("4 – Frotista / Locadora")]
        FrotistaLocadora = 4,

        /// <summary>
        ///     5 – Diplomático/Consular
        /// </summary>
        [DFeEnum("5")] [Description("5 – Diplomático / Consular")]
        DiplomaticoConsular = 5,

        /// <summary>
        ///     6 – Utilitários e Motocicletas da Amazônia Ocidental e Áreas de Livre Comércio (Resolução 714/88 e 790/94 – CONTRAN
        ///     e suas alterações)
        /// </summary>
        [DFeEnum("6")] [Description("6 – Utilitários e Motocicletas da Amazônia Ocidental e Áreas de Livre Comércio")]
        AmazoniaLivreComercio = 6,

        /// <summary>
        ///     7 – SUFRAMA
        /// </summary>
        [DFeEnum("7")] [Description("7 – SUFRAMA")]
        Suframa = 7,

        /// <summary>
        ///     8 – Venda a Órgãos Públicos
        /// </summary>
        [DFeEnum("8")] [Description("8 – Venda a Órgãos Públicos")]
        VendaOrgaosPublicos = 8,

        /// <summary>
        ///     9 – Outros
        /// </summary>
        [DFeEnum("9")] [Description("9 – Outros")]
        Outros = 9,

        /// <summary>
        ///     10- Deficiente Condutor
        /// </summary>
        [DFeEnum("10")] [Description("10- Deficiente Condutor")]
        DeficienteCondutor = 10,

        /// <summary>
        ///     11- Deficiente não condutor
        /// </summary>
        [DFeEnum("11")] [Description("11- Deficiente não condutor")]
        DeficienteNaoCondutor = 11,

        /// <summary>
        ///     16 - Olimpíadas Rio 2016
        /// </summary>
        [DFeEnum("16")] [Description("16 - Olimpíadas Rio 2016")]
        OlimpiadasRio2016 = 16,

        /// <summary>
        ///     90 - Solicitação do Fisco
        /// </summary>
        [DFeEnum("90")] [Description("90 - Solicitação do Fisco")]
        SolicitacaoFisco = 90
    }
}