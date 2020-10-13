using System.ComponentModel;
using Vip.DFe.Attributes;

namespace Vip.DFe.NFe.Enum
{
    /// <summary>
    ///     Indica se valor do Item (vProd) entra no valor total da NF-e(vProd)
    /// </summary>
    public enum NFeIndTotal
    {
        [DFeEnum("0")] [Description("0 - Valor do item não compõe o total da NFe")]
        ValorItemNaoCompoeTotalNota = 0,

        [DFeEnum("1")] [Description("1 - Valor do item compõe o total da NFe")]
        ValorItemCompoeTotalNota = 1,
    }
}