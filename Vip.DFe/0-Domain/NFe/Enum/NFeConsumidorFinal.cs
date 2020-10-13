using System.ComponentModel;
using Vip.DFe.Attributes;

namespace Vip.DFe.NFe.Enum
{
    public enum NFeConsumidorFinal
    {
        [DFeEnum("0")] [Description("0 - Não")]
        Nao = 0,

        [DFeEnum("1")] [Description("1 - Consumidor Final")]
        Sim = 1
    }
}