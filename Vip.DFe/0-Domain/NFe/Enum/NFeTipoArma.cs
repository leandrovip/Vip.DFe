using System.ComponentModel;
using Vip.DFe.Attributes;

namespace Vip.DFe.NFe.Enum
{
    /// <summary>
    ///     Indicador do tipo de arma de fogo
    /// </summary>
    public enum NFeTipoArma
    {
        [DFeEnum("0")] [Description("0 - Uso Permitido")]
        UsoPermitido = 0,

        [DFeEnum("1")] [Description("1 - Uso Restrito")]
        UsoRestrito = 1
    }
}