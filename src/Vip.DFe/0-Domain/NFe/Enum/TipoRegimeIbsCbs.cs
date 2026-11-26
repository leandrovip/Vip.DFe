using System.ComponentModel;
using Vip.DFe.Attributes;

namespace Vip.DFe.NFe.Enum;

/// <summary>
///     C21a - Tipo de Regime de Apuração do IBS e da CBS (NT 2026.005)
/// </summary>
public enum TipoRegimeIbsCbs
{
    [DFeEnum("1")] [Description("1 - Contribuinte do Simples Nacional (CRT=1 ou 2) com opção pelo regime regular do IBS e da CBS")]
    SimplesNacionalComRegimeRegular = 1,

    [DFeEnum("2")] [Description("2 - Contribuinte do Regime Normal (CRT=3) com opção pendente pelo CRT=1 (Simples Nacional) sem regime regular do IBS e da CBS")]
    NormalOpcaoPendenteSimplesNacionalSemRegimeRegular = 2,

    [DFeEnum("3")] [Description("3 - Contribuinte do Regime Normal (CRT=3) com opção pendente pelo CRT=2 (Simples Nacional, excesso sublimite de receita bruta) sem regime regular do IBS e da CBS")]
    NormalOpcaoPendenteSimplesNacionalExcessoSemRegimeRegular = 3,

    [DFeEnum("4")] [Description("4 - Contribuinte do Regime Normal (CRT=3) com opção pendente pelo Simples Nacional (CRT=1 ou 2), com regime regular do IBS e da CBS")]
    NormalOpcaoPendenteSimplesNacionalComRegimeRegular = 4,
}