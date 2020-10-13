using System.ComponentModel;
using Vip.DFe.Attributes;

namespace Vip.DFe.NFe.Enum
{
    /// <summary>
    ///     Tipo de combustível
    /// </summary>
    public enum NFeTipoCombustivel
    {
        [DFeEnum("01")] [Description("01 - Álcool")]
        Alcool = 1,

        [DFeEnum("02")] [Description("02 - Gasolina")]
        Gasolina = 2,

        [DFeEnum("03")] [Description("03 - Diesel")]
        Diesel = 3,

        [DFeEnum("16")] [Description("16 - Álcool / Gasolina")]
        AlcoolGasolina = 16,

        [DFeEnum("17")] [Description("17 - Gasolina / Álcool / GNV")]
        GasolinaAlcoolGNV = 17,

        [DFeEnum("18")] [Description("18 - Gasolina / Elétrico")]
        GasolinaEletrico = 18
    }
}