using System.ComponentModel;
using Vip.DFe.Attributes;

namespace Vip.DFe.NFe.Enum
{
    /// <summary>
    ///     Tipo de Ente Governamental que realizou a compra
    /// </summary>
    public enum NFeTipoCompraGov
    {
        [DFeEnum("1")] [Description("1 - União")]
        Uniao = 1,

        [DFeEnum("2")] [Description("2 - Estado")]
        Estado = 2,

        [DFeEnum("3")] [Description("3 - Distrito Federal")]
        DistritoFederal = 3,

        [DFeEnum("4")] [Description("4 - Município")]
        Municipio = 4,

        [DFeEnum("5")] [Description("5 - Consórcio Público")]
        ConsorcioPublico = 5,

        [DFeEnum("6")] [Description("6 - Comitê Gestor do IBS")]
        ComiteGestorIbs = 6,
    }
}