using System.ComponentModel;
using Vip.DFe.Attributes;

namespace Vip.DFe.NFe.Enum;

/// <summary>
///     Classificação conforme percentuais definidos no art. 450, § 1º, da
///     LC 214/25 para o cálculo do crédito presumido:
/// </summary>
public enum NFeTipoCredPresIbsZFM
{
    [DFeEnum("0")] [Description("0 - Sem Crédito Presumido")]
    SemCreditoPresumido = 0,

    [DFeEnum("1")] [Description("1 - Bens de consumo final (55%)")]
    BensDeConsumoFinal = 1,

    [DFeEnum("2")] [Description("2 - Bens de capital (75%)")]
    BensDeCapital = 2,

    [DFeEnum("3")] [Description("3 - Bens intermediários (90,25%)")]
    BensIntermediarios = 3,

    [DFeEnum("4")] [Description("4 - Bens de informática e outros definidos em legislação (100%)")]
    BensDeInformaticaOutros = 4,
}