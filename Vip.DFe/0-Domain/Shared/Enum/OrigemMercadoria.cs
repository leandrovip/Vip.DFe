using System.ComponentModel;
using Vip.DFe.Attributes;

namespace Vip.DFe.Shared.Enum
{
    public enum OrigemMercadoria
    {
        [DFeEnum("0")] [Description("Nacional")]
        Nacional = 0,

        [DFeEnum("1")] [Description("Estrangeira importa��o direta")]
        EstrangeiraImportacaoDireta = 1,

        [DFeEnum("2")] [Description("Estrangeira adquirida no Brasil")]
        EstrangeiraAdquiridaBrasil = 2,

        [DFeEnum("3")] [Description("Nacional conte�do importado superior 40%")]
        NacionalConteudoImportacaoSuperior40 = 3,

        [DFeEnum("4")] [Description("Nacional processos b�sicos")]
        NacionalProcessosBasicos = 4,

        [DFeEnum("5")] [Description("Nacional conte�do importado inferior 40%")]
        NacionalConteudoImportacaoInferiorIgual40 = 5,

        [DFeEnum("6")] [Description("Estrangeira importa��o direta sem similar")]
        EstrangeiraImportacaoDiretaSemSimilar = 6,

        [DFeEnum("7")] [Description("Estrangeira aduirida no Brasil sem similar")]
        EstrangeiraAdquiridaBrasilSemSimilar = 7,

        [DFeEnum("8")] [Description("Nacional conte�do importado superior 70%")]
        NacionalConteudoImportacaoSuperior70 = 8
    }
}