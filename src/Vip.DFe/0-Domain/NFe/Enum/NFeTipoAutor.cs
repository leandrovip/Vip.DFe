using System.ComponentModel;
using Vip.DFe.Attributes;

namespace Vip.DFe.NFe.Enum;

/// <summary>
///     Indicador do tipo de autor. Campo exclusivo do evento de cancelamento por substituição.
/// </summary>
public enum NFeTipoAutor
{
    [DFeEnum("1")] [Description("1 - Uso Empresa Emitente")]
    EmpresaEmitente = 1,

    [DFeEnum("2")] [Description("2 - Empresa Destinatária")]
    EmpresaDestinataria = 2,

    [DFeEnum("3")] [Description("3 - Empresa")]
    Empresa = 3,

    [DFeEnum("5")] [Description("5 - Fisco")]
    Fisco = 5,

    [DFeEnum("6")] [Description("6 - RFB")]
    RFB = 6,
    
    [DFeEnum("9")] [Description("9 - Outros Órgãos")]
    OutrosOrgaos = 9,
}