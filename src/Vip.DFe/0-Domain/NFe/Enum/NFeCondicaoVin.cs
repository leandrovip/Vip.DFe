using System.ComponentModel;
using Vip.DFe.Attributes;

namespace Vip.DFe.NFe.Enum
{
    /// <summary>
    ///     Informa-se o veículo tem VIN (chassi) remarcado.
    /// </summary>
    public enum NFeCondicaoVin
    {
        [DFeEnum("N")] [Description("N - Normal")]
        Normal = 0,

        [DFeEnum("R")] [Description("R - Remarcado")]
        Remarcado = 1
    }
}