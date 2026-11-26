using System.ComponentModel;
using Vip.DFe.Attributes;

namespace Vip.DFe.NFe.Enum;

/// <summary>
///     I17d - Código de atividade do Simples Nacional (NT 2026.005).
///     Informar somente quando o emitente for optante do Simples Nacional ou estiver com opção pendente.
/// </summary>
public enum CodigoAtividadeSimplesNacional
{
    [DFeEnum("01")] [Description("01 - Revenda de mercadorias, venda de mercadorias industrializadas pelo contribuinte - Anexo I")]
    RevendaMercadoriasAnexoI = 1,

    [DFeEnum("02")] [Description("02 - Operações com os demais bens materiais, sem incidência de ICMS - Anexo I")]
    DemaisBensSemIcmsAnexoI = 2,

    [DFeEnum("03")] [Description("03 - Venda de mercadorias industrializadas pelo contribuinte, com industrialização incentivada na Zona Franca de Manaus - Anexo II")]
    IndustrializadasZfmAnexoII = 3,

    [DFeEnum("04")] [Description("04 - Operações com os demais bens materiais, com industrialização incentivada na Zona Franca de Manaus, sem incidência de ICMS - Anexo II")]
    DemaisBensZfmSemIcmsAnexoII = 4,

    [DFeEnum("90")] [Description("90 - Operações não tributáveis")]
    OperacoesNaoTributaveis = 90,
}