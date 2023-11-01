using Vip.DFe.Attributes;

namespace Vip.DFe.NFe.Enum;

/// <summary>
///     LA19 - Indicador de importação
/// </summary>
public enum NFeIndImport
{
    [DFeEnum("0")] Nacional = 0,
    [DFeEnum("1")] Importado = 1
}