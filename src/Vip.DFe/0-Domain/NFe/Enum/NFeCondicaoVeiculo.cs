using System.ComponentModel;
using Vip.DFe.Attributes;

namespace Vip.DFe.NFe.Enum
{
    /// <summary>
    ///     Condição do Veículo
    /// </summary>
    public enum NFeCondicaoVeiculo
    {
        [DFeEnum("1")] [Description("1 - Acabado")]
        Acabado = 1,

        [DFeEnum("2")] [Description("2 - Inacabado")]
        Inacabado = 2,

        [DFeEnum("3")] [Description("3 - Semi-Acabado")]
        Semiacabado = 3
    }
}