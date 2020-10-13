using System.ComponentModel;
using Vip.DFe.Attributes;

namespace Vip.DFe.NFe.Enum
{
    /// <summary>
    ///     Indicador da origem do processo
    ///     <para />
    ///     <br /> 0 = SEFAZ
    ///     <br /> 1 = Justiça Federal
    ///     <br /> 2 = Justiça Estadual
    ///     <br /> 3 = Secex/RFB
    ///     <br /> 9 = Outros
    /// </summary>
    public enum NFeIndProcesso
    {
        [DFeEnum("0")] [Description("0 - SEFAZ")]
        Sefaz = 0,

        [DFeEnum("1")] [Description("1 - Justiça Federal")]
        JusticaFederal = 1,

        [DFeEnum("2")] [Description("2 - Justiça Estadual")]
        JusticaEstadual = 2,

        [DFeEnum("3")] [Description("3 - Secex/RFB")]
        SecexRFB = 3,

        [DFeEnum("9")] [Description("9 - Outros")]
        Outros = 9
    }
}