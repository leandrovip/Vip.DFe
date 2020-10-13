using System.ComponentModel;
using Vip.DFe.Attributes;

namespace Vip.DFe.NFe.Enum
{
    /// <summary>
    ///     Indicador da exigibilidade do ISS
    ///     <para />
    ///     1 - Exigível, 2 - Não incidência; 3 - Isenção; 4 - Exportação; 5 - Imunidade; 6 - Exigibilidade Suspensa por
    ///     Decisão Judicial;
    ///     7 - Exigibilidade Suspensa por Processo Administrativo
    /// </summary>
    public enum NFeIndicadorIss
    {
        [DFeEnum("1")] [Description("1 - Exigível")]
        Exigivel = 1,

        [DFeEnum("2")] [Description("2 - Não incidência")]
        NaoIncidencia = 2,

        [DFeEnum("3")] [Description("3 - Isenção")]
        Isencao = 3,

        [DFeEnum("4")] [Description("4 - Exportação")]
        Exportacao = 4,

        [DFeEnum("5")] [Description("5 - Imunidade")]
        Imunidade = 5,

        [DFeEnum("6")] [Description("6 - Exigibilidade Suspensa por Decisão Judicial")]
        ExigibilidadeSuspensaPorDecisaoJudicial = 6,

        [DFeEnum("7")] [Description("7 - Exigibilidade Suspensa por Processo Administrativo")]
        ExigibilidadeSuspensaPorProcessoAdministrativo = 7
    }
}